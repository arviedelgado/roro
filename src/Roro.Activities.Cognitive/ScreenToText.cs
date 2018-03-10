namespace Roro.Activities.Cognitive
{
    public class ScreenToText : ProcessNodeActivity
    {
        public Input<Number> X { get; set; }

        public Input<Number> Y { get; set; }

        public Input<Number> Width { get; set; }

        public Input<Number> Height { get; set; }

        public Output<Text> Text { get; set; }

        public override void Execute(ActivityContext context)
        {
            var x = context.Get(this.X, 0);
            var y = context.Get(this.Y, 0);
            var w = context.Get(this.Width, 0);
            var h = context.Get(this.Height, 0);
            context.Set(this.Text, AIBot.ScreenToText(x, y, w, h));
        }
    }
}
