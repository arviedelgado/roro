using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roro.Workflow.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
        }

        Document robot;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 800;
            this.Height = 600;
            robot = Test.TestWorkflowSerialization();
            robot.Pages.First().AttachEvents(this);
        }
    }
}
