using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;

namespace ProjectCopyServer.Worker;

public class CollectionCreationWorker : AsyncPeriodicBackgroundWorkerBase
{
    // private readonly ICollectionCreationAppService _collectionCreationAppService;
    //
    public CollectionCreationWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory) : base(timer, serviceScopeFactory)
    {
        
        Timer.Period = 5000; 
    }

    protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
    {

        Logger.LogDebug("Executing collection creation job");
    }
}