using System;

namespace Roro.Activities.Excel
{
    public class WorkbookCloseAll : ProcessNodeActivity
    {
        public override void Execute(ActivityContext context)
        {
            var count = ExcelBot.Shared.GetApp().Workbooks.Count;
            for (var i = 0; i < count; i++)
            {
                ExcelBot.Shared.GetApp().Workbooks.Item(i).Close();
            }

            if (ExcelBot.Shared.GetApp().Workbooks.Count == 0)
            {
                ExcelBot.Shared.Dispose();
            }
        }
    }
}
