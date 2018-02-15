namespace Roro.Activities.Excel
{
    public class CellValueGet : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public Input<Text> CellAddress { get; set; }

        public Output<Text> CellValue { get; set; }

        public override void Execute(ActivityContext context)
        {
            // inputs
            var workbookName = (string)context.Get(this.WorkbookName);
            var worksheetName = (string)context.Get(this.WorksheetName);
            var rangeAddress = (string)context.Get(this.CellAddress);

            var xlApp = ExcelBot.Shared.GetInstance();
            var xlWb = ExcelBot.Shared.GetWorkbook(xlApp, workbookName);
            var xlWs = ExcelBot.Shared.GetWorksheet(xlWb, worksheetName);
            var rangeValue = xlWs.Range(rangeAddress).Value?.ToString() ?? string.Empty;

            // outputs
            context.Set(this.CellValue, rangeValue);
        }
    }
}
