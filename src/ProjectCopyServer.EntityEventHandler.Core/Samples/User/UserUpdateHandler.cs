using System;
using System.Threading.Tasks;
using AElf.Indexing.Elasticsearch;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectCopyServer.Common;
using ProjectCopyServer.Samples.Users;
using ProjectCopyServer.Samples.Users.Eto;
using ProjectCopyServer.Users.Index;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;

namespace ProjectCopyServer.EntityEventHandler.Core.Samples.User;

public class UserUpdateHandler : IDistributedEventHandler<UserInformationEto>, ITransientDependency
{
    private readonly INESTRepository<UserIndex, Guid> _userRepository;
    private readonly ILogger<UserUpdateHandler> _logger;
    private readonly IObjectMapper _objectMapper;

    public UserUpdateHandler(INESTRepository<UserIndex, Guid> userRepository,
                         ILogger<UserUpdateHandler> logger, IObjectMapper objectMapper)
    {
        _userRepository = userRepository;
        _logger = logger;
        _objectMapper = objectMapper;
    }

    public async Task HandleEventAsync(UserInformationEto eventData)
    {
        try
        {
            AssertHelper.NotNull(eventData.Data, "UserEto empty");
            var userIndex = _objectMapper.Map<UserGrainDto, UserIndex>(eventData.Data);
            await _userRepository.AddOrUpdateAsync(userIndex);
            _logger.LogDebug("User information add or update success: {UserId}", eventData.Data.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "User information add or update fail: {Data}",
                JsonConvert.SerializeObject(eventData));
        }
    }
}