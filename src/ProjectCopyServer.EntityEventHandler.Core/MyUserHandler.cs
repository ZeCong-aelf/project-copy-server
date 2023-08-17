using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AElf.Indexing.Elasticsearch;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectCopyServer.Users.Eto;
using ProjectCopyServer.Users.Index;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;

namespace ProjectCopyServer.EntityEventHandler.Core;

public class MyUserHandler : IDistributedEventHandler<UserInformationEto>, ITransientDependency
{
    private readonly INESTRepository<UserIndex, Guid> _userRepository;
    private readonly IObjectMapper _objectMapper;
    private readonly ILogger<MyUserHandler> _logger;

    public MyUserHandler(INESTRepository<UserIndex, Guid> userRepository,
                         IObjectMapper objectMapper,
                         ILogger<MyUserHandler> logger)
    {
        _userRepository = userRepository;
        _objectMapper = objectMapper;
        _logger = logger;
    }

    public async Task HandleEventAsync(UserInformationEto eventData)
    {
        try
        {
            var userInfo = _objectMapper.Map<UserInformationEto, UserIndex>(eventData);
            if (eventData.CaAddressSide != null)
            {
                List<UserAddress> userAddresses = new List<UserAddress>();
                foreach (var addressMap in eventData.CaAddressSide)
                {
                    UserAddress userAddress = new UserAddress
                    {
                        ChainId = addressMap.Key,
                        Address = addressMap.Value
                    };
                    userAddresses.Add(userAddress);
                }

                userInfo.CaAddressListSide = userAddresses;
            }

            await _userRepository.AddOrUpdateAsync(userInfo);

            if (userInfo != null)
            {
                _logger.LogDebug("User information add or update success: {userInformation}",
                    JsonConvert.SerializeObject(userInfo));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "User information add or update fail: {Data}",
                JsonConvert.SerializeObject(eventData));
        }
    }
}