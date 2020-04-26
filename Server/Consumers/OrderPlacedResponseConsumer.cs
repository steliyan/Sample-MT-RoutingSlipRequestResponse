using Contracts;
using MassTransit;
using MassTransit.Courier;
using MassTransit.Courier.Contracts;

namespace Server.Consumers
{
    public class OrderPlacedResponseConsumer : RoutingSlipResponseProxy<OrderPlaced, OrderCompleted>
    {
        protected override OrderCompleted CreateResponseMessage(ConsumeContext<RoutingSlipCompleted> context, OrderPlaced request)
        {
            var response = context.Init<OrderCompleted>(new { request.Id, Data = "We've successfully processed your request." }).Result;
            return response;
        }
    }
}
