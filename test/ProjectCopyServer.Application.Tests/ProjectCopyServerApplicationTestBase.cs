using Xunit.Abstractions;

namespace ProjectCopyServer;

public abstract partial class ProjectCopyServerApplicationTestBase : ProjectCopyServerOrleansTestBase<ProjectCopyServerApplicationTestModule>
{

    public  readonly ITestOutputHelper Output;
    protected ProjectCopyServerApplicationTestBase(ITestOutputHelper output) : base(output)
    {
        Output = output;
    }
}