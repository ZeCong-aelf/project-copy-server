using System.Threading.Tasks;
using ProjectCopyServer.Users.Dto;

namespace ProjectCopyServer.Samples.Users.Provider;

public interface IUserInformationProvider
{

    /// delete this method, just a demo
    public Task<UserDto> SaveUserSourceAsync(UserSourceInput userSourceInput);

}