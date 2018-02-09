using System;

namespace Roro.Activities.Excel
{
    public class RangeValuePaste : ProcessNodeActivity
    {
        public Input<Text> FromWorkbookName { get; set; }

        public Input<Text> FromWorksheetName { get; set; }

        public Input<Text> FromRangeAddress { get; set; }

        public Input<Text> ToWorkbookName { get; set; }

        public Input<Text> ToWorksheetName { get; set; }

        public Input<Text> ToRangeAddress { get; set; }

        public Input<TrueFalse> PasteAsValue { get; set; }

        public override void Execute(ActivityContext context)
        {
            throw new NotImplementedException();
        }
    }
}
