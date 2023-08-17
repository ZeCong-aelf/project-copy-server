using Volo.Abp.Modularity;

namespace ProjectCopyServer;

[DependsOn(
    typeof(ProjectCopyServerTestBaseModule)
)]
public class ProjectCopyServerDomainTestModule : AbpModule
{

}