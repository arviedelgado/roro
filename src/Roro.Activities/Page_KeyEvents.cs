using System.Windows.Forms;

namespace Roro.Activities
{
    public partial class Page
    {
        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.DeleteNode();
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                this.SelectAllNodes();
            }
        }

        private void DeleteNode()
        {
            foreach (var selectedNode in this.SelectedNodes)
            {
                if (selectedNode == this.currentNode)
                {
                    this.currentNode = null;
                }
                this.RemoveNode(selectedNode);
            }
            this.SelectedNodes.Clear();
            this.Canvas.Invalidate();
        }

        private void SelectAllNodes()
        {
            this.SelectedNodes.Clear();
            foreach (var node in this.Nodes)
            {
                this.SelectedNodes.Add(node);
            }
            this.Canvas.Invalidate();
        }
    }
}