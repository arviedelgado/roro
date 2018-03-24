using System;

namespace Roro.Activities.Excel
{
    public class CellValueClear : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public Input<Text> Cell { get; set; }

        public Input<TrueFalse> ClearContentsOnly { get; set; }

        public override void Execute(ActivityContext context)
        {
            var wbName = context.Get(this.WorkbookName);
            var wsName = context.Get(this.WorksheetName);
            var range = context.Get(this.Cell);
            var clearContentsOnly = context.Get(this.ClearContentsOnly, false);

            if (clearContentsOnly)
            {
                ExcelBot.Shared.GetRange(wbName, wsName, range).ClearContents();
            }
            else
            {
                ExcelBot.Shared.GetRange(wbName, wsName, range).Clear();
            }
        }
    }
}
