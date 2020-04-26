using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Server.Consumers;

namespace Server.Consumers
{ 
    public class OrderPlacedConsumerDefinition : ConsumerDefinition<OrderPlacedConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<OrderPlacedConsumer> consumerConfigurator)
        {
            endpointConfigurator.Instance(new OrderPlacedResponseConsumer());
        }
    }
}
