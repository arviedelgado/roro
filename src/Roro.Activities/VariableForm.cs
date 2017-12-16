using Roro.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Roro.Activities
{
    public sealed class VariableForm : Form
    {
        private Panel panel1;
        private TreeView treeView1;
        private Label variableLabel;
        private TextBox textBox1;

        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node0");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node1");
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.textBox1 = new TextBox();
            this.variableLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.variableLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(434, 311);
            this.panel1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 48);
            this.treeView1.Name = "treeView1";
            treeNode3.Name = "Node0";
            treeNode3.Text = "Node0";
            treeNode4.Name = "Node1";
            treeNode4.Text = "Node1";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
            this.treeView1.Size = new System.Drawing.Size(434, 263);
            this.treeView1.TabIndex = 1;
            this.treeView1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Location = new System.Drawing.Point(0, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(434, 23);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            // 
            // variableLabel
            // 
            this.variableLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.variableLabel.Font = new System.Drawing.Font("Segoe UI Light", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.variableLabel.Location = new System.Drawing.Point(0, 0);
            this.variableLabel.Name = "variableLabel";
            this.variableLabel.Size = new System.Drawing.Size(434, 25);
            this.variableLabel.TabIndex = 4;
            this.variableLabel.Text = "Variables";
            // 
            // VariableForm
            // 
            this.ClientSize = new System.Drawing.Size(434, 311);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "VariableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.VariableForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private VariableForm() => this.InitializeComponent();

        public static Panel Create(Page page)
        {
            var form = new VariableForm();
            form.treeView1.Tag = page.Variables;
            form.textBox1.TextChanged += (sender, e) => form.FilterTreeView();
            form.FilterTreeView();
            return form.panel1;
        }

        private void FilterTreeView()
        {
            this.treeView1.BeginUpdate(); // avoid flickering.
            var dataSource = this.treeView1.Tag as List<Variable>;
            var filterText = this.textBox1.Text.ToLower().Replace(" ", string.Empty);
            this.treeView1.Nodes.Clear();
            foreach (var dataType in DataType.GetCommonTypes())
            {
                var rootNode = this.treeView1.Nodes.Add(dataType.Name);
                foreach (var variable in dataSource.Where(x => x.DataTypeId == dataType.Id))
                {
                    if (variable.Name.ToLower().Contains(filterText))
                    {
                        rootNode.Nodes.Add(variable.Name).EnsureVisible();
                    }
                }
            }
            this.treeView1.Nodes[0].EnsureVisible();
            this.treeView1.EndUpdate();
        }

        private void VariableForm_Load(object sender, EventArgs e)
        {

        }
    }
}