using Xunit.Abstractions;

namespace ProjectCopyServer;

public abstract class ProjectCopyServerApplicationTestBase : ProjectCopyServerTestBase<ProjectCopyServerApplicationTestModule>
{
    protected ProjectCopyServerApplicationTestBase(ITestOutputHelper output) : base(output)
    {
    }
}