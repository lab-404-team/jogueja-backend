using Core.Application;
using Core.Application.EventStore;
using Core.Infrastructure.Configuration;
using Core.Infrastructure.Extensions;
using Core.Persistence;
using Core.Persistence.EventStore;
using Core.Persistence.Projection;
using Core.Persistence.Projection.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using CourtOwners.Application.CourtOwners;

namespace CourtOwners.Persistence;

internal sealed class CourtOwnersModuleInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default")!;

        services.AddDbContext<CourtOwnerDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddScoped<IUnitOfWork<CourtOwnerDbContext>, UnitOfWork<CourtOwnerDbContext>>();
        services.AddTransient<IEventStore<CourtOwnerDbContext>, EventStore<CourtOwnerDbContext>>();

        services.TryAddSingleton<IMongoDbContext>(_ =>
            new ProjectionDbContext(
                configuration.GetConnectionString("Projection")!,
                "jogueja"));

        services.AddTransient<Core.Domain.Projection.IProjection<CourtOwnerReadModel>>(sp =>
            new Projection<CourtOwnerReadModel>(sp.GetRequiredService<IMongoDbContext>()));

        services.AddScopedAsMatchingInterfaces(Assembly);
    }
}
