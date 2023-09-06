
using System.Collections.Generic;

namespace ProjectCopyServer.Samples.AElfSdk.Dtos;

public class SystemContractName
{
    public static string System_BasicContractZero = "";
    public static string System_CrossChainContract = "AElf.ContractNames.CrossChain";
    public static string System_TokenContract = "AElf.ContractNames.Token";
    public static string System_ParliamentContract = "AElf.ContractNames.Parliament";
    public static string System_ConsensusContract = "AElf.ContractNames.Consensus";
    public static string System_ReferendumContract = "AElf.ContractNames.Referendum";
    public static string System_TreasuryContract = "AElf.ContractNames.Treasury";
    public static string System_AssociationContract = "AElf.ContractNames.Association";
    public static string System_TokenConverterContract = "AElf.ContractNames.TokenConverter";

    public static List<string> All = new()
    {
        System_BasicContractZero, System_CrossChainContract, System_TokenContract,
        System_ParliamentContract, System_ConsensusContract, System_ReferendumContract,
        System_TreasuryContract, System_AssociationContract, System_TokenConverterContract,
    };
}


public class ContractName : SystemContractName
{
    // add non-system contract name here
    public static string ProxyAccountContract = "ProxyAccountContract";

}