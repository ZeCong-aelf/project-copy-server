using Xunit.Abstractions;

namespace ProjectCopyServer;

public abstract class ProjectCopyServerApplicationTestBase : ProjectCopyServerOrleansTestBase<ProjectCopyServerApplicationTestModule>
{
    protected ProjectCopyServerApplicationTestBase(ITestOutputHelper output) : base(output)
    {
    }
}