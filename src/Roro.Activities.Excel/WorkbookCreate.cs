using System;

namespace Roro.Activities.Excel
{
    public class WorkbookCreate : ProcessNodeActivity
    {
        public Output<Text> WorkbookName { get; set; }

        public override void Execute(ActivityContext context)
        {
            var wbName = ExcelBot.Shared.GetApp().Workbooks.Add().Name.ToString();

            context.Set(this.WorkbookName, wbName);
        }
    }
}
