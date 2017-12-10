using Microsoft.Office.Interop.Excel;
using System;

namespace Roro.Activities.Excel
{
    public class HasWorksheet : DecisionNodeActivity
    {
        public InArgument<Text> WorkbookPath { get; set; }

        public InArgument<Text> WorksheetName { get; set; }

        public override bool Execute(ActivityContext context)
        {
            throw new NotImplementedException();
        }
    }
}
