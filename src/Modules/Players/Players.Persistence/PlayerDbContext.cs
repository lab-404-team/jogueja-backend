using Microsoft.EntityFrameworkCore;
using Players.Persistence.Configurations;

namespace Players.Persistence;

public sealed class PlayerDbContext(DbContextOptions<PlayerDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PlayerStoreEventConfiguration());
        modelBuilder.ApplyConfiguration(new PlayerSnapshotConfiguration());
    }
}
