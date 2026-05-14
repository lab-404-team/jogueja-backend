using Microsoft.EntityFrameworkCore;
using CourtOwners.Persistence.Configurations;

namespace CourtOwners.Persistence;

public sealed class CourtOwnerDbContext(DbContextOptions<CourtOwnerDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CourtOwnerStoreEventConfiguration());
        modelBuilder.ApplyConfiguration(new CourtOwnerSnapshotConfiguration());
    }
}
