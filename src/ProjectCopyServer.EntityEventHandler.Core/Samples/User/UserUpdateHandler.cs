using System;
using System.Threading.Tasks;
using AElf.Indexing.Elasticsearch;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectCopyServer.Common;
using ProjectCopyServer.Users.Eto;
using ProjectCopyServer.Users.Index;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace ProjectCopyServer.EntityEventHandler.Core.Samples.User;

public class UserUpdateHandler : IDistributedEventHandler<UserInformationEto>, ITransientDependency
{
    private readonly INESTRepository<UserIndex, Guid> _userRepository;
    private readonly ILogger<UserUpdateHandler> _logger;

    public UserUpdateHandler(INESTRepository<UserIndex, Guid> userRepository,
                         ILogger<UserUpdateHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task HandleEventAsync(UserInformationEto eventData)
    {
        try
        {
            AssertHelper.NotNull(eventData.Data, "UserEto empty");
            await _userRepository.AddOrUpdateAsync(eventData.Data);
            _logger.LogDebug("User information add or update success: {UserId}", eventData.Data.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "User information add or update fail: {Data}",
                JsonConvert.SerializeObject(eventData));
        }
    }
}