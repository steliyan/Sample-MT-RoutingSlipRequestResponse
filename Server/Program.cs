using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Server.Activities;
using Server.Consumers;

namespace Server
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            var builder = new HostBuilder();

            builder
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddMassTransit(cfg =>
                    {
                        cfg.AddConsumer<OrderPlacedConsumer>(typeof(OrderPlacedConsumerDefinition));
                        cfg.AddActivity<ProcessOrderActivity, ProcessOrderArgs, ProcessOrderLog>();
                        cfg.AddExecuteActivity<FullfilOrderActivity, FullfilOrderArgs>();

                        cfg.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg => cfg.ConfigureEndpoints(provider)));
                    });

                    services.AddSingleton<IHostedService, MassTransitService>();
                })
                .ConfigureLogging((ctx, logging) => logging.AddSerilog(dispose: true));

            var host = builder.Build();

            await host.RunAsync();
        }
    }
}
