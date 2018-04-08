using System;
using System.IO;

namespace Roro.Activities.Files
{
    public class FolderExists : DecisionNodeActivity
    {
        public Input<Text> FolderPath { get; set; }

        public override bool Execute(ActivityContext context)
        {
            var folderPath = context.Get(this.FolderPath);
            return Directory.Exists(folderPath);
        }
    }
}
