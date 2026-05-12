using MassTransit;
using Microsoft.Extensions.Options;

namespace Jogueja.Api.ServiceInstallers.EventBus
{
    internal sealed class MassTransitHostOptionsSetup(IConfiguration configuration) : IConfigureOptions<MassTransitHostOptions>
    {
        private const string ConfigurationSectionName = "MassTransitHostOptions";

        public void Configure(MassTransitHostOptions options) => configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
