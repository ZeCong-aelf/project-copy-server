using Orleans.TestingHost;
using Xunit.Abstractions;

namespace ProjectCopyServer;

public abstract class ProjectCopyServerOrleansTestBase : 
    ProjectCopyServerTestBase<ProjectCopyServerOrleansTestBaseModule>
{

    protected readonly TestCluster Cluster;
    
    protected ProjectCopyServerOrleansTestBase(ITestOutputHelper output) : base(output)
    {
        Cluster = GetRequiredService<ClusterFixture>().Cluster;
    }
}