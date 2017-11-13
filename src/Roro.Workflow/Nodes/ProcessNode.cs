using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Roro.Workflow
{
    public sealed class ProcessNode : Node
    {
        public ProcessNode()
        {
            this.Ports.Add(new NextPort());
        }

        public override GraphicsPath Render(Graphics g, Rectangle r, NodeStyle o)
        {
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddRectangle(r);
            path.CloseFigure();
            //
            g.FillPath(o.BackBrush, path);
            g.DrawPath(o.BorderPen, path);
            return path;
        }

        public override Size GetSize() => new Size(4, 2);
    }
}
