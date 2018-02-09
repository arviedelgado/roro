using System;

namespace Roro.Activities.Excel
{
    public class WorksheetCreate : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public override void Execute(ActivityContext context)
        {
            throw new NotImplementedException();
        }
    }
}
