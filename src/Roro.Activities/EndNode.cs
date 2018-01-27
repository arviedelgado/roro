
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public sealed class EndNode : Node
    {
        public EndNode(Activity activity) : base(activity)
        {
            this.Ports.Clear();
        }

        public override Guid Execute(IEnumerable<VariableNode> variableNodes)
        {
            var activity = Activity.CreateInstance(this.ActivityId) as EndNodeActivity;
            activity.Execute(new ActivityContext(variableNodes));
            return Guid.Empty;
        }

        public override bool CanStartLink => false;

        public override GraphicsPath Render(Graphics g, Rectangle r, NodeStyle o)
        {
            var leftRect = new Rectangle(r.X, r.Y, r.Height, r.Height);
            var rightRect = new Rectangle(r.Right - r.Height, r.Y, r.Height, r.Height);
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(leftRect, 90, 180);
            path.AddArc(rightRect, -90, 180);
            path.CloseFigure();
            //
            g.FillPath(o.BackBrush, path);
            g.DrawPath(o.BorderPen, path);
            return path;
        }
    }
}
