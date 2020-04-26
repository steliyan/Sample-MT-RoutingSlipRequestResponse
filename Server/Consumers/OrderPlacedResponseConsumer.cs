using System;
using System.Threading.Tasks;
using Contracts;
using MassTransit;
using MassTransit.Courier;
using MassTransit.Courier.Contracts;

namespace Server.Consumers
{
    public class OrderPlacedResponseConsumer : RoutingSlipResponseProxy2<OrderPlaced, OrderCompleted, OrderFaulted>
    {
        protected override async Task<OrderFaulted> CreateFaultedResponseMessage(ConsumeContext<RoutingSlipFaulted> context, OrderPlaced request, Guid requestId)
        {
            var response = await context.Init<OrderFaulted>(new { request.Id, Reason = context.Message.ActivityExceptions[0].ExceptionInfo.Message });
            return response;
        }

        protected override async Task<OrderCompleted> CreateResponseMessage(ConsumeContext<RoutingSlipCompleted> context, OrderPlaced request)
        {
            var response = await context.Init<OrderCompleted>(new { request.Id, Data = "We've successfully processed your request." });
            return response;
        }
    }
}
