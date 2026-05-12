namespace Jogueja.Api.StartupTasks
{
    internal sealed class MigrateDatabaseStartupTask(IServiceProvider serviceProvider) : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
