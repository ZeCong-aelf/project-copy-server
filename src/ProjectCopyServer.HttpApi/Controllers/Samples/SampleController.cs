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
        string transactionId = "4937584b45ab17872e2331e5709e3a81f6b66de569ed3436dcc071f4c58e9c92",
        string chainId = "AELF")
    {
        return await _sampleService.GetTransactionResultAsync(transactionId, chainId);
    }

    [HttpGet("cache/transaction")]
    public async Task<TransactionResultDto> GetCachedTransactionResult(
        string transactionId = "4937584b45ab17872e2331e5709e3a81f6b66de569ed3436dcc071f4c58e9c92",
        string chainId = "AELF")
    {
        return await _sampleService.GetTransactionResultWithCache(transactionId, chainId);
    }

    [HttpGet("sdk/userBalance")]
    public async Task<decimal> QueryBalance(
        string address = "23GxsoW9TRpLqX1Z5tjrmcRMMSn5bhtLAf4HtPj8JX9BerqTqp",
        string chainId = "AELF")
    {
        return await _sampleService.QueryBalance(chainId, address);
    }
    
}