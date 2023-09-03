using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectCopyServer.Common.Dtos;
using ProjectCopyServer.Samples.Http;
using ProjectCopyServer.Samples.Users;
using ProjectCopyServer.Users;
using ProjectCopyServer.Users.Dto;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace ProjectCopyServer.Controllers.Samples;

[RemoteService]
[Area("app")]
[ControllerName("SampleDemo")]
[Route("api/app/transaction")]
public class TransactionDemoController : AbpController
{
    private readonly ITransactionAppService _transactionAppService;

    public TransactionDemoController(ITransactionAppService transactionAppService)
    {
        _transactionAppService = transactionAppService;
    }

    /// post method used to query data
    [HttpGet]
    public async Task<TransactionResultDto> GetTransactionResult(string transactionId, string chainId = "AELF")
    {
        return await _transactionAppService.GetTransactionResultAsync(transactionId, chainId);
    }

}