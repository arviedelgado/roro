using System;
using System.Linq;

namespace Roro.Activities.Apps
{
    public sealed class ElementValueSet : ProcessNodeActivity
    {
        public Input<ElementQuery> Element { get; set; }

        public Input<Text> Value { get; set; }

        public override void Execute(ActivityContext context)
        {
            var value = context.Get(this.Value, string.Empty);

            var e = context.GetElement(this.Element);

            e.Focus();

            e.Value = value;
        }
    }
}
