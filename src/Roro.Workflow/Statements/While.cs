using System;

namespace Roro.Workflow.Statements
{
    public sealed class While : Step
    {
        public Condition Condition { get; }

        public override StepExecutionResult Execute(StepExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
