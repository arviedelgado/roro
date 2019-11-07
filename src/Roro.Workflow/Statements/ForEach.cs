using System;

namespace Roro.Workflow.Statements
{
    public sealed class ForEach : Step
    {
        public Condition Condition { get; }

        public override StepExecutionResult Execute(StepExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
