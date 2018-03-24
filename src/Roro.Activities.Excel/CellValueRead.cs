namespace Roro.Activities.Excel
{
    public class CellValueRead : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public Input<Text> Cell { get; set; }

        public Output<Text> Value { get; set; }

        public override void Execute(ActivityContext context)
        {
            var wbName = context.Get(this.WorkbookName);
            var wsName = context.Get(this.WorksheetName);
            var range = context.Get(this.Cell);

            var value = ExcelBot.Shared.GetRange(wbName, wsName, range).Value?.ToString() ?? string.Empty;

            context.Set(this.Value, value);
        }
    }
}
