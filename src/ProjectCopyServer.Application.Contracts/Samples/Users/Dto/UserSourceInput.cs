using System;
using System.Collections.Generic;

namespace ProjectCopyServer.Samples.Users.Dto;

public class UserSourceInput
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string AelfAddress { get; set; }
    public string CaHash { get; set; }
    public string CaAddressMain { get; set; }
    public Dictionary<string, string> CaAddressSide { get; set; } = new();
}