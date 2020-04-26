using System;
using System.Threading.Tasks;
using Contracts;
using MassTransit;
using MassTransit.Courier;

namespace Server.Consumers
{
    public class OrderPlacedConsumer : RoutingSlipRequestProxy2<OrderPlaced>
    {
        protected override async Task BuildRoutingSlip(RoutingSlipBuilder builder, ConsumeContext<OrderPlaced> request)
        {
            // wait for some stuff...
            await Task.Delay(1000);

            builder.SetVariables(request.Message);

            builder.AddActivity("ProcessOrder", new Uri("exchange:ProcessOrder_execute"));
            builder.AddActivity("FullfilOrder", new Uri("exchange:FullfilOrder_execute"));
        }
    }
}
