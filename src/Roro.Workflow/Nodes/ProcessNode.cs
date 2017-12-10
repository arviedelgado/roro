using Roro.Activities;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;

namespace Roro.Workflow
{
    [DataContract]
    public sealed class ProcessNode : Node
    {
        public ProcessNode(Activity activity) : base(activity)
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
