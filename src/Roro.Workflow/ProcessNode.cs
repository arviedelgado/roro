using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Roro.Workflow
{
    public sealed class ProcessNode : Node
    {
        public Guid Next { get; set; }

        public override Guid Execute()
        {
            Console.WriteLine("Execute: {0} {1}", this.Id, this.GetType().Name);
            return this.Next;
        }

        public override void SetNextTo(Guid id)
        {
            this.Next = id;
        }

        public override GraphicsPath Render(Graphics g, Rectangle r, DefaultNodeStyle o)
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
    }
}
