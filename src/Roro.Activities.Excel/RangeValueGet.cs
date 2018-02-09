namespace Roro.Activities.Excel
{
    public class RangeValueGet : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public Input<Text> RangeAddress { get; set; }

        public Output<Text> RangeValue { get; set; }

        public override void Execute(ActivityContext context)
        {
            // inputs
            var workbookName = (string)context.Get(this.WorkbookName);
            var worksheetName = (string)context.Get(this.WorksheetName);
            var rangeAddress = (string)context.Get(this.RangeAddress);

            var xlApp = ExcelBot.Shared.GetInstance();
            var xlWb = ExcelBot.Shared.GetWorkbook(xlApp, workbookName);
            var xlWs = ExcelBot.Shared.GetWorksheet(xlWb, worksheetName);
            var rangeValue = xlWs.Range(rangeAddress).Value?.ToString() ?? string.Empty;

            // outputs
            context.Set(this.RangeValue, rangeValue);
        }
    }
}
