using System;
using System.IO;

namespace Roro.Activities.Files
{
    public class FolderCreate : ProcessNodeActivity
    {
        public Input<Text> FolderPath { get; set; }

        public override void Execute(ActivityContext context)
        {
            var folderPath = context.Get(this.FolderPath);
            Directory.CreateDirectory(folderPath);
        }
    }
}
