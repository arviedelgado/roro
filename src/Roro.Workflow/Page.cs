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
        public List<Node> Nodes { get; private set; }

        internal Dictionary<Guid, GraphicsPath> Paths { get; }

        internal List<Guid> SelectedNodes { get; private set; }

        public Page()
        {
            this.Id = Guid.NewGuid();
            this.Name = string.Format("My{0}_{1}", this.GetType().Name, DateTime.Now.Ticks);
            this.Nodes = new List<Node>();
            this.Nodes.Add(new StartNode());
            this.Paths = new Dictionary<Guid, GraphicsPath>();
            this.SelectedNodes = new List<Guid>();
        }

        private Guid GetNodeIdFromPoint(int pX, int pY)
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
            this.control.MouseDown += NodeDragRequest;
            this.control.MouseDown += NodeSelectRequest;
            this.control.MouseDown += NodeDragSelectRequest;
        }

        private Control control;

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
                if (this.SelectedNodes.Contains(node.Id))
                {
                    o = new SelectedNodeStyle();
                    r.Offset(this.NodeDragOffsetPoint);
                }
                this.Paths.Add(node.Id, node.Render(g, r, o));
            }
            if (this.NodeDragSelectRectangle != Rectangle.Empty)
            {
                g.DrawRectangle(Pens.Purple, this.NodeDragSelectRectangle);
            }
        }

        #endregion

    }
}