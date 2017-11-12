using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        [DataMember]
        private HashSet<Guid> SelectedNodes { get; set; }

        private Dictionary<Guid, GraphicsPath> RenderedNodes { get; }

        private Dictionary<Guid, GraphicsPath> RenderedPorts { get; }

        public Page()
        {
            this.Id = Guid.NewGuid();
            this.Name = string.Format("My{0}_{1}", this.GetType().Name, DateTime.Now.Ticks);
            this.Nodes = new List<Node>();
            this.AddNode<StartNode>();
            this.SelectedNodes = new HashSet<Guid>();
            this.RenderedNodes = new Dictionary<Guid, GraphicsPath>();
            this.RenderedPorts = new Dictionary<Guid, GraphicsPath>();
        }

        public void AddNode<T>() where T : Node, new()
        {
            this.Nodes.Add(new T());
        }

        private Guid GetNodeIdFromPoint(Point pt)
        {
            if (this.RenderedNodes.FirstOrDefault(x => x.Value.IsVisible(pt)) is KeyValuePair<Guid, GraphicsPath> item &&
                this.GetNodeById(item.Key) is Node node)
            {
                return node.Id;
            }
            return Guid.Empty;
        }

        private Guid GetPortIdFromPoint(Point pt)
        {
            if (this.RenderedPorts.FirstOrDefault(x => x.Value.IsVisible(pt)) is KeyValuePair<Guid, GraphicsPath> item &&
                this.GetNodeById(item.Key) is Node node)
            {
                return node.Id;
            }
            return Guid.Empty;
        }

        public Node GetNodeById(Guid id)
        {
            return this.Nodes.FirstOrDefault(n => n.Id.Equals(id));
        }

        #region Events

        private Control control;

        public void AttachEvents(Control control)
        {
            this.control = control;
            this.control.Paint += OnPaint;
            this.control.MouseDown += MouseEvents;
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Console.Clear();
            var total = Stopwatch.StartNew();

            var sw = Stopwatch.StartNew();
            this.RenderBackground(e);
            Console.WriteLine("Render Back\t{0}", sw.ElapsedMilliseconds / 1000.0);

            sw.Restart();
            this.RenderLines(e);
            Console.WriteLine("Render Lines\t{0}", sw.ElapsedMilliseconds / 1000.0);

            sw.Restart();
            this.RenderNodes(e);
            Console.WriteLine("Render Nodes\t{0}", sw.ElapsedMilliseconds / 1000.0);

            if (this.SelectNodeRect != Rectangle.Empty)
            {
                e.Graphics.FillRectangle(PageRenderOptions.SelectionBackBrush, this.SelectNodeRect);
            }

            Console.WriteLine("Render Total\t{0}", total.ElapsedMilliseconds / 1000.0);
        }

        private void RenderBackground(PaintEventArgs e)
        {
            var g = e.Graphics;
            var r = e.ClipRectangle;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
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

        private void RenderLines(PaintEventArgs e)
        {
            var g = e.Graphics;
            var o = new DefaultLineStyle();
            var x = new NodeStyle();
            var pathFinder = new PathFinder(g, this.Nodes);
            foreach (var node in this.Nodes)
            {
                if (this.LinkNodeEndPoint != Point.Empty && this.LinkNodeStartPort.Id == node.Id)
                {
                    var nodePoint = new Point(node.Bounds.CenterX(), node.Bounds.Bottom);
                    g.DrawLine(o.LinePenWithArrow, nodePoint, this.LinkNodeEndPoint);
                }
                else if (node.Next != Guid.Empty)
                {
                    var nextNode = this.GetNodeById(node.Next);
                    g.DrawPath(o.LinePenWithArrow, pathFinder.GetPath(node.Bounds, nextNode.Bounds));
                }
            }
        }

        private void RenderNodes(PaintEventArgs e)
        {
            this.RenderedNodes.Clear();
            this.RenderedPorts.Clear();
            var g = e.Graphics;
            foreach (var node in this.Nodes)
            {
                var r = node.Bounds;
                var o = new NodeStyle();
                if (this.SelectedNodes.Contains(node.Id))
                {
                    o = new SelectedNodeStyle();
                    r.Offset(this.MoveNodeOffsetPoint);
                }
                this.RenderedNodes.Add(node.Id, node.RenderNode(g, r, o));
                this.RenderedPorts.Add(node.Id, node.RenderPort(g, r, o));
            }
        }

        #endregion

    }
}