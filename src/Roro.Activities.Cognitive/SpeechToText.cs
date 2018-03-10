namespace Roro.Activities.Cognitive
{
    public class SpeechToText : ProcessNodeActivity
    {
        public Output<Text> Text { get; set; }

        public override void Execute(ActivityContext context)
        {
            var text = AIBot.SpeechToText();

            context.Set(this.Text, text);
        }
    }
}
