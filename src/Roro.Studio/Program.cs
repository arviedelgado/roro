
using Roro.Activities;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Roro.Studio
{
    class Program
    {
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [STAThread]
        static void Main(string[] args)
        {
            SetProcessDPIAware();
            Application.EnableVisualStyles();
            using (var f = PageForm.Create())
            {
                f.ShowDialog();
            }
        }
    }
}
