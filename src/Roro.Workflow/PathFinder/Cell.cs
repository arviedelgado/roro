using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Roro.Workflow
{
    public class Cell
    {
        public static readonly CellLocation Up = new CellLocation(-1, 0);

        public static readonly CellLocation Down = new CellLocation(+1, 0);

        public static readonly CellLocation Left = new CellLocation(0, -1);

        public static readonly CellLocation Right = new CellLocation(0, +1);

        public static readonly CellLocation[] All = new CellLocation[] { Up, Down, Left, Right };

        public CellLocation Location { get; set; }

        public CellLocation Direction { get; set; }

        public Cell Parent { get; set; }

        public int Priority { get; set; }

        public int RowEffort { get; set; }

        public int ColEffort { get; set; }

        public bool Walkable { get; set; }

        public Cell(int row, int col)
        {
            this.Location = new CellLocation(row, col);
            this.RowEffort = int.MaxValue;
            this.ColEffort = int.MaxValue;
            this.Walkable = true;
        }

        public override string ToString()
        {
            return string.Format("[Tile Row={0}, Col={1}, Walkable={2}]", this.Location.Row, this.Location.Col, this.Walkable);
        }
    }
}
