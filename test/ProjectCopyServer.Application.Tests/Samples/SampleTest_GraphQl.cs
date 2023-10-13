using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectCopyServer.Samples.GraphQL;
using Shouldly;
using Xunit;

namespace ProjectCopyServer.Samples;

public partial class SampleTest
{

    [Fact]
    public async Task GraphQlTest()
    {
        
        MockGraphQlRes(new IndexerSymbols
        {
            TokenInfo = new List<SymbolInfo>{ new (){ Symbol = "USDT", Decimals = 2}}
        }, GraphQlMethodPattern("tokenInfo"));
        
        var res = await _sampleService.GetTokenInfoAsync("USDT");
        res.ShouldNotBeNull();
        res.TokenInfo.ShouldNotBeEmpty();
        res.TokenInfo[0].Symbol.ShouldBe("USDT");
        res.TokenInfo[0].Decimals.ShouldBe(2);
        
        
        MockGraphQlRes(new IndexerSymbols
        {
            TokenInfo = new List<SymbolInfo>{ new (){ Symbol = "USDT", Decimals = 3}}
        }, GraphQlMethodPattern("tokenInfo"));
        res = await _sampleService.GetTokenInfoAsync("USDT");
        res.ShouldNotBeNull();
        res.TokenInfo.ShouldNotBeEmpty();
        res.TokenInfo[0].Symbol.ShouldBe("USDT");
        res.TokenInfo[0].Decimals.ShouldBe(3);
    }
    
    
}