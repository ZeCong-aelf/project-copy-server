using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProjectCopyServer.Common;
using ProjectCopyServer.Common.HttpClient;
using ProjectCopyServer.Options;
using ProjectCopyServer.Samples.HttpClient;
using Volo.Abp.DependencyInjection;

namespace ProjectCopyServer.Samples.Http;

public interface ITransactionHttpProvider
{

    Task<TransactionResultDto> GetTransactionResultAsync(string transactionId, string chainId = "AELF");

}


public static class NodeApi
{
    public static readonly ApiInfo TransactionResult = new(HttpMethod.Get, "/api/blockChain/transactionResult");
}


public class TransactionHttpProvider : ITransactionHttpProvider, ISingletonDependency
{

    private readonly ILogger<TransactionHttpProvider> _logger;
    private readonly IOptionsMonitor<ChainOption> _chainOption;
    private readonly IHttpProvider _httpProvider;

    private static readonly Dictionary<string, string> AcceptJsonHeader = new()
    {
        ["Accept"] = "application/json"
    };

    public TransactionHttpProvider(IOptionsMonitor<ChainOption> options, IHttpProvider httpProvider, ILogger<TransactionHttpProvider> logger)
    {
        _chainOption = options;
        _httpProvider = httpProvider;
        _logger = logger;
    }

    /// <summary>
    ///     Query transaction result via node http-API
    /// </summary>
    /// <param name="transactionId"></param>
    /// <param name="chainId"></param>
    /// <returns></returns>
    public async Task<TransactionResultDto> GetTransactionResultAsync(string transactionId, string chainId = "AELF")
    {
        var endpoint = _chainOption.CurrentValue.NodeApis.GetValueOrDefault(chainId);
        AssertHelper.NotEmpty(endpoint, "chainId {ChainId} node api not found", chainId);
        
        return await _httpProvider.Invoke<TransactionResultDto>(endpoint, NodeApi.TransactionResult,
            header: AcceptJsonHeader,
            param: new Dictionary<string, string>
            {
                ["transactionId"] = transactionId
            },
            withLog: true
        );
    }
    
}