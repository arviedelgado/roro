using System;

namespace Roro.Activities.Excel
{
    public class WorksheetCount : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Output<Number> Count { get; set; }

        public override void Execute(ActivityContext context)
        {
            var wbName = context.Get(this.WorkbookName);

            var count = ExcelBot.Shared.GetWorkbookByName(wbName, true).Worksheets.Count;

            context.Set(this.Count, count);
        }
    }
}
