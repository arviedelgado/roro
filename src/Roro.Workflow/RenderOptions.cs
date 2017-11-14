using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Roro.Workflow
{
    public sealed class PageRenderOptions
    {
        public static int GridSize = 20;

        public static Pen GridPen = new Pen(Color.FromArgb(240, 240, 240), 1);

        public static Color BackColor = Color.FromArgb(250, 250, 250);

        public static Brush SelectionBackBrush = new SolidBrush(Color.FromArgb(50, 150, 150, 150));
    }

    public class NodeStyle
    {
        public Font Font { get; protected set; }

        public Brush FontBrush { get; protected set; }

        public Pen BorderPen { get; protected set; }

        public Brush PortBackBrush { get; protected set; }

        public Brush BackBrush { get; protected set; }

        public Pen LinePen { get; protected set; }


        public NodeStyle()
        {
            this.BorderPen = new Pen(Color.FromArgb(100, 100, 100), 1);
            this.BackBrush = new SolidBrush(PageRenderOptions.BackColor);
            this.PortBackBrush = new SolidBrush(Color.FromArgb(150, 50, 150, 250));
            this.LinePen = new Pen(Color.FromArgb(100, 100, 100), 1);
        }

        public StringFormat StringFormat = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
    }

    public class SelectedNodeStyle : NodeStyle
    {
        public SelectedNodeStyle()
        {
            this.BackBrush = new SolidBrush(Color.FromArgb(250, 250, 150));
        }
    }
}