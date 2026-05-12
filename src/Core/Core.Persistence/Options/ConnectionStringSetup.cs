using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Core.Persistence.Options
{
    internal sealed class ConnectionStringSetup(IConfiguration configuration) : IConfigureOptions<ConnectionStringOptions>
    {
        private const string ConnectionStringName = "Default";

        public void Configure(ConnectionStringOptions options) => options.Value = configuration.GetConnectionString(ConnectionStringName);
    }
}
