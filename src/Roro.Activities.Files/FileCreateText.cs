using System;
using System.IO;

namespace Roro.Activities.Files
{
    public class FileCreateText : ProcessNodeActivity
    {
        public Input<Text> FilePath { get; set; }

        public override void Execute(ActivityContext context)
        {
            var filePath = context.Get(this.FilePath);

            File.CreateText(filePath).Close();
        }
    }
}
