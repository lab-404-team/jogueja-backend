using Common.HealthChecks.Memory;
using CorrelationId.DependencyInjection;

namespace Jogueja.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    internal const string LoggingScopeKey = "CorrelationId";

    internal static IServiceCollection AddCorrelationId(this IServiceCollection services)
        => services.AddDefaultCorrelationId(options =>
        {
            options.RequestHeader =
                options.ResponseHeader =
                    options.LoggingScopeKey = LoggingScopeKey;

            options.UpdateTraceIdentifier =
                options.AddToLoggingScope = true;
        });

    internal static void AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        => services.AddHealthChecks()
            .AddMemory(
                name: "Memory",
                tags: ["memory"]);
}
