using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectCopyServer.Samples.Http;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace ProjectCopyServer.Controllers.Samples;

[RemoteService]
[Area("app")]
[ControllerName("SampleDemo")]
[Route("api/app/chain")]
public class TransactionDemoController : AbpController
{
    private readonly ITransactionAppService _transactionAppService;

    public TransactionDemoController(ITransactionAppService transactionAppService)
    {
        _transactionAppService = transactionAppService;
    }

    /// post method used to query data
    [HttpGet("transaction")]
    public async Task<TransactionResultDto> GetTransactionResult(
        string transactionId = "4937584b45ab17872e2331e5709e3a81f6b66de569ed3436dcc071f4c58e9c92",
        string chainId = "AELF")
    {
        return await _transactionAppService.GetTransactionResultAsync(transactionId, chainId);
    }

    [HttpGet("balance")]
    public async Task<decimal> QueryBalance(
        string address = "23GxsoW9TRpLqX1Z5tjrmcRMMSn5bhtLAf4HtPj8JX9BerqTqp",
        string chainId = "AELF")
    {
        return await _transactionAppService.QueryBalance(chainId, address);
    }
}