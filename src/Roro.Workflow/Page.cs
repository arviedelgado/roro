using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Roro.Workflow
{
    [DataContract]
    public partial class Page
    {
        [DataMember]
        public Guid Id { get; private set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        private List<Node> Nodes { get; set; }

        private HashSet<Node> SelectedNodes { get; set; }

        private Dictionary<Node, SKPath> RenderedNodes { get; }

        public Page()
        {
            this.Id = Guid.NewGuid();
            this.Name = string.Format("My{0}_{1}", this.GetType().Name, DateTime.Now.Ticks);
            this.Nodes = new List<Node>();
            this.AddNode<StartNode>();
            this.SelectedNodes = new HashSet<Node>();
            this.RenderedNodes = new Dictionary<Node, SKPath>();
        }

        public Node GetNodeById(Guid id)
        {
            return this.Nodes.FirstOrDefault(x => x.Id == id);
        }

        public Node GetNodeFromPoint(Point pt)
        {
            if (this.RenderedNodes.FirstOrDefault(x => x.Value.Contains(pt.X, pt.Y)) is KeyValuePair<Node, SKPath> item)
            {
                return item.Key;
            }
            return null;
        }

        public void AddNode<T>() where T : Node, new()
        {
            this.Nodes.Add(new T());
        }

        #region Events

        private SKControl control;

        private PropertyGrid propGrid;

        public void AttachEvents(Control parent, PropertyGrid propGrid)
        {
            this.control = new SKControl();
            this.control.Dock = DockStyle.Fill;
            this.propGrid = propGrid;
            this.propGrid.PropertyValueChanged += (s, e) => this.control.Invalidate();

            this.control.PaintSurface += OnPaintSurface;
            this.control.MouseDown += MouseEvents;
   
            parent.Controls.Add(this.control);
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            Console.Clear();
            var total = Stopwatch.StartNew();

            var sw = Stopwatch.StartNew();
            this.RenderBackground(e);
            Console.WriteLine("Render Back\t{0}", sw.ElapsedMilliseconds / 1000.0);

            sw.Restart();
            this.RenderNodes(e);
            Console.WriteLine("Render Nodes\t{0}", sw.ElapsedMilliseconds / 1000.0);

            sw.Restart();
            this.RenderLines(e);
            Console.WriteLine("Render Lines\t{0}", sw.ElapsedMilliseconds / 1000.0);

            sw.Restart();
            this.RenderPorts(e);
            Console.WriteLine("Render Ports\t{0}", sw.ElapsedMilliseconds / 1000.0);

            if (this.SelectNodeRect != Rectangle.Empty)
            {
                using (var p = new SKPaint() { IsAntialias = true })
                {
                    p.Color = new Pen(PageRenderOptions.SelectionBackBrush).Color.ToSKColor();
                    e.Surface.Canvas.DrawRect(this.SelectNodeRect.ToSKRect(), p);
                }
            }

            Console.WriteLine("Render Total\t{0}", total.ElapsedMilliseconds / 1000.0);
            Console.WriteLine("Render Total\t{0:#.00} fps", 1000.0 / total.ElapsedMilliseconds);

            // Update Property Grid
            this.propGrid.SelectedObject = this.SelectedNodes.Count == 1 ? this.SelectedNodes.First() : null;
        }

        private void RenderBackground(SKPaintSurfaceEventArgs e)
        {
            var r = e.Info.Rect;
            var g = e.Surface.Canvas;
            using (var p = new SKPaint())
            {
                g.Clear(PageRenderOptions.BackColor.ToSKColor());
                p.Color = PageRenderOptions.GridPen.Color.ToSKColor();
                p.StrokeWidth = PageRenderOptions.GridPen.Width;
                for (var y = 0; y < r.Height; y += PageRenderOptions.GridSize)
                {
                    g.DrawLine(0, y, r.Width, y, p);
                }
                for (var x = 0; x < r.Width; x += PageRenderOptions.GridSize)
                {
                    g.DrawLine(x, 0, x, r.Height, p);
                }
            }
        }

        private void RenderPorts(SKPaintSurfaceEventArgs e)
        {
            var g = e.Surface.Canvas;
            var o = new NodeStyle();
            foreach (var node in this.Nodes)
            {
                node.RenderedPorts.Clear();
                var nodePath = this.RenderedNodes[node];
                foreach (var port in node.Ports)
                {
                    var portPath = port.Render(g, node.Bounds, o);
                    node.RenderedPorts.Add(port, portPath);
                    nodePath.AddPath(portPath, SKPathAddMode.Append);
                }
            }
        }

        private void RenderLines(SKPaintSurfaceEventArgs e)
        {
            var g = e.Surface.Canvas;
            var o = new NodeStyle();
            var f = new PathFinder(this.Nodes);
            using (var p = new SKPaint() { IsAntialias = true })
            {
                p.IsStroke = true;
                p.StrokeWidth = o.LinePen.Width;

                foreach (var node in this.Nodes)
                {
                    foreach (var port in node.Ports)
                    {
                        p.Color = new Pen(port.GetBackBrush()).Color.ToSKColor().WithAlpha(255);
                        if (this.LinkNodeEndPoint != Point.Empty && this.LinkNodeStartPort == port)
                        {
                            g.DrawLine(port.Bounds.Center().X, port.Bounds.Center().Y, this.LinkNodeEndPoint.X, this.LinkNodeEndPoint.Y, p);
                        }
                        else if (this.GetNodeById(port.NextNodeId) is Node linkedNode)
                        {
                            g.DrawPath(f.GetPath(port.Bounds.Center(), linkedNode.Bounds.CenterTop()), p);
                        }
                    }
                }
            }
        }

        private void RenderNodes(SKPaintSurfaceEventArgs e)
        {
            var g = e.Surface.Canvas;
            this.RenderedNodes.Clear();
            foreach (var node in this.Nodes)
            {
                var r = node.Bounds;
                var o = new NodeStyle();
                if (this.SelectedNodes.Contains(node))
                {
                    o = new SelectedNodeStyle();
                    r.Offset(this.MoveNodeOffsetPoint);
                }
                this.RenderedNodes.Add(node, node.Render(g, r, o));
                node.RenderText(g, r, o);
            }
        }

        #endregion
    }
}