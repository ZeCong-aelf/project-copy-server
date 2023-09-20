using Xunit.Abstractions;

namespace ProjectCopyServer;

public abstract class ProjectCopyServerDomainTestBase : ProjectCopyServerTestBase<ProjectCopyServerDomainTestModule>
{
    protected ProjectCopyServerDomainTestBase(ITestOutputHelper output) : base(output)
    {
    }
}