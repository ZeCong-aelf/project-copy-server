using System.Threading.Tasks;
using GraphQL;
using Volo.Abp.DependencyInjection;

namespace ProjectCopyServer.Samples.GraphQL;


public interface IActivityProvider
{
    Task<IndexerSymbols> GetTokenInfoAsync(string symbol);
}

public class ActivityProvider : IActivityProvider, ISingletonDependency
{
    private readonly IGraphQlHelper _graphQlHelper;

    public ActivityProvider(IGraphQlHelper graphQlHelper)
    {
        _graphQlHelper = graphQlHelper;
    }
    
    
    public async Task<IndexerSymbols> GetTokenInfoAsync(string symbol)
    {
        var res = await _graphQlHelper.QueryAsync<IndexerSymbols>(new GraphQLRequest
        {
            Query = @"query(
                        $symbol:String,
                        $skipCount:Int!,
                        $maxResultCount:Int!
                    ) {
                        tokenInfo(
                            dto: {symbol:$symbol,skipCount:$skipCount,maxResultCount:$maxResultCount}
                        ){
                            decimals,symbol,chainId
                        }
                    }",
            Variables = new
            {
                symbol, skipCount = 0, maxResultCount = 1
            }
        });
        return res;
    }
    
}