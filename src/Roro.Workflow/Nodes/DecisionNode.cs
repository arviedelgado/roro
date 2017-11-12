using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Roro.Workflow
{
    public sealed class DecisionNode : Node
    {
        public override GraphicsPath RenderNode(Graphics g, Rectangle r, NodeStyle o)
        {
            var points = new Point[]
            {
                new Point(r.X, r.CenterY()),
                new Point(r.CenterX(), r.Y),
                new Point(r.Right, r.CenterY()),
                new Point(r.CenterX(), r.Bottom)
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
