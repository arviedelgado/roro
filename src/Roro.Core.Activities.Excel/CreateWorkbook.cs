using System;

namespace Roro.Core.Activities.Excel
{
    public class CreateWorkbook : Activity
    {
        public OutArgument<string> Path { get; set; }

        public override void Execute(Context context)
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
