using GraphQL.Client.Abstractions;
using Moq;

namespace ProjectCopyServer;

public partial class ProjectCopyServerApplicationTestBase
{
    private readonly Mock<IGraphQLClient> _mockGraphQlClient = new();


    protected IGraphQLClient MockGraphQl()
    {
        return _mockGraphQlClient.Object;
    }
    
    
    
}