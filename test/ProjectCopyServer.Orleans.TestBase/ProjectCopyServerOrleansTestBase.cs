using Orleans.TestingHost;
using Volo.Abp.Modularity;
using Xunit.Abstractions;

namespace ProjectCopyServer;

public abstract class ProjectCopyServerOrleansTestBase<TStartupModule> : 
    ProjectCopyServerTestBase<TStartupModule> where TStartupModule : IAbpModule
{

    protected readonly TestCluster Cluster;
    
    public ProjectCopyServerOrleansTestBase(ITestOutputHelper output) : base(output)
    {
        Cluster = GetRequiredService<ClusterFixture>().Cluster;
    }
}