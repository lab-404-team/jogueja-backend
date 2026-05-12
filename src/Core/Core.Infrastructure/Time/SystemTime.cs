using Core.Application.Time;

namespace Core.Infrastructure.Time
{
    public sealed class SystemTime : ISystemTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
