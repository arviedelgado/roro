using System;

namespace Roro.Activities.Excel
{
    public class WorkbookSave : ProcessNodeActivity
    {
        public Input<Text> WorkbookName { get; set; }

        public override void Execute(ActivityContext context)
        {
            throw new NotImplementedException();
        }
    }
}
