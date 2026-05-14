using Core.Application.Behaviors;
using Core.Infrastructure.Configuration;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourtOwners.Persistence.ServiceInstallers;

internal sealed class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(CourtOwners.Application.AssemblyReference.Assembly);
                config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            })
            .AddValidatorsFromAssembly(CourtOwners.Application.AssemblyReference.Assembly, includeInternalTypes: true);
}
