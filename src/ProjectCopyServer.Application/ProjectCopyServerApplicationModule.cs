using ProjectCopyServer.Grains;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace ProjectCopyServer;

[DependsOn(
    dependedTypes: new[]
    {
        typeof(ProjectCopyServerDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(ProjectCopyServerApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(ProjectCopyServerGrainsModule),
        typeof(AbpSettingManagementApplicationModule)
    }
)]
public class ProjectCopyServerApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<ProjectCopyServerApplicationModule>(); });
    }
}
