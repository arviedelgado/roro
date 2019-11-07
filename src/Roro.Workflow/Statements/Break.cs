using System;

namespace Roro.Workflow.Statements
{
    public sealed class Break : Step
    {
        public override StepExecutionResult Execute(StepExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
