using ProjectCopyServer.MongoDB;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace ProjectCopyServer.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ProjectCopyServerMongoDbModule),
    typeof(ProjectCopyServerApplicationContractsModule)
    )]
public class ProjectCopyServerDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
