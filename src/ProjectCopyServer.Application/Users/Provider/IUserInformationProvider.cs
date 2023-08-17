using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using ProjectCopyServer.Users.Dto;
using ProjectCopyServer.Users.Index;
using Volo.Abp.DependencyInjection;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace ProjectCopyServer.Users.Provider;

public interface IUserInformationProvider
{
 
    public Task<UserDto> SaveUserSourceAsync(UserSourceInput userSourceInput);

    public Task<UserIndex> GetByUserAddressAsync(string inputAddress);
}