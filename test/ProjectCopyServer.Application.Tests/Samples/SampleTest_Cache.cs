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
    public async Task CacheTest()
    {
        // mock http query
        var minedTx = new TransactionResultDto { Status = "MINED" };
        MockHttpByPath(HttpMethod.Get, "/api/blockChain/transactionResult", minedTx);
        
        // two invokes only print "Mock Http" log once
        var tx = await _sampleService.GetTransactionResultWithCache(HashHelper.ComputeFrom("").ToHex());
        Output.WriteLine(tx.Status);
        tx = await _sampleService.GetTransactionResultWithCache(HashHelper.ComputeFrom("").ToHex());
        Output.WriteLine(tx.Status);

        // after delay 11s, will print "Mock Http" log again
        await Task.Delay(11000);
        tx = await _sampleService.GetTransactionResultWithCache(HashHelper.ComputeFrom("").ToHex());
        Output.WriteLine(tx.Status);
    }
}