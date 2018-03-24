using System;

namespace Roro.Activities.Excel
{
    public class WorkbookRefreshAll : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public override void Execute(ActivityContext context)
        {
            var wbName = context.Get(this.WorkbookName);

            ExcelBot.Shared.GetWorkbookByName(wbName, true).RefreshAll();
        }
    }
}
