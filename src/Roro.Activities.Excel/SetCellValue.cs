using System;

namespace Roro.Activities.Excel
{
    public class SetCellValue : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public Input<Number> RowIndex { get; set; }

        public Input<Number> ColumnIndex { get; set; }

        public Input<Text> CellValue { get; set; }

        public override void Execute(ActivityContext context)
        {
            // inputs
            var workbookName = (string)context.Get(this.WorkbookName);
            var worksheetName = (string)context.Get(this.WorksheetName);
            var rowIndex = (int)context.Get(this.RowIndex);
            var columnIndex = (int)context.Get(this.ColumnIndex);
            var cellValue = (string)context.Get(this.CellValue);

            var xlApp = ExcelBot.Shared.GetInstance();
            var xlWb = ExcelBot.Shared.GetWorkbook(xlApp, workbookName);
            var xlWs = ExcelBot.Shared.GetWorksheet(xlWb, worksheetName);
            xlWs.Cells(rowIndex, columnIndex).Value = cellValue;

            // outputs
            //
        }
    }
}
