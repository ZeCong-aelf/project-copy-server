using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using ProjectCopyServer.Common;
using ProjectCopyServer.Grains.Grain.Users;
using ProjectCopyServer.Users.Dto;
using ProjectCopyServer.Users.Eto;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;

namespace ProjectCopyServer.Samples.Users.Provider;

public class UserInformationProvider: IUserInformationProvider, ISingletonDependency
{
    private readonly ILogger<UserInformationProvider> _logger;
    private readonly IClusterClient _clusterClient;
    private readonly IObjectMapper _objectMapper;
    private readonly IDistributedEventBus _distributedEventBus;
    
    
    public UserInformationProvider(IClusterClient clusterClient, IObjectMapper objectMapper, 
                                   ILogger<UserInformationProvider> logger,IDistributedEventBus distributedEventBus)
    {
        _clusterClient = clusterClient;
        _objectMapper = objectMapper;
        _logger = logger;
        _distributedEventBus = distributedEventBus;
    }

    public async Task<UserDto> SaveUserSourceAsync(UserSourceInput userSourceInput)
    {
        try
        {
            var userGrain = _clusterClient.GetGrain<IUserGrain>(userSourceInput.UserId);
            var userGrainDto = _objectMapper.Map<UserSourceInput, UserGrainDto>(userSourceInput);
            var result = await userGrain.UpdateUserAsync(userGrainDto);
            AssertHelper.IsTrue(result.Success, "Save user fail.");
            
            // publish event to wright data to ES
            await _distributedEventBus.PublishAsync(
                _objectMapper.Map<UserGrainDto, UserInformationEto>(result.Data), false);
            return _objectMapper.Map<UserGrainDto, UserDto>(result.Data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "save error!");
            throw;
        }
    }

}