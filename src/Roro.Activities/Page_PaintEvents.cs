using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Roro.Activities
{
    public partial class Page
    {
        internal void Render()
        {
            this.canvas.Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            //Console.Clear();

            //Console.WriteLine();
            //Console.WriteLine(DataContractSerializerHelper.ToString(this));
            //Console.WriteLine();

            var total = Stopwatch.StartNew();

            var c = sender as Control;
            var g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            var sw = Stopwatch.StartNew();
            this.RenderBackground(g, c.ClientRectangle);
            //Console.WriteLine("Render Back\t{0}", sw.ElapsedMilliseconds / 1000.0);

            sw.Restart();
            this.RenderNodes(g);
            //Console.WriteLine("Render Nodes\t{0}", sw.ElapsedMilliseconds / 1000.0);

            sw.Restart();
            this.RenderLines(g);
            //Console.WriteLine("Render Lines\t{0}", sw.ElapsedMilliseconds / 1000.0);

            sw.Restart();
            this.RenderPorts(g);
            //Console.WriteLine("Render Ports\t{0}", sw.ElapsedMilliseconds / 1000.0);

            if (this.SelectNodeRect != Rectangle.Empty)
            {
                e.Graphics.FillRectangle(PageRenderOptions.SelectionBackBrush, this.SelectNodeRect);
            }

            //Console.WriteLine("Render Total\t{0}", total.ElapsedMilliseconds / 1000.0);
            //Console.WriteLine("Render Total\t{0:#.00} fps", 1000.0 / total.ElapsedMilliseconds);           
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
            var width = this.canvas.Parent.ClientSize.Width;
            var height = this.canvas.Parent.ClientSize.Height;

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

            this.canvas.Size = new Size(width, height);
        }
    }
}