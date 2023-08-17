using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace ProjectCopyGenerator;

internal static class Program
{
    private static string[] _ignorePatterns = Array.Empty<string>();
    private static string _sourceDirectory = string.Empty;
    private static string _targetDirectory = string.Empty;
    private static string _oldName = string.Empty;
    private static string _newName = string.Empty;

    private static void Main(string[] args)
    {
        LoadConfigurations();

        EnsureTargetDirectoryIsEmpty(_targetDirectory);

        CopyAndRename(_sourceDirectory, _targetDirectory);
    }

    private static void LoadConfigurations()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        _sourceDirectory = config["SourceDirectory"] ?? string.Empty;
        _targetDirectory = config["TargetDirectory"] ?? string.Empty;
        _oldName = config["NameMapping:From"] ?? string.Empty;
        _newName = config["NameMapping:To"] ?? string.Empty;
        _ignorePatterns = config.GetSection("IgnorePatterns").Get<string[]>() ?? Array.Empty<string>();
    }

    private static void EnsureTargetDirectoryIsEmpty(string targetDirectory)
    {
        if (Directory.Exists(targetDirectory))
        {
            var entries = Directory.GetFileSystemEntries(targetDirectory);
            if (entries.Length > 0)
            {
                throw new InvalidOperationException($"The target directory '{targetDirectory}' is not empty.");
            }
        }
        else
        {
            Directory.CreateDirectory(targetDirectory);
        }
    }

    private static void CopyAndRename(string sourceDirectory, string targetDirectory)
    {
        foreach (var dirPath in Directory.GetDirectories(sourceDirectory, "*", SearchOption.AllDirectories))
        {
            if (ShouldIgnore(dirPath)) continue;

            var newDirPath = dirPath.Replace(sourceDirectory, targetDirectory);
            newDirPath = ReplaceNames(newDirPath);
            Directory.CreateDirectory(newDirPath);
        }

        foreach (var filePath in Directory.GetFiles(sourceDirectory, "*.*", SearchOption.AllDirectories))
        {
            if (ShouldIgnore(filePath)) continue;

            var newFilePath = filePath.Replace(sourceDirectory, targetDirectory);
            newFilePath = ReplaceNames(newFilePath);
            File.WriteAllText(newFilePath, ReplaceNames(File.ReadAllText(filePath)));
        }
    }

    private static string ReplaceNames(string input)
    {
        return input.Replace(_oldName, _newName);
    }

    private static bool ShouldIgnore(string path)
    {
        path = path.Replace(Path.DirectorySeparatorChar, '/');

        // If path is a directory, ensure it ends with a slash for pattern matching.
        if (Directory.Exists(path) && !path.EndsWith("/"))
        {
            path += "/";
        }

        foreach (var pattern in _ignorePatterns)
        {
            if (!MatchPattern(pattern, path)) continue;
            Console.WriteLine($"Ignoring: {path} due to pattern: {pattern}");
            return true;
        }

        return false;
    }

    private static bool MatchPattern(string pattern, string path)
    {
        var regexPattern = Regex.Escape(pattern)
            .Replace("\\*", "[^\\/]*")  // * matches anything except for / (or \ on Windows)
            .Replace("\\*\\*", ".*");    // ** matches anything

        return Regex.IsMatch(path, regexPattern, RegexOptions.IgnoreCase);
    }
}