using System;

namespace Roro.Core.Activities.Excel
{
    public class OpenWorkbook : Activity
    {
        public InArgument<string> Path { get; set; }

        public override void Execute(Context context)
        {
            // inputs
            var path = context.Get(this.Path);

            var bot = ExcelBot.Shared;
            var xlApp = bot.Get();
            var xlWbs = xlApp.Workbooks;
            var xlWb = xlWbs.Open(path);
            bot.Release(xlWb, xlWbs);

            // outputs
            // 
        }
    }
}
