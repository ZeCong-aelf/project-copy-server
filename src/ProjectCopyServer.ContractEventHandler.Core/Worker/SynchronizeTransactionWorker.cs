using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProjectCopyServer.ContractEventHandler.Core.Application;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;

namespace ProjectCopyServer.ContractEventHandler.Core.Worker;

public class SynchronizeTransactionWorker : AsyncPeriodicBackgroundWorkerBase
{
    private readonly ISynchronizeTransactionAppService _synchronizeTransactionAppService;
    private readonly ContractSyncOptions _contractSyncOptions;

    public SynchronizeTransactionWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory,
        ISynchronizeTransactionAppService synchronizeTransactionAppService,
        IOptions<ContractSyncOptions> contractSyncOptions) :
        base(timer, serviceScopeFactory)
    {
        _synchronizeTransactionAppService = synchronizeTransactionAppService;
        _contractSyncOptions = contractSyncOptions.Value;
        Timer.Period = 1000 * _contractSyncOptions.Sync;
    }

    protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
    {
        return;
    }
}