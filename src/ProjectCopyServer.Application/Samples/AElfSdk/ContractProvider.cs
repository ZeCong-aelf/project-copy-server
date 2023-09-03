using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AElf;
using AElf.Client.Dto;
using AElf.Client.Service;
using AElf.Types;
using Google.Protobuf;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectCopyServer.Common;
using ProjectCopyServer.Options;
using ProjectCopyServer.Samples.AElfSdk.Dtos;
using Volo.Abp.Threading;

namespace ProjectCopyServer.Samples.AElfSdk;

public interface IContractProvider
{
}

public class ContractProvider : IContractProvider
{
    private readonly Dictionary<string, AElfClient> _clients = new();
    private readonly Dictionary<string, SenderAccount> _accounts = new();
    private readonly Dictionary<string, string> _emptyDict = new();
    private readonly Dictionary<string, Dictionary<string, string>> _contractAddress = new();

    private readonly ChainOption _chainOption;
    private readonly ILogger<ContractProvider> _logger;

    public ContractProvider(IOptions<ChainOption> chainOption, ILogger<ContractProvider> logger)
    {
        _logger = logger;
        _chainOption = chainOption.Value;
        InitAElfClient();
    }


    private void InitAElfClient()
    {
        if (_chainOption.NodeApis.IsNullOrEmpty())
        {
            return;
        }

        foreach (var node in _chainOption.NodeApis)
        {
            _clients[node.Key] = new AElfClient(node.Value);
            _logger.LogInformation("init AElfClient: {ChainId}, {Node}", node.Key, node.Value);
        }
    }

    private AElfClient Client(string chainId)
    {
        AssertHelper.IsTrue(_clients.ContainsKey(chainId), "AElfClient of {chainId} not found.", chainId);
        return _clients[chainId];
    }

    private SenderAccount Account(string accountName)
    {
        return _accounts.GetOrAdd(accountName, name =>
        {
            var accountOption = _chainOption.AccountPrivateKey.GetValueOrDefault(name);
            AssertHelper.NotEmpty(accountOption, "Account {Name} not found", name);
            var account = new SenderAccount(accountOption);
            _logger.LogInformation("init Sender Account: {Name}, {Address}", name,
                _accounts[name].Address.ToBase58());
            return account;
        });
    }

    private string ContractAddress(string chainId, string contractName)
    {
        var contractAddress = _contractAddress.GetOrAdd(chainId, _ => new Dictionary<string, string>());
        return contractAddress.GetOrAdd(contractName, name =>
        {
            var address = _chainOption.ContractAddress
                .GetValueOrDefault(chainId, _emptyDict)
                .GetValueOrDefault(name, null);
            if (address.IsNullOrEmpty() && SystemContractName.All.Contains(name))
                address = AsyncHelper
                    .RunSync(() => Client(chainId).GetContractAddressByNameAsync(HashHelper.ComputeFrom(name)))
                    .ToBase58();

            AssertHelper.NotEmpty(address, "Address of contract {contractName} on {chainId} not exits.",
                name, chainId);
            return address;
        });
    }

    public Hash CreateTransaction(string chainId, string senderName, string contractName, string methodName,
        IMessage param, out Transaction transaction)
    {
        var address = ContractAddress(chainId, contractName);
        var client = Client(chainId);
        var status = client.GetChainStatusAsync().GetAwaiter().GetResult();
        var height = status.BestChainHeight;
        var blockHash = status.BestChainHash;
        var account = Account(senderName);

        // create raw transaction
        transaction = new Transaction
        {
            From = account.Address,
            To = Address.FromBase58(address),
            MethodName = methodName,
            Params = param.ToByteString(),
            RefBlockNumber = height,
            RefBlockPrefix = ByteString.CopyFrom(Hash.LoadFromHex(blockHash).Value.Take(4).ToArray())
        };

        var transactionId = HashHelper.ComputeFrom(transaction.ToByteArray());
        transaction.Signature = account.GetSignatureWith(transactionId.ToByteArray());
        return transactionId;
    }

    public async Task SendTransactionAsync(string chainId, Transaction transaction)
    {
        var client = Client(chainId);
        await client.SendTransactionAsync(new SendTransactionInput()
        {
            RawTransaction = transaction.ToByteArray().ToHex()
        });
    }
}