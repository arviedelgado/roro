using Microsoft.Office.Interop.Excel;
using System;

namespace Roro.Activities.Excel
{
    public class SetCellValue : ProcessNodeActivity
    {
        public Input<Text> WorkbookPath { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public Input<Number> RowIndex { get; set; }

        public Input<Number> ColumnIndex { get; set; }

        public Input<Text> CellValue { get; set; }

        public override void Execute(ActivityContext context)
        {
            // inputs
            var workbookPath = context.Get(this.WorkbookPath);
            var worksheetName = context.Get(this.WorksheetName);
            var rowIndex = context.Get(this.RowIndex);
            var columnIndex = context.Get(this.ColumnIndex);
            var cellValue = context.Get(this.CellValue);

            var bot = ExcelBot.Shared;
            var xlApp = bot.Get(workbookPath);
            var xlWbs = xlApp.Workbooks;
            var xlWb = xlWbs.Item[1];
            var xlWss = xlWb.Worksheets;
            var xlWs = xlWss.Item[0] as Worksheet;
            var xlCell = xlWs.Cells[rowIndex, columnIndex] as Range;
            xlCell.Value = cellValue;
            bot.Release(xlCell, xlWs, xlWss, xlWb, xlWbs);

            // outputs
            //
        }
    }
}
