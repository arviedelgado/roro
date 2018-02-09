using System;

namespace Roro.Activities.Excel
{
    public class WorksheetCount : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public Output<Number> Count { get; set; }

        public override void Execute(ActivityContext context)
        {
            throw new NotImplementedException();
        }
    }
}
