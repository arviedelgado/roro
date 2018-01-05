using System;

namespace Roro.Activities
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

        public CellLocation Translate(int row, int col)
        {
            this.Row += row;
            this.Col += col;
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

        public CellLocation Scale(double scale)
        {
            this.Row = (int)(this.Row * scale);
            this.Col = (int)(this.Col * scale);
            return this;
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
            return Math.Abs(this.Row - other.Row);
        }

        public int GetColEffort(CellLocation other)
        {
            return Math.Abs(this.Col - other.Col);
        }
    }
}
