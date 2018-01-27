using System;

namespace Roro.Activities.Excel
{
    public class AddWorksheet : ProcessNodeActivity
    {
        public Input<Text> WorkbookPath { get; set; }

        public Input<Text> WorksheetName { get; set; }

        public override void Execute(ActivityContext context)
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
