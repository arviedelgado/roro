using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Roro.Workflow
{
    public class PathFinder
    {
        private Cell[,] Table;

        private Graphics Graphics;

        private int Session = 0;

        public PathFinder(Graphics g, List<Node> nodes)
        {
            this.Graphics = g;
            this.Table = PathFinder.CreateTable(g, nodes);
        }

        public Point[] FindPath(Point start, Point end)
        {
            this.Session++;

            var startLoc = new CellLocation(start).Scale(-PageRenderOptions.GridSize).Offset(+2, 0);
            var endLoc = new CellLocation(end).Scale(-PageRenderOptions.GridSize).Offset(-2, 0);

            var startCell = this.Table[startLoc.Row, startLoc.Col];
            var endCell = this.Table[endLoc.Row, endLoc.Col];

            startCell.Walkable = true;
            endCell.Walkable = true;

            endCell.Parent = startCell; // if no path is found, just connect the start and end points.

            var cellQueue = new PriorityQueue<Cell>();

            cellQueue.Enqueue(startCell, 0);

            int step = 0; // to remove

            while (cellQueue.Count > 0)
            {
                var currentCell = cellQueue.Dequeue();
                if (currentCell.Location == endCell.Location)
                {
                    break;
                }
                if (currentCell.Walkable)
                {
                    // to remove
                    if (step < 100)
                    {
                        this.Graphics.DrawString(step.ToString(), new Font("Consolas", 8), Brushes.Blue, currentCell.Location.Col * PageRenderOptions.GridSize, currentCell.Location.Row * PageRenderOptions.GridSize);
                        step++;
                    }
                    // --
                    currentCell.Walkable = false;
                    foreach (var nextCell in PathFinder.GetNextCells(this.Table, currentCell, endCell))
                    {
                        cellQueue.Enqueue(nextCell, nextCell.Priority);
                    }
                }
            }

            var traceCell = endCell;
            var cellPoints = new List<Point>();
            while (traceCell != null)
            {
                var traceCellLoc = traceCell.Location;
                traceCellLoc.Scale(PageRenderOptions.GridSize);
                cellPoints.Add(new Point(traceCellLoc.Col, traceCellLoc.Row));
                traceCell = traceCell.Parent;
            }
            cellPoints.Reverse();

            return cellPoints.ToArray();
        }

        private static Cell[] GetNextCells(Cell[,] table, Cell currentCell, Cell endCell)
        {
            var nextCells = new List<Cell>();
            foreach (var nextCellOffset in Cell.All)
            {
                var nextRow = currentCell.Location.Row + nextCellOffset.Row;
                var nextCol = currentCell.Location.Col + nextCellOffset.Col;
                if (nextRow < 0) continue;
                if (nextCol < 0) continue;
                if (nextRow >= table.GetLength(0)) continue;
                if (nextCol >= table.GetLength(1)) continue;
                var nextCell = table[nextRow, nextCol];
                if (nextCell.Walkable)
                {
                    nextCell.Priority = 0;
                    nextCell.Direction = nextCellOffset;
                    nextCell.RowEffort = nextCell.Location.GetRowEffort(endCell.Location);
                    nextCell.ColEffort = nextCell.Location.GetColEffort(endCell.Location);
                    if (nextCell.Direction == currentCell.Direction)
                    {
                        if (nextCell.Direction == Cell.Up || nextCell.Direction == Cell.Down)
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
                        if (nextCell.Direction == Cell.Left || nextCell.Direction == Cell.Right)
                        {
                            if (nextCell.ColEffort > currentCell.ColEffort)
                            {
                                nextCell.Priority = nextCell.RowEffort + nextCell.ColEffort;
                            }
                            // The endCell is always on top of the end-node rectangle.
                            // Therefore, do not prioritize the cells directly under it.
                            // The value '3' is half the end-node rectangle width -- should not be hard coded.
                            else if (nextCell.ColEffort == 3)
                            {
                                nextCell.Priority = nextCell.RowEffort + nextCell.ColEffort;
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
                    nextCell.Parent = currentCell;
                    nextCells.Add(nextCell);
                }
            }
            return nextCells.OrderBy(node => node.Priority).ToArray();
        }

        private static Cell[,] CreateTable(Graphics g, IEnumerable<Node> nodes)
        {
            var maxRow = 0;
            var maxCol = 0;
            foreach (var node in nodes)
            {
                maxRow = Math.Max(maxRow, node.Bounds.Bottom);
                maxCol = Math.Max(maxCol, node.Bounds.Right);
            }
            var table = new Cell[maxRow, maxCol];
            for (var row = 0; row < maxRow; row++)
            {
                for (var col = 0; col < maxCol; col++)
                {
                    table[row, col] = new Cell(row, col);
                }
            }
            foreach (var node in nodes)
            {
                for (var row = node.Bounds.Y; row <= node.Bounds.Bottom; row += PageRenderOptions.GridSize)
                {
                    for (var col = node.Bounds.X; col <= node.Bounds.Right; col += PageRenderOptions.GridSize)
                    {
                        if (row < 0 || col < 0 || row > table.GetUpperBound(0) || col > table.GetUpperBound(1))
                        {
                            continue;
                        }
                        table[row / PageRenderOptions.GridSize, col / PageRenderOptions.GridSize].Walkable = false;
                        if (g != null) g.DrawRectangle(Pens.Purple, col, row, 4, 4);
                    }
                }
            }
            return table;
        }
    }
}
