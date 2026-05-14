using Core.Application;
using Core.Application.EventStore;
using Core.Infrastructure.Configuration;
using Core.Persistence;
using Core.Persistence.EventStore;
using Core.Persistence.Projection;
using Core.Persistence.Projection.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Players.Application.Players.Queries.GetById;

namespace Players.Persistence.ServiceInstallers;

internal sealed class PersistenceServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default")!;

        services.AddDbContext<PlayerDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddScoped<IUnitOfWork<PlayerDbContext>, UnitOfWork<PlayerDbContext>>();
        services.AddTransient<IEventStore<PlayerDbContext>, EventStore<PlayerDbContext>>();

        services.TryAddSingleton<IMongoDbContext>(_ =>
            new ProjectionDbContext(
                configuration.GetConnectionString("Projection")!,
                "jogueja"));

        services.AddTransient<Core.Domain.Projection.IProjection<PlayerReadModel>>(sp =>
            new Projection<PlayerReadModel>(sp.GetRequiredService<IMongoDbContext>()));
    }
}
