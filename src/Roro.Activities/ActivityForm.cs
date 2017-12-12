using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roro.Activities
{
    public class ActivityForm : Form
    {
        private Panel panel1;
        private TabControl tabControl1;
        private TabPage variablesTabPage;
        private TabPage environmentVariablesTabPage;
        private SplitContainer splitContainer1;
        private TableLayoutPanel argumentTableLayoutPanel;

        private void InitializeComponent()
        {
            this.argumentTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.variablesTabPage = new System.Windows.Forms.TabPage();
            this.environmentVariablesTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1.SuspendLayout();
            this.variablesTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // argumentTableLayoutPanel
            // 
            this.argumentTableLayoutPanel.ColumnCount = 1;
            this.argumentTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.argumentTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.argumentTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.argumentTableLayoutPanel.Name = "argumentTableLayoutPanel";
            this.argumentTableLayoutPanel.Size = new System.Drawing.Size(511, 411);
            this.argumentTableLayoutPanel.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(205, 377);
            this.panel1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.variablesTabPage);
            this.tabControl1.Controls.Add(this.environmentVariablesTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(219, 411);
            this.tabControl1.TabIndex = 3;
            // 
            // variablesTabPage
            // 
            this.variablesTabPage.Controls.Add(this.panel1);
            this.variablesTabPage.Location = new System.Drawing.Point(4, 24);
            this.variablesTabPage.Name = "variablesTabPage";
            this.variablesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.variablesTabPage.Size = new System.Drawing.Size(211, 383);
            this.variablesTabPage.TabIndex = 0;
            this.variablesTabPage.Text = "Variables";
            this.variablesTabPage.UseVisualStyleBackColor = true;
            // 
            // environmentVariablesTabPage
            // 
            this.environmentVariablesTabPage.Location = new System.Drawing.Point(4, 24);
            this.environmentVariablesTabPage.Name = "environmentVariablesTabPage";
            this.environmentVariablesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.environmentVariablesTabPage.Size = new System.Drawing.Size(172, 383);
            this.environmentVariablesTabPage.TabIndex = 1;
            this.environmentVariablesTabPage.Text = "Environment Variables";
            this.environmentVariablesTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.argumentTableLayoutPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(734, 411);
            this.splitContainer1.SplitterDistance = 511;
            this.splitContainer1.TabIndex = 4;
            // 
            // ActivityForm
            // 
            this.ClientSize = new System.Drawing.Size(734, 411);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "ActivityForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ActivityForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.variablesTabPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public ActivityForm(Activity activity)
        {
            var variables = new List<Variable>()
            {
                new Variable<Text>("Text1"),
                new Variable<Text>("Text2"),
                new Variable<Number>("Num1"),
                new Variable<Number>("Num2"),
                new Variable<Number>("Num3"),
                new Variable<Flag>("Flag1"),
                new Variable<DateTime>("DateTime1"),
                new Variable<Collection>("Collection1"),
            };

            this.InitializeComponent();
            this.InitializeArgumentsLayoutPanel(activity);

            VariableForm.Create(variables).Parent = this.panel1;

        }

        private void InitializeArgumentsLayoutPanel(Activity activity)
        {
            var variables = new List<Variable>()
            {
                new Variable<Text>("Text1"),
                new Variable<Text>("Text2"),
                new Variable<Number>("Num1"),
                new Variable<Number>("Num2"),
                new Variable<Number>("Num3"),
                new Variable<Flag>("Flag1"),
                new Variable<DateTime>("DateTime1"),
                new Variable<Collection>("Collection1"),
            };

            var argumentPanels = new List<Panel>();

            if (activity.GetArguments<InArgument>() is List<InArgument> inArguments)
            {
                argumentPanels.Add(ArgumentForm.Create(activity, inArguments, variables));
            }

            if (activity.GetArguments<OutArgument>() is List<OutArgument> outArguments)
            {
                argumentPanels.Add(ArgumentForm.Create(activity, outArguments, variables));
            }

            if (activity.GetArguments<InOutArgument>() is List<InOutArgument> inOutArguments)
            {
                argumentPanels.Add(ArgumentForm.Create(activity, inOutArguments, variables));
            }

            this.argumentTableLayoutPanel.RowCount = argumentPanels.Count;
            this.argumentTableLayoutPanel.RowStyles.Clear();
            foreach (var argumentPanel in argumentPanels)
            {
                this.argumentTableLayoutPanel.Controls.Add(argumentPanel, 0,
                this.argumentTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / argumentPanels.Count)));
            }
        }

        

        private void ActivityForm_Load(object sender, EventArgs e)
        {

        }
    }
}
