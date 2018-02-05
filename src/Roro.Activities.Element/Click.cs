using System;

namespace Roro.Activities.Element
{
    public sealed class Click : ProcessNodeActivity
    {
        public Input<ElementQuery> Element { get; set; }

        public Input<Number> X { get; set; }

        public Input<Number> Y { get; set; }

        public override void Execute(ActivityContext context)
        {
            var x = context.Get(this.X);
            var y = context.Get(this.Y);
            using (var input = new InputDriver())
            {
                input.MouseMove(x, y);
                input.Click(MouseButton.Left);
            }
        }
    }
}
