using System.Threading.Tasks;
using ProjectCopyServer.Common.Dtos;
using ProjectCopyServer.Users.Dto;

namespace ProjectCopyServer.Users;

public interface IUserAppService
{
    /// <summary>
    ///     add or update
    /// </summary>
    /// <param name="userSourceInput"></param>
    /// <returns></returns>
    Task<UserDto> AddUserAsync(UserSourceInput userSourceInput);

    Task<UserDto> GetById(string userId);
    
    /// <summary>
    ///     query pager
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    Task<PageResultDto<UserDto>> QueryUserPagerAsync(UserQueryRequestDto requestDto);
}
