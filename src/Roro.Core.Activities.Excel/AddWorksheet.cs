using Microsoft.Office.Interop.Excel;
using System;

namespace Roro.Core.Activities.Excel
{
    public class AddWorksheet : Activity
    {
        public InArgument<string> WorkbookPath { get; set; }

        public InArgument<string> WorksheetName { get; set; }

        public override void Execute(Context context)
        {
            // inputs
            var workbookPath = context.Get(this.WorkbookPath);
            var worksheetName = context.Get(this.WorksheetName);

            var bot = ExcelBot.Shared;
            var xlApp = bot.Get(workbookPath);
            var xlWbs = xlApp.Workbooks;
            var xlWb = xlWbs.Item[1];
            var xlWss = xlWb.Worksheets;
            var xlWs = xlWss.Add() as Worksheet;
            xlWs.Name = worksheetName;
            bot.Release(xlWss, xlWb, xlWbs);

            // outputs
            //
        }
    }
}
