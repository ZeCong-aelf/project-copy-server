using System.Threading.Tasks;
using ProjectCopyServer.Common.Dtos;
using ProjectCopyServer.Samples.Users.Dto;
using ProjectCopyServer.Users.Dto;

namespace ProjectCopyServer.Samples.Users;

public interface IUserAppService
{
    /// <summary>
    ///     add or update
    /// </summary>
    /// <param name="userSourceInput"></param>
    /// <returns></returns>
    Task<UserDto> AddUserAsync(UserSourceInput userSourceInput);

    /// <summary>
    ///     query single user by id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<UserDto> GetById(string userId);
    
    /// <summary>
    ///     query pager
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    Task<PageResultDto<UserDto>> QueryUserPagerAsync(UserQueryRequestDto requestDto);
}
