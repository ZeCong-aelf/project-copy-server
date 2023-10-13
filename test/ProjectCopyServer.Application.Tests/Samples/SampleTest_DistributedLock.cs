using System.Net.Http;
using System.Threading.Tasks;
using AElf;
using AElf.Client.Dto;
using Shouldly;
using Volo.Abp;
using Xunit;

namespace ProjectCopyServer.Samples;

public partial class SampleTest
{
    
    [Fact]
    public async Task LockTest()
    {
        
        // mock http query
        var minedTx = new TransactionResultDto { Status = "MINED" };
        MockHttpByPath(HttpMethod.Get, "/api/blockChain/transactionResult", minedTx);
        
        // mock distributed
        MockAbpDistributedLockWithTimeout(2000);
        
        // ASYNC invoke first time 
        var taskRes = _sampleService.GetTransactionResultWithLock(HashHelper.ComputeFrom("").ToHex());
        
        // invoke again
        var ex = await Assert.ThrowsAsync<UserFriendlyException>(() =>
            _sampleService.GetTransactionResultWithLock(HashHelper.ComputeFrom("").ToHex()));
        ex.ShouldNotBeNull();
        ex.Message.ShouldContain("Rate limit exceeded");

        // await & verify first invoke
        var res = await taskRes;
        res.ShouldNotBeNull();
        res.Status.ShouldBe("MINED");

        await Task.Delay(3000);
        
        var res2 = await _sampleService.GetTransactionResultWithLock(HashHelper.ComputeFrom("").ToHex());
        res2.ShouldNotBeNull();
        res2.Status.ShouldBe("MINED");
    }
}