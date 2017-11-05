using System;
using System.Drawing;
using System.Windows.Forms;

namespace Roro.Workflow
{
    public partial class Page
    {
        private Point DragNodeStartPoint { get; set; }

        private Point DragNodeOffsetPoint { get; set; }

        private Point SelectNodeStartPoint { get; set; }

        private Rectangle SelectNodeRect { get; set; }

        private void MouseEvents(object sender, MouseEventArgs e)
        {
            // Select From Point
            this.control.MouseMove += this.SelectNodeFromPointCancel;
            this.control.MouseUp += this.SelectNodeFromPointEnd;
            if (this.GetNodeById(this.GetNodeIdFromPoint(e.Location)) is Node node)
            {
                // Drag
                this.control.MouseMove += this.DragNodeStart;
                this.control.MouseUp += this.DragNodeCancel;
                this.DragNodeStartPoint = e.Location;
                this.DragNodeOffsetPoint = Point.Empty;
            }
            else
            {
                // Select From Rect
                this.control.MouseMove += this.SelectNodesFromRectStart;
                this.control.MouseUp += this.SelectNodesFromRectCancel;
                this.SelectNodeStartPoint = e.Location;
                this.SelectNodeRect = Rectangle.Empty;
            }
        }

        #region Drag Nodes

        private void DragNodeCancel(object sender, MouseEventArgs e)
        {
            this.control.MouseMove -= this.DragNodeStart;
            this.control.MouseUp -= this.DragNodeCancel;
        }

        private void DragNodeStart(object sender, MouseEventArgs e)
        {
            this.control.Cursor = Cursors.SizeAll;
            this.control.MouseMove -= this.DragNodeStart;
            this.control.MouseUp -= this.DragNodeCancel;
            this.control.MouseMove += this.DraggingNode;
            this.control.MouseUp += this.DragNodeEnd;
            //
            var nodeId = this.GetNodeIdFromPoint(this.DragNodeStartPoint);
            if (this.SelectedNodes.Contains(nodeId))
            {
                ;
            }
            else
            {
                this.SelectedNodes.Clear();
                this.SelectedNodes.Add(nodeId);
            }
            this.control.Invalidate();
        }

        private void DraggingNode(object sender, MouseEventArgs e)
        {
            var offsetX = e.X - this.DragNodeStartPoint.X;
            var offsetY = e.Y - this.DragNodeStartPoint.Y;
            this.DragNodeOffsetPoint = new Point(offsetX, offsetY);
            this.control.Invalidate();
        }

        private void DragNodeEnd(object sender, MouseEventArgs e)
        {
            this.control.Cursor = Cursors.Default;
            this.control.MouseMove -= this.DraggingNode;
            this.control.MouseUp -= this.DragNodeEnd;
            //
            var offsetX = (int)Math.Round((double)(e.X - this.DragNodeStartPoint.X) / PageRenderOptions.GridSize) * PageRenderOptions.GridSize;
            var offsetY = (int)Math.Round((double)(e.Y - this.DragNodeStartPoint.Y) / PageRenderOptions.GridSize) * PageRenderOptions.GridSize;
            this.DragNodeOffsetPoint = new Point(offsetX, offsetY);
            foreach (var nodeId in this.SelectedNodes)
            {
                var node = this.GetNodeById(nodeId);
                var rect = node.Bounds;
                rect.Offset(this.DragNodeOffsetPoint);
                node.Bounds = rect;
            }
            this.DragNodeOffsetPoint = Point.Empty;
            this.control.Invalidate();
        }

        #endregion

        #region Select Node From Point

        private void SelectNodeFromPointCancel(object sender, MouseEventArgs e)
        {
            this.control.MouseMove -= this.SelectNodeFromPointCancel;
            this.control.MouseUp -= this.SelectNodeFromPointEnd;
        }

        private void SelectNodeFromPointEnd(object sender, MouseEventArgs e)
        {
            this.control.MouseMove -= this.SelectNodeFromPointCancel;
            this.control.MouseUp -= this.SelectNodeFromPointEnd;
            //
            var nodeId = this.GetNodeIdFromPoint(e.Location);
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

        #endregion

        #region Select Nodes From Rect

        private void SelectNodesFromRectCancel(object sender, MouseEventArgs e)
        {
            this.control.MouseMove -= this.SelectNodesFromRectStart;
            this.control.MouseUp -= this.SelectNodesFromRectCancel;
        }

        private void SelectNodesFromRectStart(object sender, MouseEventArgs e)
        {
            this.control.Cursor = Cursors.Cross;
            this.control.MouseMove -= this.SelectNodesFromRectStart;
            this.control.MouseUp -= this.SelectNodesFromRectCancel;
            this.control.MouseMove += this.SelectingNodesFromRect;
            this.control.MouseUp += this.SelectNodesFromRectEnd;
            //
            this.SelectNodeStartPoint = e.Location;
            this.SelectNodeRect = Rectangle.Empty;
            this.control.Invalidate();
        }

        private void SelectingNodesFromRect(object sender, MouseEventArgs e)
        {
            var x = Math.Min(this.SelectNodeStartPoint.X, e.X);
            var y = Math.Min(this.SelectNodeStartPoint.Y, e.Y);
            var w = Math.Abs(this.SelectNodeStartPoint.X - e.X);
            var h = Math.Abs(this.SelectNodeStartPoint.Y - e.Y);
            this.SelectNodeRect = new Rectangle(x, y, w, h);
            this.control.Invalidate();
        }

        private void SelectNodesFromRectEnd(object sender, MouseEventArgs e)
        {
            this.control.Cursor = Cursors.Default;
            this.control.MouseMove -= this.SelectingNodesFromRect;
            this.control.MouseUp -= this.SelectNodesFromRectEnd;
            //
            if (!Control.ModifierKeys.HasFlag(Keys.Control))
            {
                this.SelectedNodes.Clear();
            }
            foreach (var node in this.Nodes)
            {
                if (!this.SelectedNodes.Contains(node.Id) &&
                     this.SelectNodeRect.IntersectsWith(node.Bounds))
                {
                    this.SelectedNodes.Add(node.Id);
                }
            }
            this.SelectNodeRect = Rectangle.Empty;
            this.control.Invalidate();
        }

        #endregion
    }
}