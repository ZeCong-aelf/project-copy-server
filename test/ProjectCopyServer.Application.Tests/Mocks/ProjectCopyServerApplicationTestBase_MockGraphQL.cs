using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GraphQL;
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


    protected void MockGraphQlRes<TRes>(TRes returns, string keyword, Dictionary<string, object>? expectedVariables = null)
    {
        var response = new GraphQLResponse<TRes>
        {
            Data = returns,
        };
        _mockGraphQlClient
            .Setup(o => o.SendQueryAsync<TRes>(It.Is<GraphQLRequest>(req => 
                    req.Query.Contains(keyword) &&
                    AreVariablesMatching(req, expectedVariables)), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
    }

    private bool AreVariablesMatching(GraphQLRequest request, IDictionary<string, object>? expectedVariables)
    {
        if (expectedVariables == null) return true;
        foreach (var kvp in expectedVariables)
        {
            var actualValue = GetVariableValue(request.Variables, kvp.Key);
            if (!object.Equals(actualValue, kvp.Value))
                return false;
        }

        return true;
    }

    private object GetVariableValue(object variablesObj, string variableName)
    {
        var propertyInfo = variablesObj.GetType().GetProperty(variableName);
        return propertyInfo?.GetValue(variablesObj);
    }


}