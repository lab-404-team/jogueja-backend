using CorrelationId.Abstractions;
using MassTransit;

namespace Jogueja.Api.ServiceInstallers.EventBus.PipeFilters
{
    public class TraceIdentifierFilter<T>(ICorrelationContextAccessor correlationContextAccessor) : IFilter<PublishContext<T>>
        where T : class
    {
        public Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
        {
            if (Guid.TryParse(correlationContextAccessor.CorrelationContext?.CorrelationId, out var correlationId))
                context.CorrelationId = correlationId;

            return next.Send(context);
        }

        public void Probe(ProbeContext context) => context.CreateFilterScope("Trace Identifier");
    }
}
