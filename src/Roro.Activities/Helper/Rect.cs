using System.Drawing;
using System.Xml.Serialization;

namespace Roro.Activities
{
    public struct Rect
    {
        [XmlAttribute]
        public int X { get; set; }

        [XmlAttribute]
        public int Y { get; set; }

        [XmlAttribute]
        public int Width { get; set; }

        [XmlAttribute]
        public int Height { get; set; }

        public Rect(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public static implicit operator Rectangle(Rect r) => new Rectangle(r.X, r.Y, r.Width, r.Height);

        public static implicit operator Rect(Rectangle r) => new Rect(r.X, r.Y, r.Width, r.Height);

        public static implicit operator RectangleF(Rect r) => new RectangleF(r.X, r.Y, r.Width, r.Height);

        public static implicit operator Rect(RectangleF r) => new Rect((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height);

        public int Top => this.Y;

        public int Left => this.X;

        public int Right => this.X + this.Width;

        public int Bottom => this.Y + this.Height;

        public Point Center => new Point(this.X + this.Width / 2, this.Y + this.Height / 2);

        public Point CenterTop => new Point(this.Center.X, this.Top);

        public Point CenterLeft => new Point(this.Left, this.Center.Y);

        public Point CenterRight => new Point(this.Right, this.Center.Y);

        public Point CenterBottom => new Point(this.Center.X, this.Bottom);
    }
}