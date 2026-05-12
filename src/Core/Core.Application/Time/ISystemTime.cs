namespace Core.Application.Time
{
    public interface ISystemTime
    {
        DateTime UtcNow { get; }
    }
}
