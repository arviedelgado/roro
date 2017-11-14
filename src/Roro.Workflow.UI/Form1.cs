using System;
using System.Linq;
using System.Windows.Forms;

namespace Roro.Workflow.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Document robot;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 800;
            this.Height = 600;
            robot = Test.TestWorkflowSerialization();
            robot.Pages.First().AttachEvents(this.panel1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Test.TestElementActivities();
        }
    }
}
