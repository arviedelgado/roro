using System;
using System.IO;

namespace Roro.Activities.Files
{
    public class FileMove : ProcessNodeActivity
    {
        public Input<Text> FromFilePath { get; set; }

        public Input<Text> ToFilePath { get; set; }

        public override void Execute(ActivityContext context)
        {
            var fromFilePath = context.Get(this.FromFilePath);
            var toFilePath = context.Get(this.ToFilePath);

            File.Move(fromFilePath, toFilePath);
        }
    }
}
