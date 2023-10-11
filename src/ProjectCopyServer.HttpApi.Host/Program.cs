using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace ProjectCopyServer
{
    public class Program
    {
        public async static Task<int> Main(string[] args)
        {
            System.Threading.ThreadPool.SetMinThreads(300, 300);
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                Log.Information("Starting ProjectCopyServer.HttpApi.Host");

                var builder = WebApplication.CreateBuilder(args);
                builder.Host.AddAppSettingsSecretsJson()
                    .UseAutofac()
                    .UseSerilog();
                // builder.Services.AddSignalR();
                await builder.AddApplicationAsync<ProjectCopyServerHttpApiHostModule>();
                var app = builder.Build();
                await app.InitializeApplicationAsync();
                await app.RunAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}