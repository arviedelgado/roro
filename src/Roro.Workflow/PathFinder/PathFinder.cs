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

        private int NotWalkable = 0;

        public PathFinder(Graphics g, List<Node> nodes)
        {
            this.Graphics = g;
            this.CreateTable(g, nodes);
        }

        public GraphicsPath FindPath(Point start, Point end)
        {
            this.NotWalkable++; // increment every FindPath session.

            var cellQueue = new PriorityQueue<Cell>();

            var startLoc = new CellLocation(start).Multiply(1.0 / PageRenderOptions.GridSize).Offset(+2, 0);
            var endLoc = new CellLocation(end).Multiply(1.0 / PageRenderOptions.GridSize).Offset(-2, 0);

            var startCell = this.Table[startLoc.Row, startLoc.Col];
            var endCell = this.Table[endLoc.Row, endLoc.Col];

            startCell.Walkable = 0;
            endCell.Walkable = 0;

            var offsetStartCell = this.GetNextCells(startCell, endCell).FirstOrDefault();
            var offsetEndCell = this.GetNextCells(endCell, endCell).FirstOrDefault();
            if (offsetStartCell == null || offsetEndCell == null)
            {
                endCell.Parent = startCell;
            }
            else
            {
                startCell.Walkable = this.NotWalkable;
                endCell.Walkable = this.NotWalkable;
                endCell.Direction = offsetEndCell.Direction.Multiply(-1);
                endCell.Parent = offsetEndCell;
                offsetStartCell.Parent = startCell;
                cellQueue.Enqueue(offsetStartCell, offsetStartCell.Priority);
            }

            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < 10 && cellQueue.Count > 0)
            {
                var currentCell = cellQueue.Dequeue();
                if (currentCell.Location == offsetEndCell.Location)
                {
                    break;
                }
                if (currentCell.Walkable < this.NotWalkable)
                {
                    currentCell.Walkable = this.NotWalkable;
                    foreach (var nextCell in this.GetNextCells(currentCell, offsetEndCell))
                    {
                        cellQueue.Enqueue(nextCell, nextCell.Priority);
                    }
                }
            }

            var path = new GraphicsPath();
            bool bending = false; // bend the knee, Jon Snow.
            var a = endCell;
            while (a != null && a.Parent is Cell b)
            {
                var aLoc = a.Location.Multiply(PageRenderOptions.GridSize);
                var bLoc = b.Location.Multiply(PageRenderOptions.GridSize);
                if (a.Direction == b.Direction)
                {
                    if (bending)
                    {
                        bending = false;
                    }
                    else
                    {
                        path.AddLine(new Point(aLoc.Col, aLoc.Row), new Point(bLoc.Col, bLoc.Row));
                    }
                }
                else
                {
                    if (a.Direction == Cell.MoveUp && b.Direction == Cell.MoveLeft)
                    {
                        // a
                        // b z
                        path.AddArc(
                            bLoc.Col,
                            bLoc.Row - PageRenderOptions.GridSize,
                            PageRenderOptions.GridSize, PageRenderOptions.GridSize, 180, -90);
                    }
                    if (a.Direction == Cell.MoveUp && b.Direction == Cell.MoveRight)
                    {
                        //   a
                        // z b
                        path.AddArc(
                            bLoc.Col - PageRenderOptions.GridSize,
                            bLoc.Row - PageRenderOptions.GridSize,
                            PageRenderOptions.GridSize, PageRenderOptions.GridSize, 0, 90);
                    }
                    if (a.Direction == Cell.MoveDown && b.Direction == Cell.MoveLeft)
                    {
                        // b z
                        // a
                        path.AddArc(
                            bLoc.Col,
                            bLoc.Row,
                            PageRenderOptions.GridSize, PageRenderOptions.GridSize, 180, 90);
                    }
                    if (a.Direction == Cell.MoveDown && b.Direction == Cell.MoveRight)
                    {
                        // z b
                        //   a
                        path.AddArc(
                            bLoc.Col - PageRenderOptions.GridSize,
                            bLoc.Row,
                            PageRenderOptions.GridSize, PageRenderOptions.GridSize, 0, -90);
                    }
                    if (a.Direction == Cell.MoveLeft && b.Direction == Cell.MoveUp)
                    {
                        // a b 
                        //   z
                        path.AddArc(
                            bLoc.Col - PageRenderOptions.GridSize,
                            bLoc.Row,
                            PageRenderOptions.GridSize, PageRenderOptions.GridSize, -90, 90);
                    }
                    if (a.Direction == Cell.MoveLeft && b.Direction == Cell.MoveDown)
                    {
                        //   z
                        // a b 
                        path.AddArc(
                            bLoc.Col - PageRenderOptions.GridSize,
                            bLoc.Row - PageRenderOptions.GridSize,
                            PageRenderOptions.GridSize, PageRenderOptions.GridSize, 90, -90);
                    }
                    if (a.Direction == Cell.MoveRight && b.Direction == Cell.MoveUp)
                    {
                        // b a
                        // z
                        path.AddArc(
                            bLoc.Col,
                            bLoc.Row,
                            PageRenderOptions.GridSize, PageRenderOptions.GridSize, -90, -90);
                    }
                    if (a.Direction == Cell.MoveRight && b.Direction == Cell.MoveDown)
                    {
                        // z
                        // b a
                        path.AddArc(
                            bLoc.Col,
                            bLoc.Row - PageRenderOptions.GridSize,
                            PageRenderOptions.GridSize, PageRenderOptions.GridSize, 90, 90);
                    }
                    bending = true;
                }
                a = a.Parent;
            }
            path.AddLine(path.GetLastPoint(), new Point(
                startLoc.Col * PageRenderOptions.GridSize,
                startLoc.Row * PageRenderOptions.GridSize));
            path.Reverse();
            path.AddLine(path.GetLastPoint(), new Point(
                endLoc.Col * PageRenderOptions.GridSize,
                endLoc.Row * PageRenderOptions.GridSize));

            Console.WriteLine("FindPath\t{0}", sw.ElapsedMilliseconds / 1000.0);

            return path;
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
                if (nextCell.Walkable < this.NotWalkable)
                {
                    nextCell.Parent = currentCell;
                    nextCell.Priority = 0;
                    nextCell.Direction = nextCellOffset;
                    nextCell.RowEffort = nextCell.Location.GetRowEffort(endCell.Location);
                    nextCell.ColEffort = nextCell.Location.GetColEffort(endCell.Location);
                    if (nextCell.Direction == currentCell.Direction)
                    {
                        if (nextCell.Direction == Cell.MoveUp || nextCell.Direction == Cell.MoveDown)
                        {
                            if (nextCell.RowEffort > currentCell.RowEffort) // we walked too much
                            {
                                nextCell.Priority = nextCell.RowEffort + nextCell.ColEffort;
                            }
                            else
                            {
                                nextCell.Priority = nextCell.RowEffort;
                            }
                        }
                        if (nextCell.Direction == Cell.MoveLeft || nextCell.Direction == Cell.MoveRight)
                        {
                            if (nextCell.ColEffort > currentCell.ColEffort) // we walked too much
                            {
                                nextCell.Priority = nextCell.ColEffort + nextCell.RowEffort;
                            }
                            else if (nextCell.ColEffort == 3)
                            {
                                nextCell.Priority = nextCell.ColEffort + nextCell.RowEffort;
                            }
                            else
                            {
                                nextCell.Priority = nextCell.ColEffort;
                            }
                        }
                    }
                    else
                    {
                        nextCell.Priority = nextCell.RowEffort + nextCell.ColEffort;
                    }
                    nextCells.Add(nextCell);
                }
            }
            return nextCells.OrderBy(node => node.Priority);
        }

        private void CreateTable(Graphics g, IEnumerable<Node> nodes)
        {
            var sw = Stopwatch.StartNew();
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
                        cell.Walkable = int.MaxValue; // Never.

                        //if (g != null) g.DrawRectangle(Pens.Purple, col, row, 4, 4);
                    }
                }
            }
            Console.WriteLine("CreateTable\t{0}", sw.ElapsedMilliseconds / 1000.0);
        }
    }
}
