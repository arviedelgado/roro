using System;
using System.Windows.Forms;

namespace Roro.Activities.General
{
    public sealed class ShowMessage : ProcessNodeActivity
    {
        public InArgument<Text> Message { get; set; }

        public override void Execute(ActivityContext context)
        {
            var message = context.Get(this.Message);
            MessageBox.Show(message);
        }
    }
}
