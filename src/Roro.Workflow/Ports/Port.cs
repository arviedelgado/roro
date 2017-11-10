using Roro.Activities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;

namespace Roro.Workflow
{
    public abstract class Port
    {
        public abstract Point GetOffset(Rectangle r);

        public void Render(Graphics g, Rectangle r, NodeStyle o)
        {
            var pt = this.GetOffset(r);
            var width = PageRenderOptions.GridSize * 3 / 4;
            pt.Offset(-width / 2, -width / 2);
            var sz = new Size(width, width);
            var portRect = new Rectangle(pt, sz);

            g.FillEllipse(o.PortBackBrush, portRect);
            g.DrawEllipse(o.PortBorderPen, portRect);
        }
    }

    public sealed class OutPort : Port
    {
        public override Point GetOffset(Rectangle r) => new Point(r.CenterX(), r.Bottom);
    }

    public sealed class TruePort : Port
    {
        public override Point GetOffset(Rectangle r) => new Point(r.CenterX(), r.Bottom);
    }

    public sealed class FalsePort : Port
    {
        public override Point GetOffset(Rectangle r) => new Point(r.Right, r.CenterY());
    }

}
