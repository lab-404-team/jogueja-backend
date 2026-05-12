using Core.Persistence.Projection.Abstractions;

namespace Core.Persistence.Projection
{
    public class ProjectionDbContext(string connectionString, string databaseName) : MongoDbContext(connectionString, databaseName) { }
}
