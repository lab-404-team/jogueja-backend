using Jogueja.Api.Middlewares;

namespace Jogueja.Api.Extensions;

public static class MiddlewareExtensions
{
    public static IServiceCollection AddMiddlewares(this IServiceCollection services)
        => services.Configure<ContentValidationOptions>(options =>
           {
               options.ContentValidation = @"""event"":{""type"":""test""";
               options.Path = "/webhooks/";
           });

    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
        => app.UseMiddleware<ContentValidationMiddleware>();
}
