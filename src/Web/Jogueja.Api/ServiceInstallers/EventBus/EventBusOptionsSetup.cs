using Jogueja.Api.ServiceInstallers.EventBus.Options;
using Microsoft.Extensions.Options;

namespace Jogueja.Api.ServiceInstallers.EventBus
{
    internal sealed class EventBusOptionsSetup(IConfiguration configuration) : IConfigureOptions<EventBusOptions>
    {
        private const string ConfigurationSectionName = "EventBusOptions";

        public void Configure(EventBusOptions options) => configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
