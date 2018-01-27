using System;

namespace Roro.Activities.Excel
{
    public class CloseWorkbook : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<TrueFalse> SaveChanges { get; set; }

        public override void Execute(ActivityContext context)
        {
            // inputs
            var workbookName = (string)context.Get(this.WorkbookName);
            var saveChanges = (bool)context.Get(this.SaveChanges);

            var xlApp = ExcelBot.Shared.GetInstance();
            var xlWb = ExcelBot.Shared.GetWorkbook(xlApp, workbookName);
            xlWb.Close(saveChanges);

            if (xlApp.Workbooks.Count == 0)
            {
                ExcelBot.Shared.Dispose();
            }

            // outputs
            //
        }
    }
}
