using System;

namespace ProjectCopyServer.Samples.Users.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public string Address { get; set; }
    public string FullAddress { get; set; }
    public string Name { get; set; }

    public string ProfileImage { get; set; }

    public string ProfileImageOriginal { get; set; }

    public string BannerImage { get; set; }
    public string Email { get; set; }
    public string Twitter { get; set; }
    public string Instagram { get; set; }
}