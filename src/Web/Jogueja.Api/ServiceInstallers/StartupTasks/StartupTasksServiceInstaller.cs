using Core.Infrastructure.Configuration;
using Jogueja.Api.StartupTasks;

namespace Jogueja.Api.ServiceInstallers.StartupTasks
{
    internal sealed class StartupTasksServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration) => services.AddHostedService<MigrateDatabaseStartupTask>();
    }
}
