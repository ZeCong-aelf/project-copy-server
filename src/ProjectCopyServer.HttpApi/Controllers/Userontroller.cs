using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectCopyServer.Users;
using ProjectCopyServer.Users.Dto;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace ProjectCopyServer.Controllers;

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
    
    /// delete this method, just a demo
    [HttpGet]
    public async Task<UserDto> AddUser()
    {
        return await _userAppService.AddUserAsync();
    }
}