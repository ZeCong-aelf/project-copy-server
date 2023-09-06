using System.Threading.Tasks;
using ProjectCopyServer.Samples.Http.Provider;
using Volo.Abp.DependencyInjection;

namespace ProjectCopyServer.Samples.Http;

public class TransactionAppService : ITransactionAppService, ISingletonDependency
{

    private readonly ITransactionProvider _transactionProvider;

    public TransactionAppService(ITransactionProvider transactionProvider)
    {
        _transactionProvider = transactionProvider;
    }


    public async Task<TransactionResultDto> GetTransactionResultAsync(string transactionId, string chainId = "AELF")
    {
        return await _transactionProvider.GetTransactionResultAsync(transactionId);
    }
}