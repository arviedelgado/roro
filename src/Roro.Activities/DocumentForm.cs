using System;
using System.Linq;
using System.Windows.Forms;

namespace Roro.Workflow
{
    public class DocumentForm : Form
    {

        private void InitializeComponent()
        {
            this.activityPanel = new System.Windows.Forms.Panel();
            this.pagePanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.runButton = new System.Windows.Forms.Button();
            this.setNextButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // activityPanel
            // 
            this.activityPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.activityPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.activityPanel.Location = new System.Drawing.Point(23, 73);
            this.activityPanel.Name = "activityPanel";
            this.activityPanel.Size = new System.Drawing.Size(244, 465);
            this.activityPanel.TabIndex = 0;
            // 
            // pagePanel
            // 
            this.pagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagePanel.Location = new System.Drawing.Point(273, 73);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.pagePanel.Size = new System.Drawing.Size(688, 465);
            this.pagePanel.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.activityPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pagePanel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(20);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 561);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.setNextButton);
            this.panel1.Controls.Add(this.runButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(273, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(688, 44);
            this.panel1.TabIndex = 1;
            // 
            // runButton
            // 
            this.runButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.runButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runButton.Location = new System.Drawing.Point(3, 3);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 38);
            this.runButton.TabIndex = 0;
            this.runButton.TabStop = false;
            this.runButton.Text = "RUN";
            this.runButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // setNextButton
            // 
            this.setNextButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.setNextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.setNextButton.Location = new System.Drawing.Point(84, 3);
            this.setNextButton.Name = "setNextButton";
            this.setNextButton.Size = new System.Drawing.Size(75, 38);
            this.setNextButton.TabIndex = 1;
            this.setNextButton.TabStop = false;
            this.setNextButton.Text = "Set As Next Activity";
            this.setNextButton.Click += new System.EventHandler(this.SetNextButton_Click);
            // 
            // DocumentForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DocumentForm";
            this.Load += new System.EventHandler(this.DocumentForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private DocumentForm() => this.InitializeComponent();

        private Panel pagePanel;
        private Panel activityPanel;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Button runButton;
        private Button setNextButton;
        private Page page;

        public static DocumentForm Create()
        {
            return new DocumentForm();
        }

        private void DocumentForm_Load(object sender, System.EventArgs e)
        {
            this.page = new Page();
            this.page.AttachEvents(this.pagePanel);
            ActivityForm.Create().Parent = this.activityPanel;
        }

        private void RunButton_Click(object sender, System.EventArgs e)
        {
            Console.Clear();
            new PageRunner(this.page).Run();
        }

        private void SetNextButton_Click(object sender, EventArgs e)
        {
            if (this.page.SelectedNodes.Count == 0)
            {
                MessageBox.Show("Please select an activity.");
            }
            else if (this.page.SelectedNodes.Count > 1)
            {
                MessageBox.Show("Please select 1 activity only.");
            }
            else
            {
                this.page.DebugNode = this.page.SelectedNodes.First();
                this.page.Render();
            }
        }
    }
}
