using System;

namespace Roro.Activities.Excel
{
    public class CreateWorkbook : ProcessNodeActivity
    {
        public OutArgument<Text> Path { get; set; }

        public override void Execute(ActivityContext context)
        {
            // inputs
            //

            var bot = ExcelBot.Shared;
            var xlApp = bot.Get();
            var xlWbs = xlApp.Workbooks;
            var xlWb = xlWbs.Add();
            var path = xlWb.FullName;
            bot.Release(xlWb, xlWbs);

            // outputs
            context.Set(this.Path, path);
        }
    }
}
