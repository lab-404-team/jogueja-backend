using Core.Infrastructure.Configuration;
using Core.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourtOwners.Persistence;

public sealed class CourtOwnersModuleInstaller : IModuleInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .InstallServicesFromAssemblies(configuration, AssemblyReference.Assembly)
            .AddTransientAsMatchingInterfaces(CourtOwners.Application.AssemblyReference.Assembly)
            .AddTransientAsMatchingInterfaces(AssemblyReference.Assembly)
            .AddScopedAsMatchingInterfaces(CourtOwners.Application.AssemblyReference.Assembly)
            .AddScopedAsMatchingInterfaces(AssemblyReference.Assembly);
}
