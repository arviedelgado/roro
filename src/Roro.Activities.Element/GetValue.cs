using System;

namespace Roro.Activities.Element
{
    public sealed class GetValue : Activity
    {
        public InArgument<bool> Element { get; set; }

        public OutArgument<string> Value { get; set; }

        public override void Execute(ActivityContext context)
        {
            throw new NotImplementedException();
        }
    }
}
