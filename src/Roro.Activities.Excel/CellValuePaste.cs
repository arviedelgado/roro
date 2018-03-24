using System;

namespace Roro.Activities.Excel
{
    public class CellValuePaste : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public Input<Text> Cell { get; set; }

        public Input<TrueFalse> PasteValuesOnly { get; set; }

        public override void Execute(ActivityContext context)
        {
            var wbName = context.Get(this.WorkbookName);
            var wsName = context.Get(this.WorksheetName);
            var range = context.Get(this.Cell);
            var pasteType = context.Get(this.PasteValuesOnly, false);

            if (pasteType)
            {
                ExcelBot.Shared.GetRange(wbName, wsName, range).PasteSpecial(XlPasteType.xlPasteValuesAndNumberFormats);
            }
            else
            {
                ExcelBot.Shared.GetRange(wbName, wsName, range).PasteSpecial();
            }
        }

        /// <summary>
        /// https://msdn.microsoft.com/en-us/vba/excel-vba/articles/xlpastetype-enumeration-excel
        /// </summary>
        private enum XlPasteType
        {
            xlPasteAll = -4104, //Everything will be pasted.
            xlPasteAllExceptBorders = 7, //Everything except borders will be pasted.
            xlPasteAllMergingConditionalFormats = 14, //Everything will be pasted and conditional formats will be merged.
            xlPasteAllUsingSourceTheme = 13, //Everything will be pasted using the source theme.
            xlPasteColumnWidths = 8, //Copied column width is pasted.
            xlPasteComments = -4144, //Comments are pasted.
            xlPasteFormats = -4122, //Copied source format is pasted.
            xlPasteFormulas = -4123, //Formulas are pasted.
            xlPasteFormulasAndNumberFormats = 11, //Formulas and Number formats are pasted.
            xlPasteValidation = 6, //Validations are pasted.
            xlPasteValues = -4163, //Values are pasted.
            xlPasteValuesAndNumberFormats = 12, //Values and Number formats are pasted.
        }
    }
}
