using System;

namespace Roro.Activities.Excel
{
    public class CellValuePaste : ProcessNodeActivity
    {
        public Input<Text> WorkbookNameFrom { get; set; }

        public Input<Text> WorksheetNameFrom { get; set; }

        public Input<Text> CellAddressFrom { get; set; }

        public Input<Text> WorkbookNameTo { get; set; }

        public Input<Text> WorkbookSheetTo { get; set; }

        public Input<Text> CellAddressTo { get; set; }

        public Input<TrueFalse> PasteAsValue { get; set; }

        public override void Execute(ActivityContext context)
        {
            throw new NotImplementedException();
        }
    }
}
