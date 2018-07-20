using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Roro.Activities
{
    public sealed class LoopEndNode : Node
    {
        //public Guid Next { get; set; }

        public Guid LoopStartNodeId { get; set; }

        private LoopEndNode()
        {
            // required for XmlSerializer.
        }

        internal LoopEndNode(Activity activity) : base(activity)
        {
            this.Ports.Add(new NextPort());
        }

        public override Guid Execute(ActivityContext context)
        {
            return this.Ports.First().NextNodeId;
        }

        public override GraphicsPath Render(Graphics g, Rect r, NodeStyle o)
        {
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddPolygon(new Point[]
            {
                new Point(r.X, r.Y),
                new Point(r.Right, r.Y),
                new Point(r.Right, r.Bottom - PageRenderOptions.GridSize),
                new Point(r.Right - PageRenderOptions.GridSize, r.Bottom),
                new Point(r.X + PageRenderOptions.GridSize, r.Bottom),
                new Point(r.X, r.Bottom - PageRenderOptions.GridSize)
            });
            path.CloseFigure();
            //
            g.FillPath(o.BackBrush, path);
            g.DrawPath(o.BorderPen, path);
            return path;
        }
    }
}
