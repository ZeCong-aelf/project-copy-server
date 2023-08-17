using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace ProjectCopyServer.ContractEventHandler.Core
{
    [DependsOn(
        typeof(AbpAutoMapperModule)
    )]
    public class ProjectCopyServerContractEventHandlerCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<ProjectCopyServerContractEventHandlerCoreModule>();
            });
        }
    }
}