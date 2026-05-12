using Core.Infrastructure.Configuration;
using Core.Infrastructure.EventBus;
using Core.Shared.Extensions;
using MassTransit;
using System.Reflection;

namespace Core.Infrastructure.Extensions
{
    public static class RegistrationConfiguratorExtensions
    {
        public static void AddConsumersFromAssemblies(this IRegistrationConfigurator registrationConfigurator, params Assembly[] assemblies) =>
            InstanceFactory
                .CreateFromAssemblies<IConsumerConfiguration>(assemblies)
                .ForEach(consumerInstaller => consumerInstaller.AddConsumers(registrationConfigurator));

        public static void AddRequestClientsFromAssemblies(this IRegistrationConfigurator registrationConfigurator, params Assembly[] assemblies) =>
            InstanceFactory
                .CreateFromAssemblies<IRequestClientConfiguration>(assemblies)
                .ForEach(consumerInstaller => consumerInstaller.AddRequestClients(registrationConfigurator));

        public static void AddEventReceiveEndpointsFromAssemblies(this IRabbitMqBusFactoryConfigurator rabbitMqBusFactoryConfigurator, IRegistrationContext registrationContext, params Assembly[] assemblies) =>
            InstanceFactory
                .CreateFromAssemblies<IEventReceiveEndpointConfiguration>(assemblies)
                .ForEach(receiveEndpointsInstaller => receiveEndpointsInstaller.AddEventReceiveEndpoints(rabbitMqBusFactoryConfigurator, registrationContext));
    }
}
