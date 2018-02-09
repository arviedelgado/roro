using System;

namespace Roro.Activities.Excel
{
    public class WorkbookSaveAs : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<Text> ToWorkbookPath { get; set; }

        public override void Execute(ActivityContext context)
        {
            throw new NotImplementedException();
        }
    }
}
