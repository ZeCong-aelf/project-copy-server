using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectCopyServer.Samples.GraphQL;
using ProjectCopyServer.Samples.Http;
using Volo.Abp;

namespace ProjectCopyServer.Controllers.Samples;

[RemoteService]
[Area("app")]
[ControllerName("SampleDemo")]
[Route("api/app/graphql")]
public class GraphQlController
{
    private ISymbolService _symbolService;

    public GraphQlController(ISymbolService symbolService)
    {
        _symbolService = symbolService;
    }


    [HttpGet("decimals")]
    public async Task<IndexerSymbols> GetTransactionResult(string symbol = "ELF")
    {
        return await _symbolService.GetTokenDecimalsAsync(symbol);
    }
    
    
}