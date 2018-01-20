using System;

namespace Roro.Activities.Excel
{
    public class CloseWorkbook : ProcessNodeActivity
    {
        public Input<Text> Path { get; set; }

        public override void Execute(ActivityContext context)
        {
            // inputs
            var path = context.Get(this.Path);

            var bot = ExcelBot.Shared;
            var xlApp = bot.Get(path);
            bot.Release(xlApp);

            // outputs
            //
        }
    }
}
