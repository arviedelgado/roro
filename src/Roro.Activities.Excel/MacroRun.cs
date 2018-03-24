using System;

namespace Roro.Activities.Excel
{
    public class MacroRun : ProcessNodeActivity
    {
        public Input<Text> Macro { get; set; }

        public Input<Text> Param1 { get; set; }

        public Input<Text> Param2 { get; set; }

        public Input<Text> Param3 { get; set; }

        public Input<Text> Param4 { get; set; }

        public Output<Text> Result { get; set; }

        public override void Execute(ActivityContext context)
        {
            var macro = context.Get(this.Macro).ToString();
            var param1 = context.Get(this.Param1, null);
            var param2 = context.Get(this.Param2, null);
            var param3 = context.Get(this.Param3, null);
            var param4 = context.Get(this.Param4, null);

            var result = string.Empty;

            // F*ck! The code below is ugly.

            if (param1 == null)
            {
                result = ExcelBot.Shared.GetApp().Run(macro);
            }
            else if (param2 == null)
            {
                result = ExcelBot.Shared.GetApp().Run(macro, param1.ToString());
            }
            else if (param3 == null)
            {
                result = ExcelBot.Shared.GetApp().Run(macro, param1.ToString(), param2.ToString());
            }
            else if (param4 == null)
            {
                result = ExcelBot.Shared.GetApp().Run(macro, param1.ToString(), param2.ToString(), param3.ToString());
            }
            else
            {
                result = ExcelBot.Shared.GetApp().Run(macro, param1.ToString(), param2.ToString(), param3.ToString(), param4.ToString());
            }

            context.Set(this.Result, result);
        }
    }
}
