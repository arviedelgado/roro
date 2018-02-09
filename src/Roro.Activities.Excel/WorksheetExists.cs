using System;

namespace Roro.Activities.Excel
{
    public class WorksheetExists : DecisionNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public override bool Execute(ActivityContext context)
        {
            throw new NotImplementedException();
        }
    }
}
