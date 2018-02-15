using System;

namespace Roro.Activities.Excel
{
    public class CellValueCopy : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public Input<Text> CellAddress { get; set; }

        public override void Execute(ActivityContext context)
        {
            throw new NotImplementedException();
        }
    }
}
