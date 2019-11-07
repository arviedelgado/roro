using System;

namespace Roro.Workflow.Statements
{
    public sealed class Action : Step
    {
        public string ActionType { get; set; }

        public override StepExecutionResult Execute(StepExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
