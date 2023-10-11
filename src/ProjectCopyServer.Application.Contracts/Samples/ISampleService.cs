using System.Threading.Tasks;
using ProjectCopyServer.Samples.GraphQL;
using ProjectCopyServer.Samples.Http;

namespace ProjectCopyServer.Samples;

public interface ISampleService
{
    
    // Cache sample
    Task<TransactionResultDto> GetTransactionResultWithCache(string transactionId, string chainId = "AELF");
    
    // Http sample
    Task<TransactionResultDto> GetTransactionResultAsync(string transactionId, string chainId = "AELF");

    // AElf-sdk sample
    Task<decimal> QueryBalance(string chainId, string address);
    
    // GraphQL sample
    Task<IndexerSymbols> GetTokenInfoAsync(string symbol);

}