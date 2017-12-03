using System;

namespace Roro.Activities.Element
{
    public sealed class Click : Activity
    {
        public InArgument<int> X { get; set; }

        public InArgument<int> Y { get; set; }

        public override void Execute(ActivityContext context)
        {
            throw new NotImplementedException();
        }
    }
}
