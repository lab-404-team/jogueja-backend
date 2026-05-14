using Core.Application.Behaviors;
using Core.Infrastructure.Configuration;
using FluentValidation;

namespace Jogueja.Api.ServiceInstallers.Application;

internal sealed class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(AssemblyReference.Assembly);
                config.RegisterServicesFromAssembly(Players.Application.AssemblyReference.Assembly);
                config.RegisterServicesFromAssembly(CourtOwners.Application.AssemblyReference.Assembly);
                config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            })
            .AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true)
            .AddValidatorsFromAssembly(Players.Application.AssemblyReference.Assembly, includeInternalTypes: true)
            .AddValidatorsFromAssembly(CourtOwners.Application.AssemblyReference.Assembly, includeInternalTypes: true);
}
