using System;
using System.Collections.Generic;
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
            this.RenderBackground(e);
            this.RenderNodes(e);
            this.RenderLines(e);
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
            g.DrawLines(o.LinePenWithArrow, new PathFinder(g, this.Nodes).FindPath(g));
        }

        public GraphicsPath FindPath(Point a, Point z)
        {
            var map = new GraphicsPath();
            foreach (var p in this.Paths.Values)
            {
                map.AddPath(p, false);
            }
            var mapRect = map.GetBounds();

            var A = new Point(a.X, a.Y + PageRenderOptions.GridSize * 3);
            var Z = new Point(z.X, z.Y - PageRenderOptions.GridSize * 3);

            var path = new GraphicsPath();
            var points = new List<Point>();

            var P = A;
            points.Add(P);

            Console.Clear();
            Console.WriteLine(A);
            var findPath = true;
            while (findPath)
            {
                findPath = false;
                if (P.Y < Z.Y)
                {
                    var oldY = P.Y;
                    var newY = GoDown(map, P.X, P.Y, Z.Y);
                    if (oldY != newY)
                    {
                        P.Y = newY;
                        findPath = true;
                        Console.WriteLine(P);
                        points.Add(P);
                    }

                }
                if (P.Y != Z.Y || P.X != Z.X)
                {
                    var oldX = P.X;
                    var newX = P.X > Z.X ? GoLeft(map, P.X, P.Y, Z.X) : GoRight(map, P.X, P.Y, Z.X);
                    if (oldX != newX)
                    {
                        P.X = newX;
                        findPath = true;
                        points.Add(P);
                        Console.WriteLine(P);
                    }
                }
            }
            Console.WriteLine(Z);

            path.AddLines(points.ToArray());
            path.AddRectangle(mapRect);
            
            return path;
        }

        public int GoDown(GraphicsPath map, int X, int Y, int targetY)
        {
            Console.WriteLine("GoDown");
            var result = Y;
            while (Y <= targetY)
            {
                if (Y == targetY)
                {
                    return Y;
                }
                else if (map.IsVisible(X, Y)) // cannot go down.
                {
                    break;
                }
                else if (map.IsVisible(X + PageRenderOptions.GridSize, Y)) // can go down, cannot go right.
                {
                    ;
                }
                else // can go down, can go right.
                {
                    result = Y;
                }
                Y = Y + PageRenderOptions.GridSize;
            }
            return result;
        }

        public int GoRight(GraphicsPath map, int X, int Y, int targetX)
        {
            Console.WriteLine("GoRight");
            var result = X;
            var limitX = map.GetBounds().Right;
            while (X <= limitX)
            {
                if (X == limitX)
                {
                    result = X;
                }
                else if (map.IsVisible(X, Y)) // cannot go right.
                {
                    break;
                }
                else if (map.IsVisible(X, Y + PageRenderOptions.GridSize)) // can go right, cannot go down.
                {
                    ;
                }
                else // can go right, can go down.
                {
                    result = X;
                    limitX = targetX;
                }
                X = X + PageRenderOptions.GridSize;
            }
            return result;
        }

        public int GoLeft(GraphicsPath map, int X, int Y, int targetX)
        {
            Console.WriteLine("GoLeft");
            var result = X;
            var limitX = map.GetBounds().Left;
            while (X >= limitX)
            {
                if (X == limitX)
                {
                    result = X;
                }
                else if (map.IsVisible(X, Y)) // cannot go right.
                {
                    break;
                }
                else if (map.IsVisible(X, Y - PageRenderOptions.GridSize)) // can go right, cannot go down.
                {
                    ;
                }
                else // can go right, can go down.
                {
                    result = X;
                    limitX = targetX;
                }
                X = X - PageRenderOptions.GridSize;
            }
            return result;
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