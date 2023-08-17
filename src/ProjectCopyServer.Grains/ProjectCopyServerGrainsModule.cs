using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace ProjectCopyServer.Grains;

[DependsOn(
    typeof(AbpAutoMapperModule),typeof(ProjectCopyServerApplicationContractsModule))]
public class ProjectCopyServerGrainsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<ProjectCopyServerGrainsModule>(); });
    }
}