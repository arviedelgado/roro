
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Roro.Activities
{
    public partial class Page
    {
        private Port LinkNodeStartPort { get; set; }

        private Point LinkNodeEndPoint { get; set; }

        private Point MoveNodeStartPoint { get; set; }

        private Point MoveNodeOffsetPoint { get; set; }

        private Point SelectNodeStartPoint { get; set; }

        private Rectangle SelectNodeRect { get; set; }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            // Focus the Canvas to allow KeyEvents
            // Handle the scrollbar-resets-on-focus
            var pagePanel = this.Canvas.Parent as Panel;
            var pagePanelScrollPosition = pagePanel.AutoScrollPosition;
            this.Canvas.Focus();
            pagePanel.AutoScrollPosition = new Point(-pagePanelScrollPosition.X, -pagePanelScrollPosition.Y);

            // Handle Double Click
            this.Canvas.MouseDoubleClick -= Canvas_MouseDoubleClick;
            this.Canvas.MouseDoubleClick += Canvas_MouseDoubleClick;

            // Pick Node from Point
            this.Canvas.MouseMove += this.SelectNodeFromPointCancel;
            this.Canvas.MouseUp += this.SelectNodeFromPointEnd;
            if (this.GetNodeFromPoint(e.Location) is Node node)
            {
                if (node.GetPortFromPoint(e.Location) is Port port)
                {
                    // Link Nodes
                    if (node.CanStartLink)
                    {
                        this.Canvas.MouseMove += this.LinkNodeStart;
                        this.Canvas.MouseUp += this.LinkNodeCancel;
                        this.LinkNodeStartPort = port;
                        this.LinkNodeEndPoint = Point.Empty;
                    }
                }
                else
                {
                    // Move Nodes
                    this.Canvas.MouseMove += this.MoveNodeStart;
                    this.Canvas.MouseUp += this.MoveNodeCancel;
                    this.MoveNodeStartPoint = e.Location;
                    this.MoveNodeOffsetPoint = Point.Empty;
                }
            }
            else
            {
                // Pick Nodes from Rectangle
                this.Canvas.MouseMove += this.SelectNodesFromRectStart;
                this.Canvas.MouseUp += this.SelectNodesFromRectCancel;
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

        private void Canvas_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(TreeNode).FullName) is TreeNode treeNode)
            {
                var canvas = sender as Control;
                var location = canvas.PointToClient(new Point(e.X, e.Y));
                this.AddNode(treeNode.Name, location.X, location.Y);
                this.Canvas.Invalidate();
            }
        }

        private void Canvas_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        #region Link Nodes

        private void LinkNodeCancel(object sender, MouseEventArgs e)
        {
            this.Canvas.MouseMove -= this.LinkNodeStart;
            this.Canvas.MouseUp -= this.LinkNodeCancel;
        }

        private void LinkNodeStart(object sender, MouseEventArgs e)
        {
            this.Canvas.MouseMove -= this.LinkNodeStart;
            this.Canvas.MouseUp -= this.LinkNodeCancel;
            this.Canvas.MouseMove += this.LinkingNode;
            this.Canvas.MouseUp += this.LinkNodeEnd;
        }

        private void LinkingNode(object sender, MouseEventArgs e)
        {
            this.LinkNodeEndPoint = e.Location;
            this.Canvas.Invalidate();
        }

        private void LinkNodeEnd(object sender, MouseEventArgs e)
        {
            this.Canvas.MouseMove -= this.LinkingNode;
            this.Canvas.MouseUp -= this.LinkNodeEnd;
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
            this.Canvas.Invalidate();
        }

        #endregion

        #region Move Nodes

        private void MoveNodeCancel(object sender, MouseEventArgs e)
        {
            this.Canvas.MouseMove -= this.MoveNodeStart;
            this.Canvas.MouseUp -= this.MoveNodeCancel;
        }

        private void MoveNodeStart(object sender, MouseEventArgs e)
        {
            this.Canvas.Cursor = Cursors.SizeAll;
            this.Canvas.MouseMove -= this.MoveNodeStart;
            this.Canvas.MouseUp -= this.MoveNodeCancel;
            this.Canvas.MouseMove += this.MovingNode;
            this.Canvas.MouseUp += this.MoveNodeEnd;
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
            this.Canvas.Invalidate();
        }

        private void MovingNode(object sender, MouseEventArgs e)
        {
            var offsetX = e.X - this.MoveNodeStartPoint.X;
            var offsetY = e.Y - this.MoveNodeStartPoint.Y;
            this.MoveNodeOffsetPoint = new Point(offsetX, offsetY);
            this.Canvas.Invalidate();
        }

        private void MoveNodeEnd(object sender, MouseEventArgs e)
        {
            this.Canvas.Cursor = Cursors.Default;
            this.Canvas.MouseMove -= this.MovingNode;
            this.Canvas.MouseUp -= this.MoveNodeEnd;
            //
            var offsetX = (int)Math.Round((double)(e.X - this.MoveNodeStartPoint.X) / PageRenderOptions.GridSize) * PageRenderOptions.GridSize;
            var offsetY = (int)Math.Round((double)(e.Y - this.MoveNodeStartPoint.Y) / PageRenderOptions.GridSize) * PageRenderOptions.GridSize;
            this.MoveNodeOffsetPoint = new Point(offsetX, offsetY);
            foreach (var node in this.SelectedNodes)
            {
                var rect = node.Bounds;
                rect.X += this.MoveNodeOffsetPoint.X;
                rect.Y += this.MoveNodeOffsetPoint.Y;
                node.SetBounds(rect);
            }
            this.MoveNodeOffsetPoint = Point.Empty;
            this.Canvas.Invalidate();
        }

        #endregion

        #region Select Node From Point

        private void SelectNodeFromPointCancel(object sender, MouseEventArgs e)
        {
            this.Canvas.MouseMove -= this.SelectNodeFromPointCancel;
            this.Canvas.MouseUp -= this.SelectNodeFromPointEnd;
        }

        private void SelectNodeFromPointEnd(object sender, MouseEventArgs e)
        {
            this.Canvas.MouseMove -= this.SelectNodeFromPointCancel;
            this.Canvas.MouseUp -= this.SelectNodeFromPointEnd;
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
            this.Canvas.Invalidate();
        }

        #endregion

        #region Select Nodes From Rect

        private void SelectNodesFromRectCancel(object sender, MouseEventArgs e)
        {
            this.Canvas.MouseMove -= this.SelectNodesFromRectStart;
            this.Canvas.MouseUp -= this.SelectNodesFromRectCancel;
        }

        private void SelectNodesFromRectStart(object sender, MouseEventArgs e)
        {
            this.Canvas.Cursor = Cursors.Cross;
            this.Canvas.MouseMove -= this.SelectNodesFromRectStart;
            this.Canvas.MouseUp -= this.SelectNodesFromRectCancel;
            this.Canvas.MouseMove += this.SelectingNodesFromRect;
            this.Canvas.MouseUp += this.SelectNodesFromRectEnd;
            //
            this.SelectNodeStartPoint = e.Location;
            this.SelectNodeRect = Rectangle.Empty;
            this.Canvas.Invalidate();
        }

        private void SelectingNodesFromRect(object sender, MouseEventArgs e)
        {
            var x = Math.Min(this.SelectNodeStartPoint.X, e.X);
            var y = Math.Min(this.SelectNodeStartPoint.Y, e.Y);
            var w = Math.Abs(this.SelectNodeStartPoint.X - e.X);
            var h = Math.Abs(this.SelectNodeStartPoint.Y - e.Y);
            this.SelectNodeRect = new Rectangle(x, y, w, h);
            this.Canvas.Invalidate();
        }

        private void SelectNodesFromRectEnd(object sender, MouseEventArgs e)
        {
            this.Canvas.Cursor = Cursors.Default;
            this.Canvas.MouseMove -= this.SelectingNodesFromRect;
            this.Canvas.MouseUp -= this.SelectNodesFromRectEnd;
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
            this.Canvas.Invalidate();
        }

        #endregion

    }
}