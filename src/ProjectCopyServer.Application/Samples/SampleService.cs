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
using Volo.Abp.DistributedLocking;

namespace ProjectCopyServer.Samples;

public class SampleService : ISampleService, ISingletonDependency
{
    private readonly IDistributedCache<TransactionResultDto> _seedLockCache;
    private readonly ITransactionHttpProvider _transactionHttpProvider;
    private readonly ContractProvider _contractProvider;
    private readonly IActivityProvider _activityProvider;
    private readonly IAbpDistributedLock _distributedLock;


    public SampleService(IDistributedCache<TransactionResultDto> seedLockCache,
        ITransactionHttpProvider transactionHttpProvider, IActivityProvider activityProvider, ContractProvider contractProvider, IAbpDistributedLock distributedLock)
    {
        _seedLockCache = seedLockCache;
        _transactionHttpProvider = transactionHttpProvider;
        _activityProvider = activityProvider;
        _contractProvider = contractProvider;
        _distributedLock = distributedLock;
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

    // Distributed Lock sample
    public async Task<TransactionResultDto> GetTransactionResultWithLock(string transactionId, string chainId = "AELF")
    {
        await using var handle =
            await _distributedLock.TryAcquireAsync("LockPrefix" + transactionId);
        AssertHelper.NotNull(handle, "Rate limit exceeded");

        await Task.Delay(2000);
        
        return await GetTransactionResultAsync(transactionId, chainId);
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