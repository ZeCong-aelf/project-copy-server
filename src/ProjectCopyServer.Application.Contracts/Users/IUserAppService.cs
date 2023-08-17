using System.Threading.Tasks;
using ProjectCopyServer.Users.Dto;

namespace ProjectCopyServer.Users;

public interface IUserAppService
{
    Task<UserDto> AddUserAsync();
}
