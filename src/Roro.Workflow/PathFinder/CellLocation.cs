using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Roro.Workflow
{
    public struct CellLocation
    {
        public int Row { get; set; }

        public int Col { get; set; }

        public CellLocation(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public CellLocation(Point pt)
        {
            this.Row = pt.Y;
            this.Col = pt.X;
        }

        public CellLocation Offset(int row, int col)
        {
            this.Row += row;
            this.Col += col;
            return this;
        }

        public CellLocation Scale(int scale)
        {
            if (scale > 0)
            {
                this.Row *= scale;
                this.Col *= scale;
            }
            else
            {
                this.Row /= -scale;
                this.Col /= -scale;
            }
            return this;
        }

        public static bool operator ==(CellLocation a, CellLocation z)
        {
            return a.Equals(z);
        }

        public static bool operator !=(CellLocation a, CellLocation z)
        {
            return !a.Equals(z);
        }

        public override bool Equals(object obj)
        {
            return obj is CellLocation other && this.Row == other.Row && this.Col == other.Col;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[CellLocation Row={0}, Col={1}]", this.Row, this.Col);
        }

        public int GetRowEffort(CellLocation other)
        {
            return (int)Math.Sqrt(Math.Pow(this.Row - other.Row, 2));
        }

        public int GetColEffort(CellLocation other)
        {
            return (int)Math.Sqrt(Math.Pow(this.Col - other.Col, 2));
        }
    }
}
