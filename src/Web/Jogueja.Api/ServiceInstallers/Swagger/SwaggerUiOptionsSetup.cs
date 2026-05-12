using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Jogueja.Api.ServiceInstallers.Swagger
{
    internal sealed class SwaggerUiOptionsSetup(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerUIOptions>
    {
        public void Configure(SwaggerUIOptions options)
        {
            options.RoutePrefix = "swagger";

            foreach (var version in provider.ApiVersionDescriptions.Select(version => version.GroupName))
            {
                options.SwaggerEndpoint($"/api/swagger/{version}/swagger.json", version);
            }

            options.DisplayRequestDuration();
            options.EnableDeepLinking();
            options.EnableFilter();
            options.EnableValidator();
            options.EnableTryItOutByDefault();
            options.DocExpansion(DocExpansion.None);
        }
    }
}
