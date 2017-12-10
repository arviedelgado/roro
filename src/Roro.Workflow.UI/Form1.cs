using Roro.Activities;
using System;
using System.Drawing;
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
            this.Width = 1000;
            this.Height = 600;
            robot = Test.TestWorkflowSerialization();
            robot.Pages.First().AttachEvents(this.skWorkspaceParent);

            this.activitiesTreeView.ItemDrag += ActivitiesTreeView_ItemDrag;
            this.skWorkspaceParent.DragEnter += SkWorkspaceParent_DragEnter;
            this.skWorkspaceParent.DragDrop += SkWorkspaceParent_DragDrop;


            this.activitiesTreeView.Nodes.Clear();
            foreach (var act in Activity.GetActivities())
            {
                this.activitiesTreeView.Nodes.Add(act.FullName, act.Name.Humanize());
            }
        }

        private void SkWorkspaceParent_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void ActivitiesTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var view = sender as TreeView;
            var node = e.Item as TreeNode;
            view.SelectedNode = node;
            this.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void SkWorkspaceParent_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(TreeNode).FullName) is TreeNode treeNode)
            {
                var canvas = sender as Control;
                var node = robot.Pages.First().AddNode(treeNode.Name);
                var rect = node.Bounds;
                rect.Location = canvas.PointToClient(new Point(e.X, e.Y));
                rect.Offset(-rect.Width / 2, -rect.Height / 2);
                node.SetBounds(rect);
                canvas.Invalidate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Test.TestElementActivities();
        }
    }
}
