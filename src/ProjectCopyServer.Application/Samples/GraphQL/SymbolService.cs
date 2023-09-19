using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ProjectCopyServer.Samples.GraphQL;

public class SymbolService : ISymbolService, ISingletonDependency
{
    private IActivityProvider _activityProvider;

    public SymbolService(IActivityProvider activityProvider)
    {
        _activityProvider = activityProvider;
    }


    public Task<IndexerSymbols> GetTokenDecimalsAsync(string symbol)
    {
        return _activityProvider.GetTokenDecimalsAsync(symbol);
    }
}