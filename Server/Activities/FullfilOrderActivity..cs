using System;
using System.Threading.Tasks;
using MassTransit.Courier;

namespace Server.Activities
{
    public interface FullfilOrderArgs
    {
        string Id { get; }
    }

    public class FullfilOrderActivity : IExecuteActivity<FullfilOrderArgs>
    {
        public async Task<ExecutionResult> Execute(ExecuteContext<FullfilOrderArgs> context)
        {
            if (context.Arguments.Id.StartsWith("error"))
            {
                return context.Faulted(new ArgumentException("Faulted because of... reasons!"));
            }

            return context.Completed();
        }
    }
}
