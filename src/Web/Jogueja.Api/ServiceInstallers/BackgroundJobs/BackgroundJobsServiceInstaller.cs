using Core.Application.Services;
using Core.Infrastructure.Configuration;
using Hangfire;
using Hangfire.MySql;
using Newtonsoft.Json;
using System.Transactions;

namespace Jogueja.Api.ServiceInstallers.BackgroundJobs;

internal sealed class BackgroundJobsServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(x =>
        {
            x.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
             .UseSimpleAssemblyNameTypeSerializer()
             .UseRecommendedSerializerSettings()
             .UseSerializerSettings(new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
             .UseStorage(new MySqlStorage(
                 configuration.GetConnectionString("Hangfire"),
                 new MySqlStorageOptions
                 {
                     TransactionIsolationLevel = IsolationLevel.ReadCommitted,
                     QueuePollInterval = TimeSpan.FromSeconds(15),
                     JobExpirationCheckInterval = TimeSpan.FromHours(12),
                     CountersAggregateInterval = TimeSpan.FromMinutes(5),
                     PrepareSchemaIfNecessary = true,
                     DashboardJobListLimit = 50000,
                     TransactionTimeout = TimeSpan.FromMinutes(1),
                     TablesPrefix = "Hangfire"
                 }));

            var jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            x.UseSerializerSettings(jsonSettings);
        });

        services.AddHangfireServer(options =>
        {
            options.WorkerCount = Math.Min(Environment.ProcessorCount * Convert.ToUInt16(3), Convert.ToUInt16(10));
        });

        services.AddScoped<IJobSchedulerService, JobSchedulerService>();
        services.AddScoped<MediatorHangfireBridge>();
    }
}
