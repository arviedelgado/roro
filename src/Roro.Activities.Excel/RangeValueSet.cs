using System;

namespace Roro.Activities.Excel
{
    public class RangeValueSet : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public Input<Text> RangeAddress { get; set; }

        public Input<Text> RangeValue { get; set; }

        public override void Execute(ActivityContext context)
        {
            // inputs
            var workbookName = (string)context.Get(this.WorkbookName);
            var worksheetName = (string)context.Get(this.WorksheetName);
            var rangeAddress = (string)context.Get(this.RangeAddress);
            var rangeValue = (string)context.Get(this.RangeValue);

            var xlApp = ExcelBot.Shared.GetInstance();
            var xlWb = ExcelBot.Shared.GetWorkbook(xlApp, workbookName);
            var xlWs = ExcelBot.Shared.GetWorksheet(xlWb, worksheetName);
            xlWs.Range(rangeAddress).Value = rangeValue;

            // outputs
            //
        }
    }
}
