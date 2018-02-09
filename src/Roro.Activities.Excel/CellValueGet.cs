using System;
using System.ComponentModel;

namespace Roro.Activities.Excel
{
    public class CellValueGet : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public Input<Number> RowIndex { get; set; }

        public Input<Number> ColumnIndex { get; set; }

        public Output<Text> CellValue { get; set; }

        public override void Execute(ActivityContext context)
        {
            // inputs
            var workbookName = (string)context.Get(this.WorkbookName);
            var worksheetName = (string)context.Get(this.WorksheetName);
            var rowIndex = (int)context.Get(this.RowIndex);
            var columnIndex = (int)context.Get(this.ColumnIndex);
       
            var xlApp = ExcelBot.Shared.GetInstance();
            var xlWb = ExcelBot.Shared.GetWorkbook(xlApp, workbookName);
            var xlWs = ExcelBot.Shared.GetWorksheet(xlWb, worksheetName);
            var cellValue = xlWs.Cells(rowIndex, columnIndex).Value?.ToString() ?? string.Empty;

            // outputs
            context.Set(this.CellValue, cellValue);
        }
    }
}
