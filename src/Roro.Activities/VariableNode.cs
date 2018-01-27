
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public sealed class VariableNode : Node
    {
        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public object InitialValue { get; set; }

        public object CurrentValue { get; set; }

        public const string StartToken = "[";

        public const string EndToken = "]";

        public override bool CanEndLink => false;

        public override bool CanStartLink => false;

        public VariableNode(Activity activity) : base(activity)
        {
            this.Type = DataType.GetDefault().Id;
        }

        public override Guid Execute(IEnumerable<VariableNode> variables)
        {
            throw new NotImplementedException();
        }

        public override GraphicsPath Render(Graphics g, Rectangle r, NodeStyle o)
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

        public override void RenderText(Graphics g, Rectangle r, NodeStyle o)
        {
            g.DrawString(this.Name + Environment.NewLine + this.CurrentValue, o.Font, o.FontBrush, r, o.StringFormat);
        }
    }
}
