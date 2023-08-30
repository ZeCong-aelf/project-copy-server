using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AElf.Indexing.Elasticsearch;
using Microsoft.Extensions.Logging;
using Orleans;
using ProjectCopyServer.Users.Dto;
using ProjectCopyServer.Users.Index;
using ProjectCopyServer.Users.Provider;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;

namespace ProjectCopyServer.Users;

public class UserAppService : ProjectCopyServerAppService, IUserAppService
{
    private readonly ILogger<UserAppService> _logger;
    private readonly IObjectMapper _objectMapper;
    private readonly IUserInformationProvider _userInformationProvider;


    public UserAppService(
        IUserInformationProvider userInformationProvider,
        ILogger<UserAppService> logger,
        IDistributedEventBus distributedEventBus,
        IClusterClient clusterClient,
        IObjectMapper objectMapper)

    {
        _userInformationProvider = userInformationProvider;
        _logger = logger;
        _objectMapper = objectMapper;
    }

    
    /// delete this method, just a demo
    public async Task<UserDto> AddUserAsync()
    {
        Dictionary<string, string> caAddressSide = new Dictionary<string, string>()
        {
            ["AELF"] = "slk",
            ["TDVV"] = "slk"
        };
        UserSourceInput userSourceInput = new UserSourceInput
        {
            UserId = Guid.NewGuid(),
            AelfAddress = "slk",
            CaHash = "slk",
            CaAddressMain = "slk",
            CaAddressSide =  caAddressSide
        };
        return await _userInformationProvider.SaveUserSourceAsync(userSourceInput);
    }
}