using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Roro.Activities
{
    public partial class Page
    {
        public void Show(Panel parent)
        {
            parent.Controls.Clear();
            this.Canvas.Parent = parent;
            this.Canvas.Invalidate();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            this.RenderBackground(g, (sender as Control).ClientRectangle);
            
            this.RenderNodes(g);
            this.RenderLines(g);
            this.RenderPorts(g);

            if (this.SelectNodeRect != Rectangle.Empty)
            {
                e.Graphics.FillRectangle(PageRenderOptions.SelectionBackBrush, this.SelectNodeRect);
            }
        }

        private void RenderBackground(Graphics g, Rectangle r)
        {
            g.Clear(PageRenderOptions.BackColor);
            for (var y = 0; y < r.Height; y += PageRenderOptions.GridSize)
            {
                g.DrawLine(PageRenderOptions.GridPen, 0, y, r.Width, y);
            }
            for (var x = 0; x < r.Width; x += PageRenderOptions.GridSize)
            {
                g.DrawLine(PageRenderOptions.GridPen, x, 0, x, r.Height);
            }
        }

        private void RenderPorts(Graphics g)
        {
            if (this.MoveNodeOffsetPoint == Point.Empty)
            {
                var o = new NodeStyle();
                foreach (var node in this.SelectedNodes)
                {
                    node.RenderedPorts.Clear();
                    var nodePath = this.RenderedNodes[node];
                    foreach (var port in node.Ports)
                    {
                        var portPath = port.Render(g, node.Bounds, o);
                        node.RenderedPorts.Add(port, portPath);
                        nodePath.FillMode = FillMode.Winding;
                        nodePath.AddPath(portPath, false);
                    }
                }
            }
        }

        private void RenderLines(Graphics g)
        {
            var o = new NodeStyle();
            var f = new PathFinder(this.Nodes);
            foreach (var node in this.Nodes)
            {
                foreach (var port in node.Ports)
                {
                    o.LinePenWithArrow.Brush = port.GetBackBrush();
                    if (this.LinkNodeEndPoint != Point.Empty && this.LinkNodeStartPort == port)
                    {
                        g.DrawLine(o.LinePenWithArrow, port.Bounds.Center().X, port.Bounds.Center().Y, this.LinkNodeEndPoint.X, this.LinkNodeEndPoint.Y);
                    }
                    else if (this.GetNodeById(port.NextNodeId) is Node linkedNode)
                    {
                        g.DrawPath(o.LinePenWithArrow, f.GetPath(port.Bounds.Center(), linkedNode.Bounds.CenterTop()));
                    }
                }
            }
        }

        private void RenderNodes(Graphics g)
        {
            var width = this.Canvas.Parent.ClientSize.Width;
            var height = this.Canvas.Parent.ClientSize.Height;

            this.RenderedNodes.Clear();
            foreach (var node in this.Nodes)
            {
                var r = node.Bounds;
                var o = new NodeStyle();
                if (this.SelectedNodes.Contains(node))
                {
                    o.BorderPen = PageRenderOptions.SelectedNodeBorderPen;
                    r.Offset(this.MoveNodeOffsetPoint);
                }
                if (this.DebugNode == node)
                {
                    o.BackBrush = PageRenderOptions.DebugNodeBackBrush;
                }
                this.RenderedNodes.Add(node, node.Render(g, r, o));
                node.RenderText(g, r, o);

                // update width and height
                width = Math.Max(width, r.Right + PageRenderOptions.GridSize * 5);
                height = Math.Max(height, r.Bottom + PageRenderOptions.GridSize * 5);
            }

            this.Canvas.Size = new Size(width, height);
        }
    }
}