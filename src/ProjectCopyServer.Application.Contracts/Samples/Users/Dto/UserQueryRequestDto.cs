using System;
using Volo.Abp.Application.Dtos;

namespace ProjectCopyServer.Samples.Users.Dto;

public class UserQueryRequestDto : PagedResultRequestDto
{

    public UserQueryRequestDto(int skipCount, int maxResultCount)
    {
        base.SkipCount = skipCount;
        base.MaxResultCount = maxResultCount;
    }    

    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}