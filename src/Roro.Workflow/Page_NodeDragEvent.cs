using System;
using System.Drawing;
using System.Windows.Forms;

namespace Roro.Workflow
{
    public partial class Page
    {
        private Point NodeDragStartPoint { get; set; }

        private Point NodeDragOffsetPoint { get; set; }

        private void NodeDragRequest(object sender, MouseEventArgs e)
        {
            if (this.GetNodeById(this.GetNodeIdFromPoint(e.X, e.Y)) is Node node)
            {
                this.control.MouseMove += NodeDragStart;
                this.control.MouseUp += NodeDragCancel;
            }
            else
            {
                // NodeDragSelectRequest
            }
        }

        private void NodeDragCancel(object sender, MouseEventArgs e)
        {
            this.control.MouseMove -= NodeDragStart;
            this.control.MouseUp -= NodeDragCancel;
        }

        private void NodeDragStart(object sender, MouseEventArgs e)
        {
            this.control.MouseMove -= NodeDragStart;
            this.control.MouseUp -= NodeDragCancel;
            this.control.MouseMove += NodeDragging;
            this.control.MouseUp += NodeDragEnd;
            //
            this.NodeDragStartPoint = e.Location;
            this.NodeDragOffsetPoint = Point.Empty;
            var nodeId = this.GetNodeIdFromPoint(e.X, e.Y);
            if (this.SelectedNodes.Contains(nodeId))
            {
                ;
            }
            else
            {
                this.SelectedNodes.Clear();
                this.SelectedNodes.Add(nodeId);
            }
        }

        private void NodeDragging(object sender, MouseEventArgs e)
        {
            var offsetX = (int)Math.Round((double)(e.X - this.NodeDragStartPoint.X) / PageRenderOptions.GridSize) * PageRenderOptions.GridSize;
            var offsetY = (int)Math.Round((double)(e.Y - this.NodeDragStartPoint.Y) / PageRenderOptions.GridSize) * PageRenderOptions.GridSize;
            this.NodeDragOffsetPoint = new Point(offsetX, offsetY);
            this.control.Invalidate();
        }

        private void NodeDragEnd(object sender, MouseEventArgs e)
        {
            this.control.MouseMove -= NodeDragging;
            this.control.MouseUp -= NodeDragEnd;
            //
            foreach (var nodeId in this.SelectedNodes)
            {
                var node = this.GetNodeById(nodeId);
                var rect = node.Bounds;
                rect.Offset(this.NodeDragOffsetPoint);
                node.Bounds = rect;
            }
            this.NodeDragOffsetPoint = Point.Empty;
            this.control.Invalidate();
        }
    }
}