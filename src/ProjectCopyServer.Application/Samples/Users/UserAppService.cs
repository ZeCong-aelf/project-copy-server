using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using ProjectCopyServer.Samples.Users.Provider;
using ProjectCopyServer.Users;
using ProjectCopyServer.Users.Dto;
using Volo.Abp.EventBus.Distributed;

namespace ProjectCopyServer.Samples.Users;

public class UserAppService : ProjectCopyServerAppService, IUserAppService
{
    private readonly ILogger<UserAppService> _logger;
    private readonly IUserInformationProvider _userInformationProvider;


    public UserAppService(
        IUserInformationProvider userInformationProvider,
        ILogger<UserAppService> logger,
        IDistributedEventBus distributedEventBus,
        IClusterClient clusterClient)

    {
        _userInformationProvider = userInformationProvider;
        _logger = logger;
    }

    
    /// delete this method, just a demo
    public async Task<UserDto> AddUserAsync()
    {
        var userSourceInput = new UserSourceInput
        {
            UserId = Guid.NewGuid(),
            AelfAddress = "slk",
            CaHash = "slk",
            CaAddressMain = "slk",
            CaAddressSide =  new Dictionary<string, string>()
            {
                ["AELF"] = "slk",
                ["tDVV"] = "slk"
            }
        };
        return await _userInformationProvider.SaveUserSourceAsync(userSourceInput);
    }
}