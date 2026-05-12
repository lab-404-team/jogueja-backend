using Core.Infrastructure.Configuration;

namespace Jogueja.Api.ServiceInstallers.Swagger
{
    internal sealed class SwaggerServiceInstaller : IServiceInstaller
    {
        void IServiceInstaller.Install(IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureOptions<SwaggerOptionsSetup>();
            services.ConfigureOptions<SwaggerGenOptionsSetup>();
            services.ConfigureOptions<SwaggerUiOptionsSetup>();

            services.AddSwaggerGen();
        }
    }
}
