using Roro.Activities;
using System;
using System.Collections.Generic;

namespace Roro.Workflow
{
    public sealed class EndNodeActivity : Activity
    {
        public override List<OutArgument> Outputs => new List<OutArgument>();

        public override void Execute(Context context)
        {
            throw new NotImplementedException();
        }

        public EndNodeActivity()
        {
            this.Outputs.Add(new OutArgument<int>());
        }
    }

}
