using System;
using System.Threading.Tasks;
using AElf.Contracts.MultiToken;
using AElf.Types;
using Microsoft.Extensions.Caching.Distributed;
using ProjectCopyServer.Common;
using ProjectCopyServer.Samples.AElfSdk;
using ProjectCopyServer.Samples.AElfSdk.Dtos;
using ProjectCopyServer.Samples.GraphQL;
using ProjectCopyServer.Samples.Http;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace ProjectCopyServer.Samples;

public class SampleService : ISampleService, ISingletonDependency
{
    private readonly IDistributedCache<TransactionResultDto> _seedLockCache;
    private readonly ITransactionHttpProvider _transactionHttpProvider;
    private readonly ContractProvider _contractProvider;
    private readonly IActivityProvider _activityProvider;


    public SampleService(IDistributedCache<TransactionResultDto> seedLockCache,
        ITransactionHttpProvider transactionHttpProvider, IActivityProvider activityProvider, ContractProvider contractProvider)
    {
        _seedLockCache = seedLockCache;
        _transactionHttpProvider = transactionHttpProvider;
        _activityProvider = activityProvider;
        _contractProvider = contractProvider;
    }

    // Cache sample
    public async Task<TransactionResultDto> GetTransactionResultWithCache(string transactionId, string chainId = "AELF")
    {
        return await _seedLockCache.GetOrAddAsync(transactionId,
            () => GetTransactionResultAsync(transactionId, chainId),
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(10),
            }
        );
    }

    // Http sample
    public async Task<TransactionResultDto> GetTransactionResultAsync(string transactionId, string chainId = "AELF")
    {
        return await _transactionHttpProvider.GetTransactionResultAsync(transactionId);
    }

    // AElf-sdk sample
    public async Task<decimal> QueryBalance(string chainId, string address)
    {
        var (transactionId, transaction) = await _contractProvider.CreateTransaction(chainId, "User1",
            SystemContractName.TokenContract, "GetBalance", new GetBalanceInput
            {
                Owner = Address.FromBase58(address),
                Symbol = TokenSymbol.ELF,
            });

        var balance = await _contractProvider.CallTransactionAsync<GetBalanceOutput>(chainId, transaction);

        return balance.Balance;
    }

    // GraphQL sample
    public Task<IndexerSymbols> GetTokenInfoAsync(string symbol)
    {
        return _activityProvider.GetTokenInfoAsync(symbol);
    }
    
}