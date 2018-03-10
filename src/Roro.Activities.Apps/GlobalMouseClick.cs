using System;
using System.Linq;

namespace Roro.Activities.Apps
{
    public sealed class GlobalMouseClick : ProcessNodeActivity
    {
        public Input<ElementQuery> Element { get; set; }

        public override void Execute(ActivityContext context)
        {
            var e = context.GetElement(this.Element);

            using (var input = new InputDriver())
            {
                e.Focus();
                var p = e.Bounds.Center;
                input.MouseMove(p.X, p.Y);
                input.Click(MouseButton.Left);
            }
        }
    }
}
