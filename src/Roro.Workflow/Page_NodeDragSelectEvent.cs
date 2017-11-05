using System;
using System.Drawing;
using System.Windows.Forms;

namespace Roro.Workflow
{
    public partial class Page
    {
        private Point NodeDragSelectStartPoint { get; set; }

        private Rectangle NodeDragSelectRectangle { get; set; }

        private void NodeDragSelectRequest(object sender, MouseEventArgs e)
        {
            if (this.GetNodeById(this.GetNodeIdFromPoint(e.X, e.Y)) is Node node)
            {
                // NodeDragRequest
            }
            else
            {
                this.control.MouseMove += NodeDragSelectStart;
                this.control.MouseUp += NodeDragSelectCancel;
            }
        }

        private void NodeDragSelectCancel(object sender, MouseEventArgs e)
        {
            this.control.MouseMove -= NodeDragSelectStart;
            this.control.MouseUp -= NodeDragSelectCancel;
        }

        private void NodeDragSelectStart(object sender, MouseEventArgs e)
        {
            this.control.MouseMove -= NodeDragSelectStart;
            this.control.MouseUp -= NodeDragSelectCancel;
            this.control.MouseMove += NodeDragSelecting;
            this.control.MouseUp += NodeDragSelectEnd;
            this.NodeDragSelectStartPoint = e.Location;
            this.NodeDragSelectRectangle = Rectangle.Empty;
        }

        private void NodeDragSelecting(object sender, MouseEventArgs e)
        {
            var x = Math.Min(this.NodeDragSelectStartPoint.X, e.X);
            var y = Math.Min(this.NodeDragSelectStartPoint.Y, e.Y);
            var w = Math.Abs(this.NodeDragSelectStartPoint.X - e.X);
            var h = Math.Abs(this.NodeDragSelectStartPoint.Y - e.Y);
            this.NodeDragSelectRectangle = new Rectangle(x, y, w, h);
            this.control.Invalidate();
        }

        private void NodeDragSelectEnd(object sender, MouseEventArgs e)
        {
            this.control.MouseMove -= NodeDragSelecting;
            this.control.MouseUp -= NodeDragSelectEnd;
            //
            if (!Control.ModifierKeys.HasFlag(Keys.Control))
            {
                this.SelectedNodes.Clear();
            }
            foreach (var node in this.Nodes)
            {
                if (!this.SelectedNodes.Contains(node.Id) &&
                     this.NodeDragSelectRectangle.IntersectsWith(node.Bounds))
                {
                    this.SelectedNodes.Add(node.Id);
                }
            }
            this.NodeDragSelectRectangle = Rectangle.Empty;
            this.control.Invalidate();
        }
    }
}