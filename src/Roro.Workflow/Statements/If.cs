using System;

namespace Roro.Workflow.Statements
{
    public sealed class If : Step
    {
        public Condition Condition { get; }

        public override StepExecutionResult Execute(StepExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
