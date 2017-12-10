using System;

namespace Roro.Activities.Excel
{
    public class SaveWorkbook : ProcessNodeActivity
    {
        public InArgument<Text> Path { get; set; }

        public InArgument<Text> NewPath { get; set; }

        public override void Execute(ActivityContext context)
        {
            // inputs
            var path = context.Get(this.Path);
            var newPath = context.Get(this.NewPath);

            var bot = ExcelBot.Shared;
            var xlApp = bot.Get(path);
            var xlWbs = xlApp.Workbooks;
            var xlWb = xlWbs.Item[1];
            if (string.IsNullOrWhiteSpace(path))
            {
                xlWb.Save();
            }
            else
            {
                xlWb.SaveAs(newPath);
            }
            bot.Release(xlWb, xlWbs);

            // outputs
            //
        }
    }
}
