using Roro.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Roro.Workflow
{
    public sealed class ActivityForm : Form
    {
        private Panel activityPanel;
        private TreeView activityTreeView;
        private Label activityLabel;
        private TextBox activityTextBox;

        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node0");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node1");
            this.activityPanel = new System.Windows.Forms.Panel();
            this.activityTreeView = new System.Windows.Forms.TreeView();
            this.activityTextBox = new System.Windows.Forms.TextBox();
            this.activityLabel = new System.Windows.Forms.Label();
            this.activityPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // activityPanel
            // 
            this.activityPanel.BackColor = System.Drawing.Color.White;
            this.activityPanel.Controls.Add(this.activityTreeView);
            this.activityPanel.Controls.Add(this.activityTextBox);
            this.activityPanel.Controls.Add(this.activityLabel);
            this.activityPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityPanel.Location = new System.Drawing.Point(0, 0);
            this.activityPanel.Name = "activityPanel";
            this.activityPanel.Size = new System.Drawing.Size(434, 311);
            this.activityPanel.TabIndex = 0;
            // 
            // activityTreeView
            // 
            this.activityTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.activityTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityTreeView.Location = new System.Drawing.Point(0, 48);
            this.activityTreeView.Name = "activityTreeView";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Node0";
            treeNode2.Name = "Node1";
            treeNode2.Text = "Node1";
            this.activityTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.activityTreeView.Size = new System.Drawing.Size(434, 263);
            this.activityTreeView.TabIndex = 1;
            this.activityTreeView.TabStop = false;
            // 
            // activityTextBox
            // 
            this.activityTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.activityTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.activityTextBox.Location = new System.Drawing.Point(0, 25);
            this.activityTextBox.Name = "activityTextBox";
            this.activityTextBox.Size = new System.Drawing.Size(434, 23);
            this.activityTextBox.TabIndex = 0;
            this.activityTextBox.TabStop = false;
            // 
            // activityLabel
            // 
            this.activityLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.activityLabel.Font = new System.Drawing.Font("Segoe UI Light", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.activityLabel.Location = new System.Drawing.Point(0, 0);
            this.activityLabel.Name = "activityLabel";
            this.activityLabel.Size = new System.Drawing.Size(434, 25);
            this.activityLabel.TabIndex = 4;
            this.activityLabel.Text = "Activities";
            // 
            // ActivityForm
            // 
            this.ClientSize = new System.Drawing.Size(434, 311);
            this.Controls.Add(this.activityPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ActivityForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ActivityForm_Load);
            this.activityPanel.ResumeLayout(false);
            this.activityPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        private ActivityForm() => this.InitializeComponent();

        public static Panel Create()
        {
            var form = new ActivityForm();
            form.activityTreeView.Tag = Activity.GetActivities();
            form.activityTreeView.ItemDrag += TreeView1_ItemDrag;
            form.activityTextBox.TextChanged += (sender, e) => form.FilterTreeView();
            form.FilterTreeView();
            return form.activityPanel;
        }

        private static void TreeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var treeView = sender as TreeView;
            treeView.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void FilterTreeView()
        {
            this.activityTreeView.BeginUpdate(); // avoid flickering.
            var dataSource = this.activityTreeView.Tag as IEnumerable<Type>;
            var filterText = this.activityTextBox.Text.ToLower().Replace(" ", string.Empty);
            this.activityTreeView.Nodes.Clear();
            foreach (var type in dataSource)
            {
                var groupKey = String.Join(".", type.FullName.Split('.').Reverse().Skip(1).Reverse());
                if (!this.activityTreeView.Nodes.ContainsKey(groupKey))
                {
                    this.activityTreeView.Nodes.Add(groupKey, groupKey.Split('.').Last());
                }
                if (type.Name.ToLower().Replace(" ", string.Empty).Contains(filterText))
                {
                    this.activityTreeView.Nodes[groupKey].Nodes.Add(type.FullName, type.Name.Humanize()).EnsureVisible();
                }
            }
            if (this.activityTreeView.Nodes.Count > 0)
            {
                this.activityTreeView.Nodes[0].EnsureVisible();
            }
            this.activityTreeView.EndUpdate();
        }

        private void ActivityForm_Load(object sender, EventArgs e)
        {

        }
    }
}