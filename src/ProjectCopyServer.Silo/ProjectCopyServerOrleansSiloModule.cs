using Microsoft.Extensions.DependencyInjection;
using ProjectCopyServer.Grains;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace ProjectCopyServer.Silo;
[DependsOn(typeof(AbpAutofacModule),
    typeof(ProjectCopyServerGrainsModule),
    typeof(AbpAspNetCoreSerilogModule)
)]
public class ProjectCopyServerOrleansSiloModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHostedService<ProjectCopyServerHostedService>();
    }
}