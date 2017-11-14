using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Roro.Workflow
{
    public sealed class DecisionNode : Node
    {
        public DecisionNode()
        {
            this.Ports.Add(new TruePort());
            this.Ports.Add(new FalsePort());
        }

        public override SKPath Render(SKCanvas g, Rectangle r, NodeStyle o)
        {
            var skPath = new SKPath();
            var skPoints = new SKPoint[]
            {
                r.CenterTop().ToSKPoint(),
                r.CenterRight().ToSKPoint(),
                r.CenterBottom().ToSKPoint(),
                r.CenterLeft().ToSKPoint()
            };
            skPath.AddPoly(skPoints);
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
