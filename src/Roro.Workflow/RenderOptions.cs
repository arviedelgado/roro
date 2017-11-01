using System.Drawing;
using System.Drawing.Drawing2D;

namespace Roro.Workflow
{
    public sealed class PageRenderOptions
    {
        public static int GridSize = 15;

        public static Pen GridPen = new Pen(Color.FromArgb(220, 220, 220), 1);

        public static Color BackColor = Color.FromArgb(240, 240, 240);
    }

    public class DefaultNodeStyle
    {
        public Font Font { get; protected set; }

        public Brush FontBrush { get; protected set; }

        public Pen BorderPen { get; protected set; }

        public Brush BackgroundBrush { get; protected set; }

        public DefaultNodeStyle()
        {
            this.Font = new Font("Verdana", 10);
            this.FontBrush = new SolidBrush(Color.FromArgb(30, 250, 30));
            this.BorderPen = new Pen(Color.FromArgb(30, 30, 250), 2);
            this.BackgroundBrush = new SolidBrush(Color.FromArgb(100, 150, 150, 250));
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
            this.BorderPen = new Pen(Color.FromArgb(30, 250, 30), 2);
        }
    }

    public class DefaultLineStyle
    {
        public Pen LinePen { get; protected set; }

        public Pen LinePenWithArrow { get; protected set; }

        public DefaultLineStyle()
        {
            this.LinePen = new Pen(Color.FromArgb(250, 30, 30), 2);
            this.LinePenWithArrow = new Pen(Color.FromArgb(250, 30, 30), 2);
            this.LinePenWithArrow.CustomEndCap = new AdjustableArrowCap(5, 5);
        }
    }
}