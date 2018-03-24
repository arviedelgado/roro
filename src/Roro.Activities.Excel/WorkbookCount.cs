using System;

namespace Roro.Activities.Excel
{
    public class WorkbookCount : ProcessNodeActivity
    {
        public Output<Number> Count { get; set; }

        public override void Execute(ActivityContext context)
        {
            var count = ExcelBot.Shared.GetApp().Workbooks.Count;

            context.Set(this.Count, count);
        }
    }
}
