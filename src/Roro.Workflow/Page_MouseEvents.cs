using System;
using System.Drawing;
using System.Windows.Forms;

namespace Roro.Workflow
{
    public partial class Page
    {
        private Port LinkNodeStartPort { get; set; }

        private Point LinkNodeEndPoint { get; set; }

        private Point MoveNodeStartPoint { get; set; }

        private Point MoveNodeOffsetPoint { get; set; }

        private Point SelectNodeStartPoint { get; set; }

        private Rectangle SelectNodeRect { get; set; }

        private void MouseEvents(object sender, MouseEventArgs e)
        {
            // Pick Node from Point
            this.control.MouseMove += this.SelectNodeFromPointCancel;
            this.control.MouseUp += this.SelectNodeFromPointEnd;
            if (this.GetNodeFromPoint(e.Location) is Node node)
            {
                if (node.GetPortFromPoint(e.Location) is Port port)
                {
                    // Link Nodes
                    this.control.MouseMove += this.LinkNodeStart;
                    this.control.MouseUp += this.LinkNodeCancel;
                    this.LinkNodeStartPort = port;
                    this.LinkNodeEndPoint = Point.Empty;
                }
                else
                {
                    // Move Nodes
                    this.control.MouseMove += this.MoveNodeStart;
                    this.control.MouseUp += this.MoveNodeCancel;
                    this.MoveNodeStartPoint = e.Location;
                    this.MoveNodeOffsetPoint = Point.Empty;
                }
            }
            else
            {
                // Pick Nodes from Rectangle
                this.control.MouseMove += this.SelectNodesFromRectStart;
                this.control.MouseUp += this.SelectNodesFromRectCancel;
                this.SelectNodeStartPoint = e.Location;
                this.SelectNodeRect = Rectangle.Empty;
            }
        }

        #region Link Nodes
        
        private void LinkNodeCancel(object sender, MouseEventArgs e)
        {
            this.control.MouseMove -= this.LinkNodeStart;
            this.control.MouseUp -= this.LinkNodeCancel;
        }

        private void LinkNodeStart(object sender, MouseEventArgs e)
        {
            this.control.MouseMove -= this.LinkNodeStart;
            this.control.MouseUp -= this.LinkNodeCancel;
            this.control.MouseMove += this.LinkingNode;
            this.control.MouseUp += this.LinkNodeEnd;
        }

        private void LinkingNode(object sender, MouseEventArgs e)
        {
            this.LinkNodeEndPoint = e.Location;
            this.control.Invalidate();
        }

        private void LinkNodeEnd(object sender, MouseEventArgs e)
        {
            this.control.MouseMove -= this.LinkingNode;
            this.control.MouseUp -= this.LinkNodeEnd;
            this.LinkNodeEndPoint = Point.Empty;
            //
            if (this.GetNodeFromPoint(e.Location) is Node node)
            {
                if (this.LinkNodeStartPort.Id != node.Id)
                {
                    this.LinkNodeStartPort.NextNodeId = node.Id;
                }
            }
            else
            {
                this.LinkNodeStartPort.NextNodeId = Guid.Empty;
            }
            this.control.Invalidate();
        }

        #endregion

        #region Move Nodes

        private void MoveNodeCancel(object sender, MouseEventArgs e)
        {
            this.control.MouseMove -= this.MoveNodeStart;
            this.control.MouseUp -= this.MoveNodeCancel;
        }

        private void MoveNodeStart(object sender, MouseEventArgs e)
        {
            this.control.Cursor = Cursors.SizeAll;
            this.control.MouseMove -= this.MoveNodeStart;
            this.control.MouseUp -= this.MoveNodeCancel;
            this.control.MouseMove += this.MovingNode;
            this.control.MouseUp += this.MoveNodeEnd;
            //
            var nodeId = this.GetNodeFromPoint(this.MoveNodeStartPoint);
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

        private void MovingNode(object sender, MouseEventArgs e)
        {
            var offsetX = e.X - this.MoveNodeStartPoint.X;
            var offsetY = e.Y - this.MoveNodeStartPoint.Y;
            this.MoveNodeOffsetPoint = new Point(offsetX, offsetY);
            this.control.Invalidate();
        }

        private void MoveNodeEnd(object sender, MouseEventArgs e)
        {
            this.control.Cursor = Cursors.Default;
            this.control.MouseMove -= this.MovingNode;
            this.control.MouseUp -= this.MoveNodeEnd;
            //
            var offsetX = (int)Math.Round((double)(e.X - this.MoveNodeStartPoint.X) / PageRenderOptions.GridSize) * PageRenderOptions.GridSize;
            var offsetY = (int)Math.Round((double)(e.Y - this.MoveNodeStartPoint.Y) / PageRenderOptions.GridSize) * PageRenderOptions.GridSize;
            this.MoveNodeOffsetPoint = new Point(offsetX, offsetY);
            foreach (var node in this.SelectedNodes)
            {
                var rect = node.Bounds;
                rect.Offset(this.MoveNodeOffsetPoint);
                node.Bounds = rect;
            }
            this.MoveNodeOffsetPoint = Point.Empty;
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
            var node = this.GetNodeFromPoint(e.Location);
            var ctrl = Control.ModifierKeys.HasFlag(Keys.Control);
            if (node == null)
            {
                if (ctrl)
                {
                    ;
                }
                else
                {
                    this.SelectedNodes.Clear();
                }

            }
            else if (this.SelectedNodes.Contains(node))
            {
                if (ctrl)
                {
                    this.SelectedNodes.Remove(node);
                }
                else
                {
                    this.SelectedNodes.Clear();
                }
            }
            else
            {
                if (ctrl)
                {
                    ;
                }
                else
                {
                    this.SelectedNodes.Clear();
                }
                this.SelectedNodes.Add(node);
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
                if (!this.SelectedNodes.Contains(node) &&
                     this.SelectNodeRect.IntersectsWith(node.Bounds))
                {
                    this.SelectedNodes.Add(node);
                }
            }
            this.SelectNodeRect = Rectangle.Empty;
            this.control.Invalidate();
        }

        #endregion
    }
}