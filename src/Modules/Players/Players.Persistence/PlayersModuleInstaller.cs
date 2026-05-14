using Core.Infrastructure.Configuration;
using Core.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Players.Persistence;

public sealed class PlayersModuleInstaller : IModuleInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .InstallServicesFromAssemblies(configuration, AssemblyReference.Assembly)
            .AddTransientAsMatchingInterfaces(Players.Application.AssemblyReference.Assembly)
            .AddTransientAsMatchingInterfaces(AssemblyReference.Assembly)
            .AddScopedAsMatchingInterfaces(Players.Application.AssemblyReference.Assembly)
            .AddScopedAsMatchingInterfaces(AssemblyReference.Assembly);
}
