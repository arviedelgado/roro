using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Roro.Workflow
{
    public static class Helper
    {
        // Rectangle

        public static Point Center(this Rectangle r) => new Point(r.X + r.Width / 2, r.Y + r.Height / 2);

        public static Point CenterTop(this Rectangle r) => new Point(r.Center().X, r.Top);

        public static Point CenterLeft(this Rectangle r) => new Point(r.Left, r.Center().Y);

        public static Point CenterRight(this Rectangle r) => new Point(r.Right, r.Center().Y);

        public static Point CenterBottom(this Rectangle r) => new Point(r.Center().X, r.Bottom);
        
        // Random

        private static readonly Random Randomizer = new Random();
        
        public static int Between(int min, int max) => Randomizer.Next(min, max);
    }
}