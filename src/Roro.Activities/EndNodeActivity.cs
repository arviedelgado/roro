using System;
using System.Collections.Generic;

namespace Roro.Activities
{
    public sealed class EndNodeActivity : Activity
    {
        public override List<Output> Outputs { get; protected set; }

        public override void Execute(ActivityContext context)
        {
            foreach (var output in this.Outputs)
            {

            }
        }

        public EndNodeActivity()
        {
            this.Outputs = new List<Output>();
            this.Outputs.Add(new Output<Number>("My Custom Output"));
        }
    }

}
