using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Roro.Workflow
{
    public abstract class Port
    {
        public Guid Id { get; }

        public Guid NextNodeId { get; set; }

        public Rectangle Bounds { get; set; }

        public abstract Point GetOffset(Rectangle r);

        public abstract Brush GetBackBrush();

        public Port()
        {
            this.Id = Guid.NewGuid();
        }

        public GraphicsPath Render(Graphics g, Rectangle r, NodeStyle o)
        {
            var portPoint = this.GetOffset(r);
            portPoint.Offset(-PageRenderOptions.GridSize / 2, -PageRenderOptions.GridSize / 2);
            var portSize = new Size(PageRenderOptions.GridSize, PageRenderOptions.GridSize);
            var portRect = new Rectangle(portPoint, portSize);
            this.Bounds = portRect;
            g.FillEllipse(this.GetBackBrush(), portRect);
            var portPath = new GraphicsPath();
            portPath.StartFigure();
            portPath.AddEllipse(portRect);
            portPath.CloseFigure();
            return portPath;
        }
    }

    public sealed class NextPort : Port
    {
        public override Point GetOffset(Rectangle r) => r.CenterBottom();

        public override Brush GetBackBrush() => new SolidBrush(Color.FromArgb(50, Color.Blue));
    }

    public sealed class TruePort : Port
    {
        public override Point GetOffset(Rectangle r) => r.CenterBottom();

        public override Brush GetBackBrush() => new SolidBrush(Color.FromArgb(50, Color.Green));
    }

    public sealed class FalsePort : Port
    {
        public override Point GetOffset(Rectangle r) => r.CenterRight();

        public override Brush GetBackBrush() => new SolidBrush(Color.FromArgb(50, Color.Red));
    }
}
