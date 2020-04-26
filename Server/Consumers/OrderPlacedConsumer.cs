using System;
using Contracts;
using MassTransit;
using MassTransit.Courier;

namespace Server.Consumers
{
    public class OrderPlacedConsumer : RoutingSlipRequestProxy<OrderPlaced>
    {
        protected override void BuildRoutingSlip(RoutingSlipBuilder builder, ConsumeContext<OrderPlaced> request)
        {
            builder.SetVariables(request.Message);

            builder.AddActivity("ProcessOrder", new Uri("exchange:ProcessOrder_execute"));
            builder.AddActivity("FullfilOrder", new Uri("exchange:FullfilOrder_execute"));
        }
    }
}
