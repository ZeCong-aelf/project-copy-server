using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectCopyServer.Samples;
using ProjectCopyServer.Samples.GraphQL;
using ProjectCopyServer.Samples.Http;
using Volo.Abp;

namespace ProjectCopyServer.Controllers.Samples;

[RemoteService]
[Area("app")]
[ControllerName("SampleDemo")]
[Route("api/app/samples")]
public class SamplesController
{
    private ISampleService _sampleService;

    public SamplesController(ISampleService sampleService)
    {
        _sampleService = sampleService;
    }


    [HttpGet("graphql/tokenInfo")]
    public async Task<IndexerSymbols> GetTokenInfo(string symbol = "ELF")
    {
        return await _sampleService.GetTokenInfoAsync(symbol);
    }
    
    
    [HttpGet("http/transaction")]
    public async Task<TransactionResultDto> GetTransactionHttpResult(
        string transactionId = "2ee64abbdfbb1a76fa8084c05e69a8dd1a413059b4e4171eb2b1c106b28052da",
        string chainId = "AELF")
    {
        return await _sampleService.GetTransactionResultAsync(transactionId, chainId);
    }

    [HttpGet("cache/transaction")]
    public async Task<TransactionResultDto> GetCachedTransactionResult(
        string transactionId = "2ee64abbdfbb1a76fa8084c05e69a8dd1a413059b4e4171eb2b1c106b28052da",
        string chainId = "AELF")
    {
        return await _sampleService.GetTransactionResultWithCache(transactionId, chainId);
    }

    [HttpGet("lock/transaction")]
    public async Task<TransactionResultDto> GetLockedTransactionResult(
        string transactionId = "2ee64abbdfbb1a76fa8084c05e69a8dd1a413059b4e4171eb2b1c106b28052da",
        string chainId = "AELF")
    {
        return await _sampleService.GetTransactionResultWithLock(transactionId, chainId);
    }

    [HttpGet("sdk/userBalance")]
    public async Task<decimal> QueryBalance(
        string address = "JRmBduh4nXWi1aXgdUsj5gJrzeZb2LxmrAbf7W99faZSvoAaE",
        string chainId = "AELF")
    {
        return await _sampleService.QueryBalance(chainId, address);
    }
    
}