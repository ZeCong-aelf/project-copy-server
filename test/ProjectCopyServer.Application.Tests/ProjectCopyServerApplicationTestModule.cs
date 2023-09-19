using Microsoft.Extensions.DependencyInjection;
using Moq;
using ProjectCopyServer.Grains;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.MongoDB;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Identity.MongoDB;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace ProjectCopyServer;

[DependsOn(
    typeof(ProjectCopyServerApplicationModule),
    typeof(ProjectCopyServerApplicationContractsModule),
    typeof(ProjectCopyServerOrleansTestModule),
    typeof(ProjectCopyServerDomainTestModule)
)]
public class ProjectCopyServerApplicationTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<ProjectCopyServerApplicationModule>(); });
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<ProjectCopyServerOrleansTestModule>(); });
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<ProjectCopyServerGrainsModule>(); });


        context.Services.AddSingleton(new Mock<IMongoDbContextProvider<IAuditLoggingMongoDbContext>>().Object);
        context.Services.AddSingleton<IAuditLogRepository, MongoAuditLogRepository>();
        context.Services.AddSingleton<IIdentityUserRepository, MongoIdentityUserRepository>();

    }
}