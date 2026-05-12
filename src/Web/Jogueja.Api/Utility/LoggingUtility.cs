using Core.Shared.Extensions;
using Serilog;

namespace Jogueja.Api.Utility
{
    internal static class LoggingUtility
    {
        internal static void Run(Action startupAction)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();

            Log.Information("Starting up.");

            FunctionalExtensions.TryCatchFinally(
                startupAction,
                exception => Log.Fatal(exception, "Unhandled exception."),
                () =>
                {
                    Log.Information("Shutting down.");
                    Log.CloseAndFlush();
                });
        }
    }
}
