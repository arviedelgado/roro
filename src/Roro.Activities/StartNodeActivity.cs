using System;
using System.Collections.Generic;

namespace Roro.Activities
{
    public sealed class StartNodeActivity : Activity
    {
        public override List<Input> Inputs { get; protected set; }

        public override void Execute(ActivityContext context)
        {
            throw new NotImplementedException();
        }

        public StartNodeActivity()
        {
            this.Inputs = new List<Input>();
            this.Inputs.Add(new Input<Number>("My Custom Input"));
        }
    }
}
