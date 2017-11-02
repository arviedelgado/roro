using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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

        public Point[] FindPath(Point start, Point end)
        {
            this.NotWalkable++; // increment every FindPath session.

            var startLoc = new CellLocation(start).Scale(-PageRenderOptions.GridSize).Offset(+2, 0);
            var endLoc = new CellLocation(end).Scale(-PageRenderOptions.GridSize).Offset(-3, 0);

            var traceCell = this.Table[endLoc.Row + 1, endLoc.Col];
            var startCell = this.Table[startLoc.Row, startLoc.Col];
            var endCell = this.Table[endLoc.Row, endLoc.Col];

            traceCell.Walkable = 0;
            startCell.Walkable = 0;
            endCell.Walkable = 0;

            traceCell.Parent = endCell;
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
                if (currentCell.Walkable < this.NotWalkable)
                {
                    // to remove
                    if (step < 100)
                    {
                        this.Graphics.DrawString(step.ToString(), new Font("Consolas", 8), Brushes.Blue, currentCell.Location.Col * PageRenderOptions.GridSize, currentCell.Location.Row * PageRenderOptions.GridSize);
                        step++;
                    }
                    // --
                    currentCell.Walkable = this.NotWalkable;
                    foreach (var nextCell in this.GetNextCells(currentCell, endCell))
                    {
                        cellQueue.Enqueue(nextCell, nextCell.Priority);
                    }
                }
            }

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

        private Cell[] GetNextCells(Cell currentCell, Cell endCell)
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
                var nextCell = this.Table[nextRow, nextCol];
                if (nextCell.Walkable < this.NotWalkable)
                {
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
                    nextCell.Parent = currentCell;
                    nextCells.Add(nextCell);
                }
            }
            return nextCells.OrderBy(node => node.Priority).ToArray();
        }

        private void CreateTable(Graphics g, IEnumerable<Node> nodes)
        {
            this.RowCount = 0;
            this.ColCount = 0;
            foreach (var node in nodes)
            {
                this.RowCount = Math.Max(this.RowCount, node.Bounds.Bottom);
                this.ColCount = Math.Max(this.ColCount, node.Bounds.Right);
            }
            this.Table = new Cell[this.RowCount, this.ColCount];
            for (var row = 0; row < this.RowCount; row++)
            {
                for (var col = 0; col < this.ColCount; col++)
                {
                    this.Table[row, col] = new Cell(row, col);
                }
            }
            foreach (var node in nodes)
            {
                for (var row = node.Bounds.Y; row <= node.Bounds.Bottom; row += PageRenderOptions.GridSize)
                {
                    for (var col = node.Bounds.X; col <= node.Bounds.Right; col += PageRenderOptions.GridSize)
                    {
                        if (row < 0 || col < 0 || row > this.RowCount || col > this.ColCount)
                        {
                            continue;
                        }

                        this.Table[row / PageRenderOptions.GridSize,
                                   col / PageRenderOptions.GridSize].Walkable = int.MaxValue; // Never.

                        if (g != null) g.DrawRectangle(Pens.Purple, col, row, 4, 4);
                    }
                }
            }
        }
    }
}
