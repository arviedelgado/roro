using System;

namespace Roro.Activities.Excel
{
    public class CloseWorkbook : Activity
    {
        public InArgument<string> Path { get; set; }

        public override void Execute(Context context)
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
