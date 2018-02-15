
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public sealed class DecisionNode : Node
    {
        public DecisionNode(Activity activity) : base(activity)
        {
            this.Ports.Add(new TruePort());
            this.Ports.Add(new FalsePort());
        }

        public override Guid Execute(ActivityContext context)
        {
            var activity = Activity.CreateInstance(this.ActivityId) as DecisionNodeActivity;
            activity.Inputs = this.ActivityInputs;
            activity.Outputs = this.ActivityOutputs;
            if (activity.Execute(context))
            {
                return this.Ports.Where(x => x is TruePort).First().NextNodeId;
            }
            else
            {
                return this.Ports.Where(x => x is FalsePort).First().NextNodeId;
            }
        }

        public override GraphicsPath Render(Graphics g, Rectangle r, NodeStyle o)
        {
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddPolygon(new Point[]
            {
                r.CenterTop(),
                r.CenterRight(),
                r.CenterBottom(),
                r.CenterLeft()
            });
            path.CloseFigure();
            //
            g.FillPath(o.BackBrush, path);
            g.DrawPath(o.BorderPen, path);
            return path;
        }
    }
}
