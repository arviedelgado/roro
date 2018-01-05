
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
                //Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new Form1());
                f.ShowDialog();
            }
        }
    }
}
