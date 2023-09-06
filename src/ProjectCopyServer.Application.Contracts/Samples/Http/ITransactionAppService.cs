using System.Threading.Tasks;

namespace ProjectCopyServer.Samples.Http;

public interface ITransactionAppService
{
    Task<TransactionResultDto> GetTransactionResultAsync(string transactionId, string chainId = "AELF");
}