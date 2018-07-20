
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Roro.Activities
{
    public sealed class DecisionNode : Node
    {
        //public Guid True { get; set; }

        //public Guid False { get; set; }

        private DecisionNode()
        {
            // required for XmlSerializer.
        }

        internal DecisionNode(Activity activity) : base(activity)
        {
            this.Ports.Add(new TruePort());
            this.Ports.Add(new FalsePort());
        }

        public override Guid Execute(ActivityContext context)
        {
            if ((this.Activity as DecisionNodeActivity).Execute(context))
            {
                return this.Ports.Where(x => x is TruePort).First().NextNodeId;
            }
            else
            {
                return this.Ports.Where(x => x is FalsePort).First().NextNodeId;
            }
        }

        public override GraphicsPath Render(Graphics g, Rect r, NodeStyle o)
        {
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddPolygon(new Point[]
            {
                r.CenterTop,
                r.CenterRight,
                r.CenterBottom,
                r.CenterLeft
            });
            path.CloseFigure();
            //
            g.FillPath(o.BackBrush, path);
            g.DrawPath(o.BorderPen, path);
            return path;
        }
    }
}
