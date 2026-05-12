using System.Collections.Concurrent;

namespace Core.Application.Extensions;

public static class KeyedSemaphoreExtensions
{
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new();

    public static async Task<IAsyncDisposable> LockAsync(string key, CancellationToken ct)
    {
        var sem = _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
        await sem.WaitAsync(ct);
        return new Releaser(key, sem);
    }

    private sealed class Releaser(string key, SemaphoreSlim sem) : IAsyncDisposable
    {
        public ValueTask DisposeAsync()
        {
            sem.Release();
            if (sem.CurrentCount == 1) _ = _locks.TryRemove(key, out _);
            return ValueTask.CompletedTask;
        }
    }
}
