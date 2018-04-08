using System;
using System.IO;

namespace Roro.Activities.Files
{
    public class FolderMove : ProcessNodeActivity
    {
        public Input<Text> FromFolderPath { get; set; }

        public Input<Text> ToFolderPath { get; set; }

        public override void Execute(ActivityContext context)
        {
            var fromFolderPath = context.Get(this.FromFolderPath);
            var toFolderPath = context.Get(this.ToFolderPath);

            Directory.Move(fromFolderPath, toFolderPath);
        }
    }
}
