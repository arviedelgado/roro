using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using SkiaSharp;

namespace Roro.Workflow
{
    public class PathFinder
    {
        private Cell[,] Table;

        private int RowCount;

        private int ColCount;

        private Guid Session;

        public PathFinder(IEnumerable<Node> nodes)
        {
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
                        this.Table[r, c] = new Cell(r, c) { IsOpen = false, IsWall = true };
                    }
                }
            }
        }

        public SKPath GetPath(Point startPt, Point endPt)
        {
            this.Session = Guid.NewGuid();

            var startLoc = new CellLocation(startPt.Y, startPt.X).Scale(1.0 / PageRenderOptions.GridSize);
            var endLoc = new CellLocation(endPt.Y, endPt.X).Scale(1.0 / PageRenderOptions.GridSize);

            var startCell = this.Table[startLoc.Row, startLoc.Col];
            var endCell = this.Table[endLoc.Row, endLoc.Col];

            startCell.IsOpen = true;
            endCell.IsOpen = true;

            endCell.Parent = startCell;

            return this.GetPath(startCell, endCell);
        }

        private SKPath GetPath(Cell startCell, Cell endCell)
        {
            if (startCell == null || endCell == null)
            {
                return new SKPath();
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

                if (nextCell.Session != this.Session)
                {
                    nextCell.Parent = null;
                    nextCell.CurrentCost = 0;
                    nextCell.IsOpen = nextCell == endCell || !nextCell.IsWall;
                    nextCell.Session = this.Session;
                }

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

        private SKPath TracePath(Cell startCell, Cell endCell)
        {
            var bending = false;
            var lineToCell = endCell;
            var endToStartPath = new SKPath();
            while (lineToCell != null)
            {
                var lineToPt = new SKPoint(
                    PageRenderOptions.GridSize * lineToCell.Location.Col,
                    PageRenderOptions.GridSize * lineToCell.Location.Row);
                if (lineToCell.Parent is Cell cornerCell &&
                    cornerCell.Parent is Cell arcToCell &&
                    lineToCell.Location.Col != arcToCell.Location.Col &&
                    lineToCell.Location.Row != arcToCell.Location.Row)
                {
                    var cornerPt = new SKPoint(
                        PageRenderOptions.GridSize * cornerCell.Location.Col,
                        PageRenderOptions.GridSize * cornerCell.Location.Row);

                    var arcToPt = new SKPoint(
                        PageRenderOptions.GridSize * arcToCell.Location.Col,
                        PageRenderOptions.GridSize * arcToCell.Location.Row);

                    if (endToStartPath.IsEmpty)
                    {
                        endToStartPath.MoveTo(lineToPt);
                    }
                    endToStartPath.ArcTo(cornerPt, arcToPt, PageRenderOptions.GridSize / 2);

                    bending = true;
                }
                else
                {
                    if (bending)
                    {
                        bending = false;
                    }
                    else
                    {
                        if (endToStartPath.IsEmpty)
                        {
                            endToStartPath.MoveTo(lineToPt);
                        }
                        else
                        {
                            endToStartPath.LineTo(lineToPt);
                        }
                    }
                }
                lineToCell = lineToCell.Parent;
            }

            // reverse path
            var startToEndPath = new SKPath();
            startToEndPath.AddPathReverse(endToStartPath);

            // add end arrow
            var lastPt = startToEndPath.LastPoint;
            var prevPt = startToEndPath.GetPoint(startToEndPath.PointCount - 2);
            var angle = Math.Atan2(lastPt.Y - prevPt.Y, lastPt.X - prevPt.X) * 180 / Math.PI;
            var arrow = new SKPath();
            arrow.MoveTo(lastPt);
            arrow.RLineTo(new SKPoint(-PageRenderOptions.GridSize / 2, +PageRenderOptions.GridSize / 2));
            arrow.MoveTo(lastPt);
            arrow.RLineTo(new SKPoint(-PageRenderOptions.GridSize / 2, -PageRenderOptions.GridSize / 2));
            arrow.Transform(SKMatrix.MakeRotationDegrees((float)angle, lastPt.X, lastPt.Y));
            startToEndPath.AddPath(arrow, SKPathAddMode.Append);

            return startToEndPath;
        }
    }
}
