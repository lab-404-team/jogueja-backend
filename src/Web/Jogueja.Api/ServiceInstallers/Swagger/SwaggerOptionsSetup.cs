using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Jogueja.Api.ServiceInstallers.Swagger
{
    internal sealed class SwaggerOptionsSetup : IConfigureOptions<SwaggerOptions>
    {
        public void Configure(SwaggerOptions options)
        {
            options.RouteTemplate = "swagger/{documentName}/swagger.json";
        }
    }
}
