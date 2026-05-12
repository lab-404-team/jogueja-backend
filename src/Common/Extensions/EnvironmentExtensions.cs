using Common.Helpers;
using Common.Options;
using Microsoft.Extensions.Options;

namespace Common.Extensions;

public static class EnvironmentExtensions
{
    public static DateTimeOffset GetEnvironmentScheduleTimer(
        IOptions<EnvironmentOptions> environment,
        DateTimeOffset productionDate,
        DateTimeOffset stagingDate)
        => environment.Value.EnvironmentName switch
        {
            "PRODUCTION" => productionDate,
            "STAGING" or "DEVELOPMENT" => stagingDate,
            _ => DateTimeOffset.Now.AddDays(1)
        };

    public static DateTimeOffset GetEnvironmentScheduleTimerNextBusinessDay(
        IOptions<EnvironmentOptions> environment,
        DateTimeOffset stagingDate)
        => environment.Value.EnvironmentName switch
        {
            "PRODUCTION" => DateTimeHelper.AddOneBusinessDay(DateTimeOffset.Now),
            "STAGING" or "DEVELOPMENT" => stagingDate,
            _ => DateTimeHelper.AddOneBusinessDay(DateTimeOffset.Now)
        };
}
