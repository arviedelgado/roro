
using Roro.Activities;
using System;
using System.Windows.Forms;

namespace Roro.Studio
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (var f = PageForm.Create())
            {
                Application.EnableVisualStyles();
                f.ShowDialog();
            }
        }
    }
}
