
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Roro.Activities
{
    public sealed class ActivityForm : Form
    {
        private Panel activityPanel;
        private TreeView activityTreeView;
        private Label activityLabel;
        private TextBox activityTextBox;

        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Process Node");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Decision Node?");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Activities", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
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
            this.activityPanel.Padding = new System.Windows.Forms.Padding(5);
            this.activityPanel.Size = new System.Drawing.Size(434, 311);
            this.activityPanel.TabIndex = 0;
            // 
            // activityTreeView
            // 
            this.activityTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.activityTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityTreeView.FullRowSelect = true;
            this.activityTreeView.HotTracking = true;
            this.activityTreeView.Location = new System.Drawing.Point(5, 53);
            this.activityTreeView.Name = "activityTreeView";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Process Node";
            treeNode2.Name = "Node1";
            treeNode2.Text = "Decision Node?";
            treeNode3.Name = "Node0";
            treeNode3.Text = "Activities";
            this.activityTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.activityTreeView.ShowLines = false;
            this.activityTreeView.Size = new System.Drawing.Size(424, 253);
            this.activityTreeView.TabIndex = 1;
            this.activityTreeView.TabStop = false;
            // 
            // activityTextBox
            // 
            this.activityTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.activityTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.activityTextBox.Location = new System.Drawing.Point(5, 30);
            this.activityTextBox.Name = "activityTextBox";
            this.activityTextBox.Size = new System.Drawing.Size(424, 23);
            this.activityTextBox.TabIndex = 0;
            this.activityTextBox.TabStop = false;
            // 
            // activityLabel
            // 
            this.activityLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.activityLabel.Font = new System.Drawing.Font("Segoe UI Light", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.activityLabel.Location = new System.Drawing.Point(5, 5);
            this.activityLabel.Name = "activityLabel";
            this.activityLabel.Size = new System.Drawing.Size(424, 25);
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
            form.activityTreeView.SetWindowTheme("explorer");
            form.activityTreeView.Tag = Activity.GetExternalActivities();
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
            this.activityTreeView.Nodes.Clear();
            // internal activities
            var generalActivities = this.activityTreeView.Nodes.Add("General");
            generalActivities.Nodes.Add(typeof(VariableNodeActivity).FullName, "New Variable").EnsureVisible();
            generalActivities.Nodes.Add(typeof(PreparationNodeActivity).FullName, "Set Variables").EnsureVisible();
            generalActivities.Nodes.Add(typeof(LoopStartNodeActivity).FullName, "Loop Collection").EnsureVisible();
            generalActivities.Nodes.Add(typeof(EndNodeActivity).FullName, "Start / End").EnsureVisible();
            // external activities
            var dataSource = this.activityTreeView.Tag as IEnumerable<Type>;
            var filterText = this.activityTextBox.Text.ToLower().Replace(" ", string.Empty);
            foreach (var type in dataSource)
            {
                var groupKey = String.Join(".", type.FullName.Split('.').Reverse().Skip(1).Reverse());
                if (!this.activityTreeView.Nodes.ContainsKey(groupKey))
                {
                    this.activityTreeView.Nodes.Add(groupKey, groupKey.Split('.').Last() + " Activities");
                }
                if (type.Name.ToLower().Replace(" ", string.Empty).Contains(filterText))
                {
                    var nodeText = type.Name.Humanize();
                    if (typeof(DecisionNodeActivity).IsAssignableFrom(type))
                    {
                        nodeText += "?";
                    }
                    this.activityTreeView.Nodes[groupKey].Nodes.Add(type.FullName, nodeText).EnsureVisible();
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