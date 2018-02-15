using System;

namespace Roro.Activities.Excel
{
    public class CellValueSet : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public Input<Text> CellAddress { get; set; }

        public Input<Text> CellValue { get; set; }

        public override void Execute(ActivityContext context)
        {
            // inputs
            var workbookName = (string)context.Get(this.WorkbookName);
            var worksheetName = (string)context.Get(this.WorksheetName);
            var cellAddress = (string)context.Get(this.CellAddress);
            var cellValue = (string)context.Get(this.CellValue);

            var xlApp = ExcelBot.Shared.GetInstance();
            var xlWb = ExcelBot.Shared.GetWorkbook(xlApp, workbookName);
            var xlWs = ExcelBot.Shared.GetWorksheet(xlWb, worksheetName);
            xlWs.Range(cellAddress).Value = cellValue;

            // outputs
            //
        }
    }
}
