using Roro.Workflow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Roro.Activities
{
    public sealed class VariableForm : Form
    {
        private Panel variablePanel;
        private TreeView variableTreeView;
        private Label variableLabel;
        private TextBox variableTextBox;

        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node0");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node1");
            this.variablePanel = new System.Windows.Forms.Panel();
            this.variableTreeView = new System.Windows.Forms.TreeView();
            this.variableTextBox = new System.Windows.Forms.TextBox();
            this.variableLabel = new System.Windows.Forms.Label();
            this.variablePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // variablePanel
            // 
            this.variablePanel.BackColor = System.Drawing.Color.White;
            this.variablePanel.Controls.Add(this.variableTreeView);
            this.variablePanel.Controls.Add(this.variableTextBox);
            this.variablePanel.Controls.Add(this.variableLabel);
            this.variablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.variablePanel.Location = new System.Drawing.Point(0, 0);
            this.variablePanel.Name = "variablePanel";
            this.variablePanel.Padding = new System.Windows.Forms.Padding(5);
            this.variablePanel.Size = new System.Drawing.Size(434, 311);
            this.variablePanel.TabIndex = 0;
            // 
            // variableTreeView
            // 
            this.variableTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.variableTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.variableTreeView.FullRowSelect = true;
            this.variableTreeView.HotTracking = true;
            this.variableTreeView.Location = new System.Drawing.Point(5, 53);
            this.variableTreeView.Name = "variableTreeView";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Node0";
            treeNode2.Name = "Node1";
            treeNode2.Text = "Node1";
            this.variableTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.variableTreeView.ShowLines = false;
            this.variableTreeView.Size = new System.Drawing.Size(424, 253);
            this.variableTreeView.TabIndex = 1;
            this.variableTreeView.TabStop = false;
            // 
            // variableTextBox
            // 
            this.variableTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.variableTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.variableTextBox.Location = new System.Drawing.Point(5, 30);
            this.variableTextBox.Name = "variableTextBox";
            this.variableTextBox.Size = new System.Drawing.Size(424, 23);
            this.variableTextBox.TabIndex = 0;
            this.variableTextBox.TabStop = false;
            // 
            // variableLabel
            // 
            this.variableLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.variableLabel.Font = new System.Drawing.Font("Segoe UI Light", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.variableLabel.Location = new System.Drawing.Point(5, 5);
            this.variableLabel.Name = "variableLabel";
            this.variableLabel.Size = new System.Drawing.Size(424, 25);
            this.variableLabel.TabIndex = 4;
            this.variableLabel.Text = "Variables";
            // 
            // VariableForm
            // 
            this.ClientSize = new System.Drawing.Size(434, 311);
            this.Controls.Add(this.variablePanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "VariableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.VariableForm_Load);
            this.variablePanel.ResumeLayout(false);
            this.variablePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        private VariableForm() => this.InitializeComponent();

        public static Panel Create(Page page)
        {
            var form = new VariableForm();
            form.variableTreeView.SetWindowTheme("explorer");
            form.variableTreeView.Tag = page.Variables;
            form.variableTextBox.TextChanged += (sender, e) => form.FilterTreeView();
            form.FilterTreeView();
            return form.variablePanel;
        }

        private void FilterTreeView()
        {
            this.variableTreeView.BeginUpdate(); // avoid flickering.
            var dataSource = this.variableTreeView.Tag as List<Variable>;
            var filterText = this.variableTextBox.Text.ToLower().Replace(" ", string.Empty);
            this.variableTreeView.Nodes.Clear();
            foreach (var dataType in DataType.GetCommonTypes())
            {
                var rootNode = this.variableTreeView.Nodes.Add(dataType.Name);
                foreach (var variable in dataSource.Where(x => x.DataTypeId == dataType.Id))
                {
                    if (variable.Name.ToLower().Contains(filterText))
                    {
                        rootNode.Nodes.Add(variable.Name).EnsureVisible();
                    }
                }
            }
            this.variableTreeView.Nodes[0].EnsureVisible();
            this.variableTreeView.EndUpdate();
        }

        private void VariableForm_Load(object sender, EventArgs e)
        {

        }
    }
}