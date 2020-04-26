using System.Threading.Tasks;
using MassTransit.Courier;

namespace Server.Activities
{
    public interface ProcessOrderArgs { }

    public interface ProcessOrderLog { }

    public class ProcessOrderActivity : IActivity<ProcessOrderArgs, ProcessOrderLog>
    {
        public async Task<CompensationResult> Compensate(CompensateContext<ProcessOrderLog> context)
        {
            return context.Compensated();
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<ProcessOrderArgs> context)
        {
            return context.Completed();
        }
    }
}
