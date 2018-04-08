using System;
using System.IO;

namespace Roro.Activities.Files
{
    public class FolderInfoGet : ProcessNodeActivity
    {
        public Input<Text> FolderPath { get; set; }

        public Output<Text> FolderName { get; set; }

        public Output<Number> FolderCount { get; set; }

        public Output<Number> FileCount { get; set; }

        public Output<DateTime> CreationTime { get; set; }

        public Output<DateTime> LastAccessTime { get; set; }

        public Output<DateTime> LastWriteTime { get; set; }

        public override void Execute(ActivityContext context)
        {
            var folderPath = context.Get(this.FolderPath);

            var folderInfo = new DirectoryInfo(folderPath);

            context.Set(this.FolderName, folderInfo.Name);
            context.Set(this.FolderCount, folderInfo.GetDirectories().Length);
            context.Set(this.FileCount, folderInfo.GetFiles().Length);
            context.Set(this.CreationTime, folderInfo.CreationTime);
            context.Set(this.LastAccessTime, folderInfo.LastAccessTime);
            context.Set(this.LastWriteTime, folderInfo.LastWriteTime);
        }
    }
}
