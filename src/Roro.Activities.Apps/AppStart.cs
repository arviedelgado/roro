using System;
using System.Diagnostics;
using System.Linq;

namespace Roro.Activities.Apps
{
    public sealed class AppStart : ProcessNodeActivity
    {
        public Input<Text> AppPath { get; set; }

        public Input<Text> Arguments { get; set; }

        public override void Execute(ActivityContext context)
        {
            var appPath = context.Get(this.AppPath);
            var appArgs = context.Get(this.Arguments, string.Empty);

            var p = Process.Start(appPath, appArgs);
            while (p.MainWindowHandle == IntPtr.Zero)
            {
                context.ThrowIfCancellationRequested();
                try
                {
                    p.WaitForInputIdle(1000);
                }
                catch (InvalidOperationException)
                {
                    break;
                }
                p.Refresh();
            }
        }
    }
}
