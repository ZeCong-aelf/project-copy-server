using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ProjectCopyServer.Common;
using ProjectCopyServer.Common.HttpClient;
using ProjectCopyServer.Options;
using ProjectCopyServer.Samples.HttpClient;
using Volo.Abp.DependencyInjection;

namespace ProjectCopyServer.Samples.Http.Provider;

public interface ITransactionProvider
{

    Task<TransactionResultDto> GetTransactionResultAsync(string transactionId, string chainId = "AELF");

}


public static class NodeApi
{
    public static readonly ApiInfo TransactionResult = new(HttpMethod.Get, "/api/blockChain/transactionResult");
}


public class TransactionProvider : ITransactionProvider, ISingletonDependency
{

    private readonly ApiOption _apiOption;
    private readonly IHttpProvider _httpProvider;

    private static readonly Dictionary<string, string> AcceptJsonHeader = new()
    {
        ["Accept"] = "application/json"
    };

    public TransactionProvider(IOptions<ApiOption> options, IHttpProvider httpProvider)
    {
        _apiOption = options.Value;
        _httpProvider = httpProvider;
    }


    public async Task<TransactionResultDto> GetTransactionResultAsync(string transactionId, string chainId = "AELF")
    {
        var endpoint = _apiOption.ChainNodeApis.GetValueOrDefault(chainId);
        AssertHelper.NotEmpty(endpoint, "chainId {ChainId} node api not found", chainId);
        
        return await _httpProvider.Invoke<TransactionResultDto>(endpoint, NodeApi.TransactionResult,
            header: AcceptJsonHeader,
            param: new Dictionary<string, string>{ ["transactionId"] = transactionId},
            withLog: true
        );
    }
    
}