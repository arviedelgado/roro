using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Diagnostics;

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
            var sw = Stopwatch.StartNew();
            this.Graphics = g;
            this.CreateTable(g, nodes);
            Console.WriteLine("CreateTable\t{0}", sw.ElapsedMilliseconds / 1000.0);
        }

        public Point[] FindPath(Point start, Point end)
        {
            this.NotWalkable++; // increment every FindPath session.

            var startLoc = new CellLocation(start).Scale(-PageRenderOptions.GridSize).Offset(+2, 0);
            var endLoc = new CellLocation(end).Scale(-PageRenderOptions.GridSize).Offset(-2, 0);

            var startCell = this.Table[startLoc.Row, startLoc.Col];
            var endCell = this.Table[endLoc.Row, endLoc.Col];

            startCell.Walkable = 0;
            endCell.Walkable = 0;

            endCell.Parent = startCell; // if no path is found, just connect the start and end points.

            var cellQueue = new PriorityQueue<Cell>();

            cellQueue.Enqueue(startCell, startCell.Priority);

            int step = 0; // to remove

            var sw = Stopwatch.StartNew();

            while (sw.ElapsedMilliseconds < 10 && cellQueue.Count > 0)
            {
                var currentCell = cellQueue.Dequeue();
                if (currentCell.Location == endCell.Location)
                {
                    break;
                }
                if (currentCell.Walkable < this.NotWalkable)
                {
                    // to remove
                    //this.Graphics.DrawString(step.ToString(), new Font("Consolas", 8), Brushes.Blue, currentCell.Location.Col * PageRenderOptions.GridSize, currentCell.Location.Row * PageRenderOptions.GridSize);
                    step++;
                    // --
                    currentCell.Walkable = this.NotWalkable;
                    foreach (var nextCell in this.GetNextCells(currentCell, endCell))
                    {
                        cellQueue.Enqueue(nextCell, nextCell.Priority);
                    }
                }
            }

            var cellPoints = new List<Point>();
            var traceCell = endCell;
            while (traceCell != null)
            {
                var traceCellLoc = traceCell.Location;
                traceCellLoc.Scale(PageRenderOptions.GridSize);
                cellPoints.Add(new Point(traceCellLoc.Col, traceCellLoc.Row));
                traceCell = traceCell.Parent;
            }
            cellPoints.Reverse();

            Console.WriteLine("FindPath\t{1}\t{0} steps", cellPoints.Count, sw.ElapsedMilliseconds / 1000.0);

            //DrawTable();

            return cellPoints.ToArray();
        }

        private void DrawTable()
        {
            foreach (var cell in this.Table)
            {
                if (cell != null)
                {
                    this.Graphics.DrawRectangle(
                        cell.Walkable == 0 ? Pens.Green : Pens.Red,
                        cell.Location.Col * PageRenderOptions.GridSize,
                        cell.Location.Row * PageRenderOptions.GridSize, 4, 4);
                }
            }
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
            Console.WriteLine("CreateTable\t{0}\tRowCount, ColCount", sw.ElapsedMilliseconds / 1000.0);
            sw.Restart();
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
            Console.WriteLine("CreateTable\t{0}\tTable[row, col].Walkable = Never", sw.ElapsedMilliseconds / 1000.0);
        }
    }
}
