using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Server
{
    public class MassTransitService : IHostedService
    {
        private readonly IBusControl bus;

        public MassTransitService(IBusControl bus)
        {
            this.bus = bus;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await this.bus.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return this.bus.StopAsync(cancellationToken);
        }
    }
}
