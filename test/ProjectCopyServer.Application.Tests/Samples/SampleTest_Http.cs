using System.Net.Http;
using System.Threading.Tasks;
using AElf;
using AElf.Client.Dto;
using Shouldly;
using Xunit;

namespace ProjectCopyServer.Samples;

public partial class SampleTest
{
    
    [Fact]
    public async Task QueryTransaction()
    {
        var minedTx = new TransactionResultDto { Status = "MINED" };
        var notExistsTx = new TransactionResultDto { Status = "NOTEXISTS" };
        
        // mock mined transaction & query
        MockHttpByPath(HttpMethod.Get, "/api/blockChain/transactionResult", minedTx);
        var tx = await _sampleService.GetTransactionResultAsync(HashHelper.ComputeFrom("").ToHex());
        tx.ShouldNotBeNull();
        tx.Status.ShouldBe("MINED");
        
        // mock notExists transaction & query
        MockHttpByPath(HttpMethod.Get, "/api/blockChain/transactionResult", notExistsTx);
        var tx2 = await _sampleService.GetTransactionResultAsync(HashHelper.ComputeFrom("").ToHex());
        tx2.ShouldNotBeNull();
        tx2.Status.ShouldBe("NOTEXISTS");
    }
}