using Roro.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roro.Studio
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (var f = DocumentForm.Create())
            {
                Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new Form1());
                f.ShowDialog();
            }
        }
    }
}
