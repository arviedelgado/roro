using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Roro.Workflow
{
    public sealed class EndNode : Node
    {
        public override void SetNextTo(Guid id)
        {
            var message = string.Format("{0} cannot set next to {1}.", this.GetType().Name, id);
            MessageBox.Show(message, "Action not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public override GraphicsPath RenderNode(Graphics g, Rectangle r, NodeStyle o)
        {
            var midRect = new Rectangle(
                r.X + r.Height / 2,
                r.Y,
                r.Width - r.Height,
                r.Height);
            var leftRect = new Rectangle(
                r.X,
                r.Y,
                r.Height,
                r.Height);
            var rightRect = new Rectangle(
                r.Right - r.Height,
                r.Y,
                r.Height,
                r.Height);
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

        public override Size GetSize() => new Size(4, 2);
    }
}
