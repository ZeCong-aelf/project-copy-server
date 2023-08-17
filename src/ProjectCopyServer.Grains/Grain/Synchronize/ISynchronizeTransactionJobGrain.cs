using Orleans;

namespace ProjectCopyServer.Grains.Grain.Synchronize;

public interface ISynchronizeTransactionJobGrain : IGrainWithStringKey
{
    Task<GrainResultDto<SynchronizeTransactionJobGrainDto>> CreateSynchronizeTransactionJobAsync(
        CreateSynchronizeTransactionJobGrainDto input);

    Task<GrainResultDto<SynchronizeTransactionJobGrainDto>> ExecuteJobAsync(SynchronizeTransactionJobGrainDto input);
    Task<GrainResultDto<SynchronizeTransactionJobGrainDto>> QueryTokenStatusBySymbolResults(GetTokenStatusBySymbolGrainDto input);
}