using System;
using System.IO;

namespace Roro.Activities.Files
{
    public class FileAppendText : ProcessNodeActivity
    {
        public Input<Text> FilePath { get; set; }

        public Input<Text> Text { get; set; }

        public override void Execute(ActivityContext context)
        {
            var filePath = context.Get(this.FilePath);

            var text = context.Get(this.Text);

            File.AppendAllText(filePath, text);
        }
    }
}
