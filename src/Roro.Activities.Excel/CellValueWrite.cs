using System;

namespace Roro.Activities.Excel
{
    public class CellValueWrite : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public Input<Text> Cell { get; set; }

        public Input<Text> Value { get; set; }

        public override void Execute(ActivityContext context)
        {
            var wbName = context.Get(this.WorkbookName);
            var wsName = context.Get(this.WorksheetName);
            var range = context.Get(this.Cell);
            var value = context.Get(this.Value);

            ExcelBot.Shared.GetRange(wbName, wsName, range).Value = value;
        }
    }
}
