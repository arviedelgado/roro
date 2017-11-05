using System;
using System.Windows.Forms;

namespace Roro.Workflow
{
    public partial class Page
    {
        private void NodeSelectRequest(object sender, MouseEventArgs e)
        {
            this.control.MouseMove += NodeSelectCancel;
            this.control.MouseUp += NodeSelectStart;
        }

        private void NodeSelectCancel(object sender, MouseEventArgs e)
        {
            this.control.MouseMove -= NodeSelectCancel;
            this.control.MouseUp -= NodeSelectStart;
        }

        private void NodeSelectStart(object sender, MouseEventArgs e)
        {
            this.control.MouseMove -= NodeSelectCancel;
            this.control.MouseUp -= NodeSelectStart;
            //
            var nodeId = this.GetNodeIdFromPoint(e.X, e.Y);
            var ctrlDown = Control.ModifierKeys.HasFlag(Keys.Control);
            if (nodeId == Guid.Empty)
            {
                if (ctrlDown)
                {
                    ;
                }
                else
                {
                    this.SelectedNodes.Clear();
                }

            }
            else if (this.SelectedNodes.Contains(nodeId))
            {
                if (ctrlDown)
                {
                    this.SelectedNodes.Remove(nodeId);
                }
                else
                {
                    this.SelectedNodes.Clear();
                }
            }
            else
            {
                if (ctrlDown)
                {
                    ;
                }
                else
                {
                    this.SelectedNodes.Clear();
                }
                this.SelectedNodes.Add(nodeId);
            }
            this.control.Invalidate();
        }
    }
}