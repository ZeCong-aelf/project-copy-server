using System.Threading.Tasks;

namespace ProjectCopyServer.Samples.GraphQL;

public interface ISymbolService
{
    Task<IndexerSymbols> GetTokenDecimalsAsync(string symbol);
}