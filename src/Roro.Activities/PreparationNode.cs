
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public sealed class PreparationNode : Node
    {
        public PreparationNode(Activity activity) : base(activity)
        {
            this.Ports.Add(new NextPort());
        }

        public override Guid Execute(IEnumerable<Variable> variables)
        {
            (this.Activity as PreparationNodeActivity).Execute(new ActivityContext(variables));
            return this.Ports.First().NextNodeId;
        }

        public override GraphicsPath Render(Graphics g, Rectangle r, NodeStyle o)
        {
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddPolygon(new Point[]
            {
                new Point(r.X + PageRenderOptions.GridSize, r.Y),
                new Point(r.Right - PageRenderOptions.GridSize, r.Y),
                r.CenterRight(),
                new Point(r.Right - PageRenderOptions.GridSize, r.Bottom),
                new Point(r.X + PageRenderOptions.GridSize, r.Bottom),
                r.CenterLeft(),
            });
            path.CloseFigure();
            //
            g.FillPath(o.BackBrush, path);
            g.DrawPath(o.BorderPen, path);
            return path;
        }

        public override Size GetSize() => new Size(4, 2);
    }
}
