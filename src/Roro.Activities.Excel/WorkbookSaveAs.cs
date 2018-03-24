using System;

namespace Roro.Activities.Excel
{
    public class WorkbookSaveAs : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<Text> WorkbookNameAs { get; set; }

        public override void Execute(ActivityContext context)
        {
            var wbName = context.Get(this.WorkbookName);
            var wbNameAs = context.Get(this.WorkbookNameAs);

            ExcelBot.Shared.GetWorkbookByName(wbName, true).SaveAs(wbNameAs);
        }
    }
}
