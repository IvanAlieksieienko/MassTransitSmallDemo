using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using OrderService.Contracts;

namespace OrderService.Consumers
{
    public class OrderPlacedConsumer :
        IConsumer<OrderPlacedEvent>
    {
        readonly ILogger<OrderPlacedConsumer> _logger;

        public OrderPlacedConsumer(ILogger<OrderPlacedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderPlacedEvent> context)
        {
            _logger.LogInformation("Received Id: {Id} with Text: {Text}", context.Message.Id, context.Message.Value);
            return Task.CompletedTask;
        }
    }
}