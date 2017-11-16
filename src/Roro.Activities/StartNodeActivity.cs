using System;
using System.Collections.Generic;

namespace Roro.Activities
{
    public sealed class StartNodeActivity : Activity
    {
        public override List<InArgument> Inputs { get; protected set; }

        public override void Execute(Context context)
        {
            throw new NotImplementedException();
        }

        public StartNodeActivity()
        {
            this.Inputs = new List<InArgument>();
            this.Inputs.Add(new InArgument<int>("My Custom Input"));
        }
    }
}
