using SkiaSharp;
using SkiaSharp.Views.Desktop;
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

        internal void UpdateBounds(Rectangle r)
        {
            var portPoint = this.GetOffset(r);
            var portSize = new Size(PageRenderOptions.GridSize, PageRenderOptions.GridSize);
            portPoint.Offset(-portSize.Width / 2, -portSize.Height / 2);
            var portBounds = new Rectangle(portPoint, portSize);
            this.Bounds = portBounds;
        }

        public SKPath Render(SKCanvas g, Rectangle r, NodeStyle o)
        {
            this.UpdateBounds(r);
            var skPath = new SKPath();
            skPath.AddCircle(this.Bounds.Center().X, this.Bounds.Center().Y, this.Bounds.Center().Y - this.Bounds.Y);
            using (var p = new SKPaint() { IsAntialias = true })
            {
                p.Color = new Pen(this.GetBackBrush()).Color.ToSKColor();
                g.DrawPath(skPath, p);
                return skPath;
            }
        }
    }

    public sealed class NextPort : Port
    {
        public override Point GetOffset(Rectangle r) => r.CenterBottom();

        public override Brush GetBackBrush() => new SolidBrush(Color.FromArgb(100, Color.Blue));
    }

    public sealed class TruePort : Port
    {
        public override Point GetOffset(Rectangle r) => r.CenterBottom();

        public override Brush GetBackBrush() => new SolidBrush(Color.FromArgb(100, Color.Green));
    }

    public sealed class FalsePort : Port
    {
        public override Point GetOffset(Rectangle r) => r.CenterRight();

        public override Brush GetBackBrush() => new SolidBrush(Color.FromArgb(100, Color.Red));
    }
}
