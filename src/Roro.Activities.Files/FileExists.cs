using System;
using System.IO;

namespace Roro.Activities.Files
{
    public class FileExists : DecisionNodeActivity
    {
        public Input<Text> FilePath { get; set; }

        public override bool Execute(ActivityContext context)
        {
            var filePath = context.Get(this.FilePath);
            return File.Exists(filePath);
        }
    }
}
