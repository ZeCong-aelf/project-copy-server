using AElf.Indexing.Elasticsearch.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Volo.Abp;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;
using Orleans.Providers.MongoDB.Configuration;
using ProjectCopyServer.EntityEventHandler;
using ProjectCopyServer.EntityEventHandler.Core;
using ProjectCopyServer.Grains;
using ProjectCopyServer.MongoDB;
using Volo.Abp.OpenIddict.Tokens;

namespace ProjectCopyServer;

[DependsOn(typeof(AbpAutofacModule),
    typeof(ProjectCopyServerMongoDbModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(ProjectCopyServerEntityEventHandlerCoreModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpEventBusRabbitMqModule)
    )]
public class ProjectCopyServerEntityEventHandlerModule : AbpModule
{
  public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureTokenCleanupService();
        var configuration = context.Services.GetConfiguration();
        context.Services.AddHostedService<ProjectCopyServerHostedService>();
        context.Services.AddSingleton<IClusterClient>(o =>
        {
            return new ClientBuilder()
                .ConfigureDefaults()
                .UseMongoDBClient(configuration["Orleans:MongoDBClient"])
                .UseMongoDBClustering(options =>
                {
                    options.DatabaseName = configuration["Orleans:DataBase"];;
                    options.Strategy = MongoDBMembershipStrategy.SingleDocument;
                })
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = configuration["Orleans:ClusterId"];
                    options.ServiceId = configuration["Orleans:ServiceId"];
                })
                .ConfigureApplicationParts(parts =>
                    parts.AddApplicationPart(typeof(ProjectCopyServerGrainsModule).Assembly).WithReferences())
                //.AddSimpleMessageStreamProvider(AElfIndexerApplicationConsts.MessageStreamName)
                .ConfigureLogging(builder => builder.AddProvider(o.GetService<ILoggerProvider>()))
                .Build();
        });
        ConfigureEsIndexCreation();
    }
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var client = context.ServiceProvider.GetRequiredService<IClusterClient>();
        AsyncHelper.RunSync(async ()=> await client.Connect());
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        var client = context.ServiceProvider.GetRequiredService<IClusterClient>();
        AsyncHelper.RunSync(client.Close);
    }

    //Create the ElasticSearch Index based on Domain Entity
    private void ConfigureEsIndexCreation()
    {
        Configure<IndexCreateOption>(x => { x.AddModule(typeof(ProjectCopyServerDomainModule)); });
    }
    
    //Disable TokenCleanupService
    private void ConfigureTokenCleanupService()
    {
        Configure<TokenCleanupOptions>(x => x.IsCleanupEnabled = false);
    }
}