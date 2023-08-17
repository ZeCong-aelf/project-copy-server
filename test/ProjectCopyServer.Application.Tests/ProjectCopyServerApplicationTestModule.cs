using Volo.Abp.Modularity;

namespace ProjectCopyServer;

[DependsOn(
    typeof(ProjectCopyServerApplicationModule),
    typeof(ProjectCopyServerDomainTestModule)
)]
public class ProjectCopyServerApplicationTestModule : AbpModule
{

}