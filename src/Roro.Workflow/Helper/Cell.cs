using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Roro.Workflow
{
    public class Cell
    {
        public static readonly CellLocation MoveUp = new CellLocation(-1, 0);

        public static readonly CellLocation MoveDown = new CellLocation(+1, 0);

        public static readonly CellLocation MoveLeft = new CellLocation(0, -1);

        public static readonly CellLocation MoveRight = new CellLocation(0, +1);

        public static readonly CellLocation[] All = new CellLocation[] { MoveUp, MoveDown, MoveLeft, MoveRight };

        public CellLocation Location { get; set; }

        public CellLocation Direction { get; set; }

        public Guid Session { get; set; }

        public Cell Parent { get; set; }

        public int Priority { get; set; }

        public int CurrentCost { get; set; }

        public int RowEffort { get; set; }

        public int ColEffort { get; set; }

        public bool IsWall { get; set; }

        public bool IsOpen { get; set; }

        public Cell(int row, int col)
        {
            this.Location = new CellLocation(row, col);
            this.IsWall = false;
            this.IsOpen = true;
        }

        public override string ToString()
        {
            return string.Format("[Cell Row={0}, Col={1}, IsWall={2}, IsOpen={3}]", this.Location.Row, this.Location.Col, this.IsWall, this.IsOpen);
        }
    }
}
