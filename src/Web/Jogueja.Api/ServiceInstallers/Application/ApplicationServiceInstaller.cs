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
                config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            })
            .AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);
}
