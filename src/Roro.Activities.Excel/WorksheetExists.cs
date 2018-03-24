using System;

namespace Roro.Activities.Excel
{
    public class WorksheetExists : DecisionNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public override bool Execute(ActivityContext context)
        {
            var wbName = context.Get(this.WorkbookName);
            var wsName = context.Get(this.WorksheetName);

            return ExcelBot.Shared.GetWorksheetByName(wbName, wsName, false) != null;
        }
    }
}
