using Roro.Core.Activities;
using System;
using System.Collections.Generic;

namespace Roro.Workflow
{
    public sealed class StartNodeActivity : Activity
    {
        public override List<InArgument> Inputs => new List<InArgument>();

        public override void Execute(ActivityContext context)
        {
            throw new NotImplementedException();
        }

        public StartNodeActivity()
        {
            this.Inputs.Add(new InArgument<int>());
        }
    }
}
