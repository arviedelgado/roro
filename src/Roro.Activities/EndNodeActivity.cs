using System;
using System.Collections.Generic;

namespace Roro.Activities
{
    public sealed class EndNodeActivity : Activity
    {
        public override List<OutArgument> Outputs { get; protected set; }

        public override void Execute(Context context)
        {
            throw new NotImplementedException();
        }

        public EndNodeActivity()
        {
            this.Outputs = new List<OutArgument>();
            this.Outputs.Add(new OutArgument<int>("My Custom Output"));
        }
    }

}
