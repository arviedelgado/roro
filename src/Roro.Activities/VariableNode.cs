
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Roro.Activities
{
    public sealed class VariableNode : Node
    {
        public string Type { get; set; }

        public object InitialValue { get; set; }

        internal object CurrentValue { get; set; }

        public const string StartToken = "[";

        public const string EndToken = "]";

        public override bool CanEndLink => false;

        public override bool CanStartLink => false;

        private VariableNode()
        {
            // required for XmlSerializer.
        }

        internal VariableNode(Activity activity) : base(activity)
        {
            this.Type = DataType.GetDefault().GetType().FullName;
        }

        public override Guid Execute(ActivityContext context)
        {
            throw new NotImplementedException();
        }

        public override GraphicsPath Render(Graphics g, Rect r, NodeStyle o)
        {
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddPolygon(new Point[]
            {
                new Point(r.X + PageRenderOptions.GridSize, r.Y),
                new Point(r.Right, r.Y),
                new Point(r.Right - PageRenderOptions.GridSize, r.Bottom),
                new Point(r.X, r.Bottom),
            });
            path.CloseFigure();
            //
            g.FillPath(o.BackBrush, path);
            g.DrawPath(o.BorderPen, path);
            return path;
        }

        public override void RenderText(Graphics g, Rect r, NodeStyle o)
        {
            g.DrawString(this.Name + Environment.NewLine + this.CurrentValue, o.Font, o.FontBrush, r, o.StringFormat);
        }
    }
}
