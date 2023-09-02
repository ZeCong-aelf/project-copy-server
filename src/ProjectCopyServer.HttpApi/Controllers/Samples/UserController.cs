using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectCopyServer.Common.Dtos;
using ProjectCopyServer.Users;
using ProjectCopyServer.Users.Dto;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace ProjectCopyServer.Controllers.Samples;

[RemoteService]
[Area("app")]
[ControllerName("UserControllerDemo")]
[Route("api/app/users")]
public class MyController : AbpController
{
    private readonly IUserAppService _userAppService;

    public MyController(IUserAppService userAppService)
    {
        _userAppService = userAppService;
    }

    /// post method used to update data
    [HttpPost]
    public async Task<UserDto> AddUser(UserSourceInput userSourceInput)
    {
        return await _userAppService.AddUserAsync(userSourceInput);
    }
    
    /// post method used to query data
    [HttpGet]
    public async Task<UserDto> GetUserById(string userId)
    {
        return await _userAppService.GetById(userId);
    }

    [HttpGet]
    [HttpPatch("/page")]
    public async Task<PageResultDto<UserDto>> QueryUserPager(string address)
    {
        return await _userAppService.QueryUserPagerAsync(new UserQueryRequestDto(0, 10)
        {
            Address = address
        });
    }
    
    
}