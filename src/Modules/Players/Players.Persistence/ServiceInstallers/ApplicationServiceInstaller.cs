using Core.Application.Behaviors;
using Core.Infrastructure.Configuration;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Players.Persistence.ServiceInstallers;

internal sealed class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Players.Application.AssemblyReference.Assembly);
                config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            })
            .AddValidatorsFromAssembly(Players.Application.AssemblyReference.Assembly, includeInternalTypes: true);
}
