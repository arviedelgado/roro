using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace Roro.Workflow
{
    public class PathFinder
    {
        private Graphics Graphics;

        private Cell[,] Table;

        private int RowCount;

        private int ColCount;

        public PathFinder(Graphics g, List<Node> nodes)
        {
            this.Graphics = g;
            this.CreateTable(nodes);
        }

        private void CreateTable(IEnumerable<Node> nodes)
        {
            this.RowCount = 0;
            this.ColCount = 0;
            foreach (var node in nodes)
            {
                this.RowCount = Math.Max(this.RowCount, node.Bounds.Bottom);
                this.ColCount = Math.Max(this.ColCount, node.Bounds.Right);
            }
            this.Table = new Cell[this.RowCount, this.ColCount];
            foreach (var node in nodes)
            {
                for (var row = node.Bounds.Y; row <= node.Bounds.Bottom; row += PageRenderOptions.GridSize)
                {
                    for (var col = node.Bounds.X; col <= node.Bounds.Right; col += PageRenderOptions.GridSize)
                    {
                        var r = row / PageRenderOptions.GridSize;
                        var c = col / PageRenderOptions.GridSize;
                        if (r < 0 || c < 0 || r > this.RowCount || c > this.ColCount)
                        {
                            continue;
                        }
                        var cell = this.Table[r, c] = new Cell(r, c);
                        cell.IsWall = true;
                    }
                }
            }
        }

        private void ResetTable()
        {
            for (var row = 0; row < this.RowCount; row++)
            {
                for (var col = 0; col < this.ColCount; col++)
                {
                    if (this.Table[row, col] is Cell cell)
                    {
                        cell.Parent = null;
                        cell.CurrentCost = 0;
                        cell.IsOpen = !cell.IsWall;
                    }
                }
            }
        }

        public GraphicsPath GetPath(Point startPt, Point endPt)
        {
            this.ResetTable();

            var startLoc = new CellLocation(startPt.Y, startPt.X).Scale(1.0 / PageRenderOptions.GridSize).Translate(+2, 0);
            var endLoc = new CellLocation(endPt.Y, endPt.X).Scale(1.0 / PageRenderOptions.GridSize).Translate(-2, 0);

            var startCell = this.Table[startLoc.Row, startLoc.Col];
            var endCell = this.Table[endLoc.Row, endLoc.Col];

            startCell.IsOpen = true;
            endCell.IsOpen = true;

            var path = GetPath(startCell, endCell);

            startLoc.Scale(PageRenderOptions.GridSize);
            endLoc.Scale(PageRenderOptions.GridSize);

            startPt = new Point(startLoc.Col, startLoc.Row);
            endPt = new Point(endLoc.Col, endLoc.Row);

            path.AddLine(startPt, startPt);
            path.Reverse();
            path.AddLine(endPt, endPt);
            return path; 
        }

        private GraphicsPath GetPath(Cell startCell, Cell endCell)
        {
            if (startCell == null || endCell == null)
            {
                return new GraphicsPath();
            }

            var stopWatch = Stopwatch.StartNew();
            var cellQueue = new PriorityQueue<Cell>();
            cellQueue.Enqueue(startCell, startCell.Priority);
            while (stopWatch.ElapsedMilliseconds < 10 && cellQueue.Count > 0)
            {
                var currentCell = cellQueue.Dequeue();
                if (currentCell.Location == endCell.Location)
                {
                    break;
                }
                if (currentCell.IsOpen)
                {
                    currentCell.IsOpen = false;
                    foreach (var nextCell in this.GetNextCells(currentCell, endCell))
                    {
                        cellQueue.Enqueue(nextCell, nextCell.Priority);
                    }
                }
            }

            return TracePath(startCell, endCell);
        }

        private IEnumerable<Cell> GetNextCells(Cell currentCell, Cell endCell)
        {
            var nextCells = new List<Cell>();
            foreach (var nextCellOffset in Cell.All)
            {
                var nextRow = currentCell.Location.Row + nextCellOffset.Row;
                var nextCol = currentCell.Location.Col + nextCellOffset.Col;
                if (nextRow < 0) continue;
                if (nextCol < 0) continue;
                if (nextRow >= this.RowCount) continue;
                if (nextCol >= this.ColCount) continue;
                if (this.Table[nextRow, nextCol] == null)
                {
                    this.Table[nextRow, nextCol] = new Cell(nextRow, nextCol);
                }

                var nextCell = this.Table[nextRow, nextCol];
                if (nextCell.IsOpen)
                {
                    nextCell.RowEffort = nextCell.Location.GetRowEffort(endCell.Location);
                    nextCell.ColEffort = nextCell.Location.GetColEffort(endCell.Location);
                    var nextCellNewCost = currentCell.CurrentCost + 1; // (nextCell.Direction == currentCell.Direction ? 0 : 10);
                    if (nextCell.CurrentCost == 0 || nextCellNewCost < nextCell.CurrentCost)
                    {
                        nextCell.CurrentCost = nextCellNewCost;
                        nextCell.Direction = nextCellOffset;
                        nextCell.Priority = nextCellNewCost + nextCell.RowEffort + nextCell.ColEffort;
                        nextCell.Parent = currentCell;
                        nextCells.Add(nextCell);
                    }
                }
            }
            return nextCells.OrderBy(node => node.Priority).ThenByDescending(node => node.Direction == currentCell.Direction);
        }

        private GraphicsPath TracePath(Cell startCell, Cell endCell)
        {
            var path = new GraphicsPath();
            bool bending = false; // Jon Snow.
            var a = endCell;
            while (a != startCell && a.Parent is Cell b)
            {
                var aDir = a.Direction.Scale(-1);
                var bDir = b.Direction.Scale(-1);
                var aLoc = a.Location;
                var bLoc = b.Location;
                var cLoc = b.Location.Translate(bDir.Row, bDir.Col);
                var rect = new Rectangle(
                    PageRenderOptions.GridSize * Math.Min(aLoc.Col, Math.Min(bLoc.Col, cLoc.Col)),
                    PageRenderOptions.GridSize * Math.Min(aLoc.Row, Math.Min(bLoc.Row, cLoc.Row)),
                    PageRenderOptions.GridSize * 1,
                    PageRenderOptions.GridSize * 1);
                if (aDir == bDir)
                {
                    if (bending)
                    {
                        bending = false;
                    }
                }
                else
                {
                    bending = true;
                    if (aDir == Cell.MoveUp && bDir == Cell.MoveLeft)
                    {
                        // c b
                        //   a
                        path.AddArc(rect, 0, -90);
                    }
                    else if (aDir == Cell.MoveUp && bDir == Cell.MoveRight)
                    {
                        // b c
                        // a
                        path.AddArc(rect, 180, 90);
                    }
                    else if (aDir == Cell.MoveDown && bDir == Cell.MoveLeft)
                    {
                        //   a
                        // c b
                        path.AddArc(rect, 0, 90);
                    }
                    else if (aDir == Cell.MoveDown && bDir == Cell.MoveRight)
                    {
                        // a
                        // b c
                        path.AddArc(rect, 180, -90);
                    }
                    else if (aDir == Cell.MoveLeft && bDir == Cell.MoveUp)
                    {
                        // c
                        // b a
                        path.AddArc(rect, 90, 90);
                    }
                    else if (aDir == Cell.MoveLeft && bDir == Cell.MoveDown)
                    {
                        // b a
                        // c
                        path.AddArc(rect, -90, -90);
                    }
                    else if (aDir == Cell.MoveRight && bDir == Cell.MoveUp)
                    {
                        //   c
                        // a b 
                        path.AddArc(rect, 90, -90);
                    }
                    else if (aDir == Cell.MoveRight && bDir == Cell.MoveDown)
                    {
                        // a b 
                        //   c
                        path.AddArc(rect, -90, 90);
                    }
                }
                a = b;
            }
            return path;
        }
    }
}
