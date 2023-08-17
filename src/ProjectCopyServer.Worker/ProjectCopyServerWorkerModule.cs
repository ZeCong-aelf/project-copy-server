using Volo.Abp;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Modularity;

namespace ProjectCopyServer.Worker
{
    [DependsOn(typeof(AbpBackgroundWorkersQuartzModule),
        typeof(AbpBackgroundWorkersModule))]
    public class ProjectCopyServerWorkerModule : AbpModule
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            context.AddBackgroundWorkerAsync<CollectionCreationWorker>();
        }
    }
}