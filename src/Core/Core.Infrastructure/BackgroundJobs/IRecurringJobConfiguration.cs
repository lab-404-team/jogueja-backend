namespace Core.Infrastructure.BackgroundJobs
{
    public interface IRecurringJobConfiguration
    {
        string Name { get; }
        Type Type { get; }
        int IntervalInSeconds { get; }
    }
}
