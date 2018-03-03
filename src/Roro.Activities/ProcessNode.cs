
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Roro.Activities
{
    public sealed class ProcessNode : Node
    {
        //public Guid Next { get; set; }

        private ProcessNode()
        {
            // required for XmlSerializer.
        }

        internal ProcessNode(Activity activity) : base(activity)
        {
            this.Ports.Add(new NextPort());
        }

        public override Guid Execute(ActivityContext context)
        {
            (this.Activity as ProcessNodeActivity).Execute(context);
            return this.Ports.First().NextNodeId;
        }

        public override GraphicsPath Render(Graphics g, Rect r, NodeStyle o)
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
