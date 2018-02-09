using System;

namespace Roro.Activities.Excel
{
    public class WorkbookCreate : ProcessNodeActivity
    {
        public Output<Text> WorkbookName { get; set; }

        public override void Execute(ActivityContext context)
        {
            // inputs
            //

            var workbookName = ExcelBot.Shared.GetInstance().Workbooks.Add().Name.ToString();

            // outputs
            context.Set(this.WorkbookName, workbookName);
        }
    }
}
