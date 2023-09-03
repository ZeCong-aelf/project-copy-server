using System.Collections.Generic;

namespace ProjectCopyServer.Options;

public class ApiOption
{
    public Dictionary<string, string> ChainNodeApis { get; set; } = new();
}