using Masking.Serilog;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace Jogueja.Api.Extensions
{
    internal static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureLogging(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Host.UseSerilog((context, services, logger) =>
            {
                logger
                    .Destructure.ByMaskingProperties("Password")
                    .Enrich.WithCorrelationId()
                    .Enrich.WithRequestUserId()
                    .Enrich.WithMachineName()
                    .Enrich.WithEnvironmentName()
                    .Enrich.WithThreadId()
                    .Enrich.WithThreadName()
                    .Enrich.WithExceptionDetails()
                    .Enrich.WithProperty("ApplicationName", context.HostingEnvironment.ApplicationName)
                    .Enrich.FromLogContext()
                    .MinimumLevel.Override("MassTransit", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .ReadFrom.Configuration(configuration);
            });

            return builder.Host;
        }

        public static IHostBuilder ConfigureServiceProvider(this IHostBuilder builder) =>
            builder.UseDefaultServiceProvider((context, provider) =>
                provider.ValidateScopes =
                provider.ValidateOnBuild =
                context.HostingEnvironment.IsDevelopment());

        public static IHostBuilder ConfigureAppConfiguration(this IHostBuilder builder) =>
            builder.ConfigureAppConfiguration(configuration =>
                configuration
                    .AddUserSecrets<Program>()
                    .AddEnvironmentVariables());
    }
}
