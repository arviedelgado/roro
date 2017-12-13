using Roro.Activities;
using System;
using System.ComponentModel;
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
            this.canvas.Focus();

            this.canvas.MouseDoubleClick -= Canvas_MouseDoubleClick;
            this.canvas.MouseDoubleClick += Canvas_MouseDoubleClick;

            // Pick Node from Point
            this.canvas.MouseMove += this.SelectNodeFromPointCancel;
            this.canvas.MouseUp += this.SelectNodeFromPointEnd;
            if (this.GetNodeFromPoint(e.Location) is Node node)
            {
                if (node.GetPortFromPoint(e.Location) is Port port)
                {
                    // Link Nodes
                    if (node.CanStartLink)
                    {
                        this.canvas.MouseMove += this.LinkNodeStart;
                        this.canvas.MouseUp += this.LinkNodeCancel;
                        this.LinkNodeStartPort = port;
                        this.LinkNodeEndPoint = Point.Empty;
                    }
                }
                else
                {
                    // Move Nodes
                    this.canvas.MouseMove += this.MoveNodeStart;
                    this.canvas.MouseUp += this.MoveNodeCancel;
                    this.MoveNodeStartPoint = e.Location;
                    this.MoveNodeOffsetPoint = Point.Empty;
                }
            }
            else
            {
                // Pick Nodes from Rectangle
                this.canvas.MouseMove += this.SelectNodesFromRectStart;
                this.canvas.MouseUp += this.SelectNodesFromRectCancel;
                this.SelectNodeStartPoint = e.Location;
                this.SelectNodeRect = Rectangle.Empty;
            }
        }

        private void Canvas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.GetNodeFromPoint(e.Location) is Node node)
            {
                using (var f = new NodeForm(this, node))
                {
                    f.ShowDialog();
                }
            }
        }

        private void DragDropEvent(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(TreeNode).FullName) is TreeNode treeNode)
            {
                var canvas = sender as Control;
                var node = this.AddNode(treeNode.Name);
                var rect = node.Bounds;
                rect.Location = canvas.PointToClient(new Point(e.X, e.Y));
                rect.Offset(-rect.Width / 2, -rect.Height / 2);
                node.SetBounds(rect);
                this.canvas.Invalidate();
            }
        }

        private void DragEnterEvent(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        #region Link Nodes

        private void LinkNodeCancel(object sender, MouseEventArgs e)
        {
            this.canvas.MouseMove -= this.LinkNodeStart;
            this.canvas.MouseUp -= this.LinkNodeCancel;
        }

        private void LinkNodeStart(object sender, MouseEventArgs e)
        {
            this.canvas.MouseMove -= this.LinkNodeStart;
            this.canvas.MouseUp -= this.LinkNodeCancel;
            this.canvas.MouseMove += this.LinkingNode;
            this.canvas.MouseUp += this.LinkNodeEnd;
        }

        private void LinkingNode(object sender, MouseEventArgs e)
        {
            this.LinkNodeEndPoint = e.Location;
            this.canvas.Invalidate();
        }

        private void LinkNodeEnd(object sender, MouseEventArgs e)
        {
            this.canvas.MouseMove -= this.LinkingNode;
            this.canvas.MouseUp -= this.LinkNodeEnd;
            this.LinkNodeEndPoint = Point.Empty;
            //
            if (this.GetNodeFromPoint(e.Location) is Node node && node.CanEndLink)
            {
                this.LinkNodeStartPort.NextNodeId = node.Id;
            }
            else
            {
                this.LinkNodeStartPort.NextNodeId = Guid.Empty;
            }
            this.canvas.Invalidate();
        }

        #endregion

        #region Move Nodes

        private void MoveNodeCancel(object sender, MouseEventArgs e)
        {
            this.canvas.MouseMove -= this.MoveNodeStart;
            this.canvas.MouseUp -= this.MoveNodeCancel;
        }

        private void MoveNodeStart(object sender, MouseEventArgs e)
        {
            this.canvas.Cursor = Cursors.SizeAll;
            this.canvas.MouseMove -= this.MoveNodeStart;
            this.canvas.MouseUp -= this.MoveNodeCancel;
            this.canvas.MouseMove += this.MovingNode;
            this.canvas.MouseUp += this.MoveNodeEnd;
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
            this.canvas.Invalidate();
        }

        private void MovingNode(object sender, MouseEventArgs e)
        {
            var offsetX = e.X - this.MoveNodeStartPoint.X;
            var offsetY = e.Y - this.MoveNodeStartPoint.Y;
            this.MoveNodeOffsetPoint = new Point(offsetX, offsetY);
            this.canvas.Invalidate();
        }

        private void MoveNodeEnd(object sender, MouseEventArgs e)
        {
            this.canvas.Cursor = Cursors.Default;
            this.canvas.MouseMove -= this.MovingNode;
            this.canvas.MouseUp -= this.MoveNodeEnd;
            //
            var offsetX = (int)Math.Round((double)(e.X - this.MoveNodeStartPoint.X) / PageRenderOptions.GridSize) * PageRenderOptions.GridSize;
            var offsetY = (int)Math.Round((double)(e.Y - this.MoveNodeStartPoint.Y) / PageRenderOptions.GridSize) * PageRenderOptions.GridSize;
            this.MoveNodeOffsetPoint = new Point(offsetX, offsetY);
            foreach (var node in this.SelectedNodes)
            {
                var rect = node.Bounds;
                rect.Offset(this.MoveNodeOffsetPoint);
                node.SetBounds(rect);
            }
            this.MoveNodeOffsetPoint = Point.Empty;
            this.canvas.Invalidate();
        }

        #endregion

        #region Select Node From Point

        private void SelectNodeFromPointCancel(object sender, MouseEventArgs e)
        {
            this.canvas.MouseMove -= this.SelectNodeFromPointCancel;
            this.canvas.MouseUp -= this.SelectNodeFromPointEnd;
        }

        private void SelectNodeFromPointEnd(object sender, MouseEventArgs e)
        {
            this.canvas.MouseMove -= this.SelectNodeFromPointCancel;
            this.canvas.MouseUp -= this.SelectNodeFromPointEnd;
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
            this.canvas.Invalidate();
        }

        #endregion

        #region Select Nodes From Rect

        private void SelectNodesFromRectCancel(object sender, MouseEventArgs e)
        {
            this.canvas.MouseMove -= this.SelectNodesFromRectStart;
            this.canvas.MouseUp -= this.SelectNodesFromRectCancel;
        }

        private void SelectNodesFromRectStart(object sender, MouseEventArgs e)
        {
            this.canvas.Cursor = Cursors.Cross;
            this.canvas.MouseMove -= this.SelectNodesFromRectStart;
            this.canvas.MouseUp -= this.SelectNodesFromRectCancel;
            this.canvas.MouseMove += this.SelectingNodesFromRect;
            this.canvas.MouseUp += this.SelectNodesFromRectEnd;
            //
            this.SelectNodeStartPoint = e.Location;
            this.SelectNodeRect = Rectangle.Empty;
            this.canvas.Invalidate();
        }

        private void SelectingNodesFromRect(object sender, MouseEventArgs e)
        {
            var x = Math.Min(this.SelectNodeStartPoint.X, e.X);
            var y = Math.Min(this.SelectNodeStartPoint.Y, e.Y);
            var w = Math.Abs(this.SelectNodeStartPoint.X - e.X);
            var h = Math.Abs(this.SelectNodeStartPoint.Y - e.Y);
            this.SelectNodeRect = new Rectangle(x, y, w, h);
            this.canvas.Invalidate();
        }

        private void SelectNodesFromRectEnd(object sender, MouseEventArgs e)
        {
            this.canvas.Cursor = Cursors.Default;
            this.canvas.MouseMove -= this.SelectingNodesFromRect;
            this.canvas.MouseUp -= this.SelectNodesFromRectEnd;
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
            this.canvas.Invalidate();
        }

        #endregion

    }
}