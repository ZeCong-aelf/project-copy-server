using System.Threading.Tasks;
using AElf.Contracts.MultiToken;
using AElf.Types;
using ProjectCopyServer.Common;
using ProjectCopyServer.Samples.AElfSdk;
using ProjectCopyServer.Samples.AElfSdk.Dtos;
using ProjectCopyServer.Samples.Http.Provider;
using Volo.Abp.DependencyInjection;

namespace ProjectCopyServer.Samples.Http;

public class TransactionAppService : ITransactionAppService, ISingletonDependency
{

    private readonly ITransactionProvider _transactionProvider;
    private readonly ContractProvider _contractProvider;

    public TransactionAppService(ITransactionProvider transactionProvider, ContractProvider contractProvider)
    {
        _transactionProvider = transactionProvider;
        _contractProvider = contractProvider;
    }


    public async Task<TransactionResultDto> GetTransactionResultAsync(string transactionId, string chainId = "AELF")
    {
        return await _transactionProvider.GetTransactionResultAsync(transactionId);
    }

    public async Task<decimal> QueryBalance(string chainId, string address)
    {
        var (transactionId, transaction) = await _contractProvider.CreateTransaction(chainId, "User1", 
            SystemContractName.TokenContract, "GetBalance", new GetBalanceInput
            {
                Owner = Address.FromBase58(address),
                Symbol = TokenSymbol.ELF,
                
            });

        var balance = await _contractProvider.CallTransactionAsync<GetBalanceOutput>(chainId, transaction);
        
        return balance.Balance;
    }
}