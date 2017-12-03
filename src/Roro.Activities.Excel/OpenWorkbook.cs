using System;

namespace Roro.Activities.Excel
{
    public class OpenWorkbook : Activity
    {
        public Input<Text> Path { get; set; }

        public override void Execute(ActivityContext context)
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
