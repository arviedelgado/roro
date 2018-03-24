using System;

namespace Roro.Activities.Excel
{
    public class WorksheetDelete : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public override void Execute(ActivityContext context)
        {
            var wbName = context.Get(this.WorkbookName);
            var wsName = context.Get(this.WorksheetName);

            ExcelBot.Shared.GetWorksheetByName(wbName, wsName, true).Delete();
        }
    }
}
