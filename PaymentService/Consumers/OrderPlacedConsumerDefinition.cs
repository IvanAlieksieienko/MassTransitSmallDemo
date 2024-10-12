using MassTransit;

namespace PaymentService.Consumers
{
    public class OrderPlacedConsumerDefinition :
        ConsumerDefinition<OrderPlacedConsumer>
    {
        public OrderPlacedConsumerDefinition()
        {
            EndpointName = "payment-service-queue";
        }
        
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<OrderPlacedConsumer> consumerConfigurator, IRegistrationContext context)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));

            endpointConfigurator.UseInMemoryOutbox(context);
        }
    }
}