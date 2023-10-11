using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using ProjectCopyServer.Samples.Http;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace ProjectCopyServer.Samples.Cache;

public class CacheTestService: ICacheTestService, ITransientDependency
{

    private readonly ITransactionAppService _transactionAppService;
    
    private readonly IDistributedCache<TransactionResultDto> _seedLockCache;

    public CacheTestService(IDistributedCache<TransactionResultDto> seedLockCache, ITransactionAppService transactionAppService)
    {
        _seedLockCache = seedLockCache;
        _transactionAppService = transactionAppService;
    }
    

    public async Task<TransactionResultDto> GetTransactionResultWithCache(string transactionId, string chainId = "AELF")
    {
        return await _seedLockCache.GetOrAddAsync(transactionId,  
            () => _transactionAppService.GetTransactionResultAsync(transactionId, chainId),
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(10),
            }
        );
    }
}