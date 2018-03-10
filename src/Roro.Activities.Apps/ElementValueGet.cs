using System;
using System.Linq;

namespace Roro.Activities.Apps
{
    public sealed class ElementValueGet : ProcessNodeActivity
    {
        public Input<ElementQuery> Element { get; set; }

        public Output<Text> Value { get; set; }

        public override void Execute(ActivityContext context)
        {
            var e = context.GetElement(this.Element);

            e.Focus();

            context.Set(this.Value, e.Value);

        }
    }
}
