using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace ProjectCopyGenerator;

internal static class Program
{
    private static IConfiguration? _config;
    private static string[] _ignorePatterns = Array.Empty<string>();
    private static List<string> _excludedExtensions = new List<string>();
    private static string _sourceDirectory = string.Empty;
    private static string _targetDirectory = string.Empty;
    private static string _oldName = string.Empty;
    private static string _newName = string.Empty;

    private static void Main(string[] args)
    {
        LoadConfigurations();

        EnsureTargetDirectoryIsEmpty(_targetDirectory);

        CopyAndRename(_sourceDirectory, _targetDirectory);

        CreateAdditionalFiles();
    }

    private static void LoadConfigurations()
    {
        _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        _excludedExtensions = _config.GetSection("ExcludedExtensions").Get<List<string>>() ?? new List<string>();
        _sourceDirectory = _config["SourceDirectory"] ?? string.Empty;
        _targetDirectory = _config["TargetDirectory"] ?? string.Empty;
        _oldName = _config["NameMapping:From"] ?? string.Empty;
        _newName = _config["NameMapping:To"] ?? string.Empty;
        _ignorePatterns = _config.GetSection("IgnorePatterns").Get<string[]>() ?? Array.Empty<string>();
        
    }

    private static void CreateAdditionalFiles()
    {
        var addFilesSection = _config.GetSection("addFiles");
        foreach (var item in addFilesSection.GetChildren())
        {
            var filePath = item.Key.Replace("{NameMappingTo}", _newName);
            var fileContent = item.Value;
            CreateFileWithContent(Path.Combine(_targetDirectory, filePath), fileContent);
        }
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

            if (ShouldReplaceContent(filePath))
            {
                File.WriteAllText(newFilePath, ReplaceNames(File.ReadAllText(filePath)));
            }
            else
            {
                File.Copy(filePath, newFilePath, true);
            }
        }
    }
    
    private static bool ShouldReplaceContent(string filePath)
    {
        var extension = Path.GetExtension(filePath);
        return !_excludedExtensions.Contains(extension);
    }
    
    static void CreateFileWithContent(string path, string content)
    {
        var directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        File.WriteAllText(path, content);
    }

    private static string ReplaceNames(string input)
    {
        return Regex.Replace(input, Regex.Escape(_oldName), _newName, RegexOptions.IgnoreCase);
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