using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;


namespace ProjectCopyServer.ContractEventHandler.Core.Application;

public interface ISynchronizeTransactionAppService
{
    Task<List<string>> SearchUnfinishedSynchronizeTransactionAsync();
    Task ExecuteJobAsync(string txHash);
}

public class SynchronizeTransactionAppService : ISynchronizeTransactionAppService, ISingletonDependency,
    ITransientDependency
{
    public Task<List<string>> SearchUnfinishedSynchronizeTransactionAsync()
    {
        throw new NotImplementedException();
    }

    public Task ExecuteJobAsync(string txHash)
    {
        throw new NotImplementedException();
    }
}