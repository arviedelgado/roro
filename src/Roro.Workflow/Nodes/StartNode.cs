using Roro.Activities;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Drawing;

namespace Roro.Workflow
{
    public sealed class StartNode : Node
    {
        public StartNode()
        {
            this.Ports.Add(new NextPort());
            this.Activity = new StartNodeActivity();
        }

        public override SKPath Render(SKCanvas g, Rectangle r, NodeStyle o)
        {
            var skPath = new SKPath();
            skPath.AddRoundedRect(r.ToSKRect(), r.Height / 2, r.Height / 2);
            using (var p = new SKPaint() { IsAntialias = true })
            {
                p.Color = new Pen(o.BackBrush).Color.ToSKColor();
                g.DrawPath(skPath, p);
                p.IsStroke = true;
                p.Color = o.BorderPen.Color.ToSKColor();
                p.StrokeWidth = o.BorderPen.Width;
                g.DrawPath(skPath, p);
            }
            return skPath;
        }

        public override Size GetSize() => new Size(4, 2);
    }
}
