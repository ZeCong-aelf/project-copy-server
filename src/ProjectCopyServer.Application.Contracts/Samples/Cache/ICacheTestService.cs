using System.Threading.Tasks;
using ProjectCopyServer.Samples.Http;

namespace ProjectCopyServer.Samples.Cache;

public interface ICacheTestService
{
    Task<TransactionResultDto> GetTransactionResultWithCache(string transactionId, string chainId = "AELF");
}