using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Roro.Workflow
{
    public sealed class PageRenderOptions
    {
        public static int GridSize = 15;

        public static Pen GridPen = new Pen(Color.FromArgb(240, 240, 240), 1);

        public static Color BackColor = Color.FromArgb(250, 250, 250);

        public static Brush SelectionBackBrush = new SolidBrush(Color.FromArgb(50, 150, 150, 150));
    }

    public class DefaultNodeStyle
    {
        public Font Font { get; protected set; }

        public Brush FontBrush { get; protected set; }

        public Pen BorderPen { get; protected set; }

        public Brush BackBrush { get; protected set; }

        public DefaultNodeStyle()
        {
            this.Font = new Font("Verdana", 10);
            this.FontBrush = new SolidBrush(Color.FromArgb(40, 240, 40));
            this.BorderPen = new Pen(Color.FromArgb(100, 100, 100), 1);
            this.BackBrush = new SolidBrush(Color.FromArgb(200, 220, 240));
        }

        public StringFormat StringFormat = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
    }

    public class SelectedNodeStyle : DefaultNodeStyle
    {
        public SelectedNodeStyle()
        {
            this.BorderPen = new Pen(Color.FromArgb(100, 100, 100), 2);
        }
    }

    public class DefaultLineStyle
    {
        public Pen LinePenWithArrow { get; protected set; }

        public DefaultLineStyle()
        {
            var arrowSize = PageRenderOptions.GridSize / 4f;
            this.LinePenWithArrow = new Pen(Color.FromArgb(100, 100, 100), 1);
            this.LinePenWithArrow.CustomEndCap = new AdjustableArrowCap(arrowSize, arrowSize);
        }
    }
}