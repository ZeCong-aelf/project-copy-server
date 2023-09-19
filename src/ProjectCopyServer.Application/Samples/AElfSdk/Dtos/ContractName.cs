using System.Collections.Generic;

namespace ProjectCopyServer.Samples.AElfSdk.Dtos;

public class SystemContractName
{
    public const string SystemBasicContractZero = "";
    public const string SystemCrossChainContract = "AElf.ContractNames.CrossChain";
    public const string SystemTokenContract = "AElf.ContractNames.Token";
    public const string SystemParliamentContract = "AElf.ContractNames.Parliament";
    public const string SystemConsensusContract = "AElf.ContractNames.Consensus";
    public const string SystemReferendumContract = "AElf.ContractNames.Referendum";
    public const string SystemTreasuryContract = "AElf.ContractNames.Treasury";
    public const string SystemAssociationContract = "AElf.ContractNames.Association";
    public const string SystemTokenConverterContract = "AElf.ContractNames.TokenConverter";

    public static readonly List<string> All = new()
    {
        SystemBasicContractZero, SystemCrossChainContract, SystemTokenContract,
        SystemParliamentContract, SystemConsensusContract, SystemReferendumContract,
        SystemTreasuryContract, SystemAssociationContract, SystemTokenConverterContract,
    };
}


public class ContractName : SystemContractName
{
    // add non-system contract name here
    public const string ProxyAccountContract = "ProxyAccountContract";

}