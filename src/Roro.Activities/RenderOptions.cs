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

        public Pen LinePenWithArrow { get; protected set; }


        public NodeStyle()
        {
            this.Font = new Font("Segoe UI", 10, GraphicsUnit.Pixel);
            this.FontBrush = new SolidBrush(Color.FromArgb(50, 50, 50));
            this.BorderPen = new Pen(Color.FromArgb(100, 100, 100), 1.5f);
            this.BackBrush = new SolidBrush(PageRenderOptions.BackColor);
            this.PortBackBrush = new SolidBrush(Color.FromArgb(150, 50, 150, 250));
            this.LinePenWithArrow = new Pen(Color.FromArgb(100, 100, 100), 1.5f);
            using (GraphicsPath endCap = new GraphicsPath())
            {
                var width = PageRenderOptions.GridSize * 3 / 4 / 2 / this.LinePenWithArrow.Width;

                endCap.AddLine(-width, -width - this.LinePenWithArrow.Width / 2, 0, -this.LinePenWithArrow.Width / 2);
                endCap.AddLine(+width, -width - this.LinePenWithArrow.Width / 2, 0, -this.LinePenWithArrow.Width / 2);
                this.LinePenWithArrow.CustomEndCap = new CustomLineCap(null, endCap);
            }
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