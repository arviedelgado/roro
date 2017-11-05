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
    public class Page
    {
        [DataMember]
        public Guid Id { get; private set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<Node> Nodes { get; private set;  }

        internal Dictionary<Guid, GraphicsPath> Paths { get; }

        internal Guid activeNode { get; private set; }

        public Page()
        {
            this.Id = Guid.NewGuid();
            this.Name = string.Format("My{0}_{1}", this.GetType().Name, DateTime.Now.Ticks);
            this.Nodes = new List<Node>();
            this.Nodes.Add(new StartNode());
            this.Paths = new Dictionary<Guid, GraphicsPath>();
        }

        private Guid GetNodeFromPoint(int pX, int pY)
        {
            if (this.Paths.FirstOrDefault(x => x.Value.IsVisible(pX, pY)) is KeyValuePair<Guid, GraphicsPath> item &&
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

        public void AttachEvents(Control control)
        {
            this.control = control;
            this.control.Paint += OnPaint;
            this.control.MouseDown += OnDragNodeStart;
            //this.control.MouseMove += OnDraggingNode;
            this.control.MouseUp += OnDragNodeEnd;
        }

        private Control control;

        private bool isDraggingNode;

        private Point dragStartPoint;

        internal Point dragOffsetPoint;

        private void OnDragNodeStart(object sender, MouseEventArgs e)
        {
            var node = this.GetNodeFromPoint(e.X, e.Y);
            this.activeNode = node;
            if (node == Guid.Empty)
            {
                this.isDraggingNode = false;
            }
            else
            {
                this.isDraggingNode = true;
                this.dragStartPoint = e.Location;
                this.dragOffsetPoint = default(Point);
            }
            this.control.Invalidate();
        }

        private void OnDraggingNode(object sender, MouseEventArgs e)
        {
            if (this.isDraggingNode)
            {
                var dX = (int)Math.Round((double)(e.X - this.dragStartPoint.X) / PageRenderOptions.GridSize) * PageRenderOptions.GridSize;
                var dY = (int)Math.Round((double)(e.Y - this.dragStartPoint.Y) / PageRenderOptions.GridSize) * PageRenderOptions.GridSize;
                this.dragOffsetPoint = new Point(dX, dY);
                this.control.Invalidate();
            }
        }

        private void OnDragNodeEnd(object sender, MouseEventArgs e)
        {
            if (this.isDraggingNode)
            {
                var dX = (int)Math.Round((double)(e.X - this.dragStartPoint.X) / PageRenderOptions.GridSize) * PageRenderOptions.GridSize;
                var dY = (int)Math.Round((double)(e.Y - this.dragStartPoint.Y) / PageRenderOptions.GridSize) * PageRenderOptions.GridSize;
                var node = this.GetNodeById(this.activeNode);
                var rect = node.Bounds;
                rect.Offset(dX, dY);
                node.Bounds = rect;
                this.dragOffsetPoint = default(Point);
                this.isDraggingNode = false;
            }
            this.control.Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
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
            var x = new DefaultNodeStyle();
            var pathFinder = new PathFinder(g, this.Nodes);
            Node prev = null;
            foreach (var node in this.Nodes)
            {
                if (prev != null)
                {
                    g.DrawPath(o.LinePenWithArrow, pathFinder.GetPath(prev.Bounds.Center(), node.Bounds.Center()));
                }
                prev = node;
            }
        }

        private void RenderNodes(PaintEventArgs e)
        {
            this.Paths.Clear();
            var g = e.Graphics;
            foreach (var node in this.Nodes)
            {
                var r = node.Bounds;
                var o = new DefaultNodeStyle();
                if (node.Id.Equals(this.activeNode))
                {
                    if (this.isDraggingNode)
                    {
                        r.Inflate(2, 2);
                        r.Offset(this.dragOffsetPoint);
                        o = new SelectedNodeStyle();
                    }
                }
                this.Paths.Add(node.Id, node.Render(g, r, o));
            }
        }

        #endregion

    }
}