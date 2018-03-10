namespace Roro.Activities.Cognitive
{
    public class TextToSpeech : ProcessNodeActivity
    {
        public Input<Text> Text { get; set; }

        public override void Execute(ActivityContext context)
        {
            var text = context.Get(this.Text);

            AIBot.TextToSpeech(text);
        }
    }
}
