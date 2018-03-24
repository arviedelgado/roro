using System;

namespace Roro.Activities.Excel
{
    public class WorkbookOpen : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public override void Execute(ActivityContext context)
        {
            var wbName = context.Get(this.WorkbookName);

            ExcelBot.Shared.GetApp().Workbooks.Open(wbName);

            if (ExcelBot.Shared.GetApp().Workbooks.Count == 0)
            {
                ExcelBot.Shared.Dispose();
            }
        }
    }
}
