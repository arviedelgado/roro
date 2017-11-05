using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Roro.Workflow
{
    public sealed class DecisionNode : Node
    {
        public Guid True { get; set; }

        public Guid False { get; set; }

        public override Guid Execute()
        {
            Console.WriteLine("Execute: {0} {1}", this.Id, this.GetType().Name);
            return this.True;
        }

        public override void SetNextTo(Guid id)
        {
            this.True = id;
        }

        public override GraphicsPath Render(Graphics g, Rectangle r, DefaultNodeStyle o)
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
    }
}
