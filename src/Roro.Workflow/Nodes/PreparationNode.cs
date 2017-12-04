using Roro.Activities;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Roro.Workflow
{
    public sealed class PreparationNode : Node
    {
        public PreparationNode()
        {
            this.Ports.Add(new NextPort());
        }

        public override GraphicsPath Render(Graphics g, Rectangle r, NodeStyle o)
        {
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddPolygon(new Point[]
            {
                new Point(r.X + PageRenderOptions.GridSize, r.Y),
                new Point(r.Right - PageRenderOptions.GridSize, r.Y),
                r.CenterRight(),
                new Point(r.Right - PageRenderOptions.GridSize, r.Bottom),
                new Point(r.X + PageRenderOptions.GridSize, r.Bottom),
                r.CenterLeft(),
            });
            path.CloseFigure();
            //
            g.FillPath(o.BackBrush, path);
            g.DrawPath(o.BorderPen, path);
            return path;
        }

        public override Size GetSize() => new Size(4, 2);
    }
}
