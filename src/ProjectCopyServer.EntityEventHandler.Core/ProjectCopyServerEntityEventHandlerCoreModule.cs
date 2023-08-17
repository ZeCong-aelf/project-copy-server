using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace ProjectCopyServer.EntityEventHandler.Core
{
    [DependsOn(typeof(AbpAutoMapperModule),
        typeof(ProjectCopyServerApplicationModule),
        typeof(ProjectCopyServerApplicationContractsModule))]
    public class ProjectCopyServerEntityEventHandlerCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<ProjectCopyServerEntityEventHandlerCoreModule>();
            });
        }
    }
}