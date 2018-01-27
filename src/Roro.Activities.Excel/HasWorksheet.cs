using System;

namespace Roro.Activities.Excel
{
    public class HasWorksheet : DecisionNodeActivity
    {
        public Input<Text> WorkbookPath { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public override bool Execute(ActivityContext context)
        {
            throw new NotImplementedException();
        }
    }
}
