using System.Drawing;
using System.Drawing.Drawing2D;

namespace Roro.Workflow
{
    public sealed class DecisionNode : Node
    {
        public DecisionNode()
        {
            this.Ports.Add(new TruePort());
            this.Ports.Add(new FalsePort());
        }

        public override GraphicsPath Render(Graphics g, Rectangle r, NodeStyle o)
        {
            var points = new Point[]
            {
                r.CenterTop(),
                r.CenterRight(),
                r.CenterBottom(),
                r.CenterLeft()
            };
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddPolygon(points);
            path.CloseFigure();
            //
            g.FillPath(o.BackBrush, path);
            g.DrawPath(o.BorderPen, path);
            return path;
        }

        public override Size GetSize() => new Size(4, 2);
    }
}
