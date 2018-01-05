
using System;
using System.Windows.Forms;

namespace Roro.Activities
{
    public class NodeForm : Form
    {
        private TableLayoutPanel tableLayoutPanel1;
        private Panel variablePanel;
        private Panel argumentPanel;
        private Panel nodeNamePanel;
        private Label nodeNameLabel;
        private TextBox nodeNameTextBox;

        private void InitializeComponent()
        {
            this.nodeNameTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.nodeNamePanel = new System.Windows.Forms.Panel();
            this.nodeNameLabel = new System.Windows.Forms.Label();
            this.variablePanel = new System.Windows.Forms.Panel();
            this.argumentPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.nodeNamePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // nodeNameTextBox
            // 
            this.nodeNameTextBox.BackColor = System.Drawing.Color.White;
            this.nodeNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nodeNameTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.nodeNameTextBox.Font = new System.Drawing.Font("Segoe UI Light", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.nodeNameTextBox.Location = new System.Drawing.Point(0, 0);
            this.nodeNameTextBox.MinimumSize = new System.Drawing.Size(0, 100);
            this.nodeNameTextBox.Name = "nodeNameTextBox";
            this.nodeNameTextBox.Size = new System.Drawing.Size(468, 27);
            this.nodeNameTextBox.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.Controls.Add(this.nodeNamePanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.variablePanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.argumentPanel, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(20);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 461);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // nodeNamePanel
            // 
            this.nodeNamePanel.Controls.Add(this.nodeNameLabel);
            this.nodeNamePanel.Controls.Add(this.nodeNameTextBox);
            this.nodeNamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeNamePanel.Location = new System.Drawing.Point(23, 23);
            this.nodeNamePanel.Name = "nodeNamePanel";
            this.nodeNamePanel.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.nodeNamePanel.Size = new System.Drawing.Size(488, 34);
            this.nodeNamePanel.TabIndex = 0;
            this.nodeNamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.nodeNamePanel_Paint);
            // 
            // nodeNameLabel
            // 
            this.nodeNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nodeNameLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.nodeNameLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.nodeNameLabel.Location = new System.Drawing.Point(0, 33);
            this.nodeNameLabel.Name = "nodeNameLabel";
            this.nodeNameLabel.Size = new System.Drawing.Size(468, 1);
            this.nodeNameLabel.TabIndex = 0;
            this.nodeNameLabel.Text = "nodeNameLabel";
            // 
            // variablePanel
            // 
            this.variablePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.variablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.variablePanel.Location = new System.Drawing.Point(517, 23);
            this.variablePanel.Name = "variablePanel";
            this.tableLayoutPanel1.SetRowSpan(this.variablePanel, 2);
            this.variablePanel.Size = new System.Drawing.Size(244, 415);
            this.variablePanel.TabIndex = 8;
            // 
            // argumentPanel
            // 
            this.argumentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.argumentPanel.Location = new System.Drawing.Point(23, 63);
            this.argumentPanel.Name = "argumentPanel";
            this.argumentPanel.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.argumentPanel.Size = new System.Drawing.Size(488, 375);
            this.argumentPanel.TabIndex = 7;
            // 
            // NodeForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "NodeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Activity Editor";
            this.Load += new System.EventHandler(this.ActivityForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.nodeNamePanel.ResumeLayout(false);
            this.nodeNamePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        private NodeForm() => this.InitializeComponent();

        public NodeForm(Page page, Node node) : this()
        {
            ArgumentForm.Create(page, node).Parent = this.argumentPanel;
            VariableForm.Create(page).Parent = this.variablePanel;
            this.nodeNameTextBox.DataBindings.Add("Text", node, "Name", false, DataSourceUpdateMode.OnPropertyChanged);
            this.nodeNameTextBox.PreviewKeyDown += NodeNameTextBox_PreviewKeyDown;
            this.Text = "Activity Editor - " + node.Activity.GetType().FullName;
        }

        private void NodeNameTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.Close();
        }

        private void ActivityForm_Load(object sender, EventArgs e)
        {

        }

        private void nodeNamePanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
