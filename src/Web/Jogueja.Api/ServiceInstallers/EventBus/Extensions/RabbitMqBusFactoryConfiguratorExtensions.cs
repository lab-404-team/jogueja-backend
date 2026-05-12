using Core.Domain.Primitives;
using Core.Infrastructure.Extensions;
using MassTransit;

namespace Jogueja.Api.ServiceInstallers.EventBus.Extensions
{
    public static class RabbitMqBusFactoryConfiguratorExtensions
    {
        public static void ConfigureEventReceiveEndpoint<TConsumer, TMessage>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext context, string module)
            where TConsumer : class, IConsumer
            where TMessage : class, IMessage
            => bus.ReceiveEndpoint(
                queueName: $"jogueja.{module}.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TMessage).ToKebabCaseString()}",
                configureEndpoint: endpoint =>
                {
                    endpoint.ConfigureConsumeTopology = false;
                    endpoint.Bind<TMessage>();
                    endpoint.ConfigureConsumer<TConsumer>(context);
                });
    }
}
