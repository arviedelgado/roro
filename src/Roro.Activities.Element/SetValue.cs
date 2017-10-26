using System;

namespace Roro.Activities.Element
{
    public sealed class SetValue : Activity
    {
        public InArgument<bool> Element { get; set; }

        public InArgument<string> Value { get; set; }

        public override void Execute(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
