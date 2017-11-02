using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Roro.Workflow
{
    public class PathFinder
    {
        private Tile[,] map;
        private Tile start;
        private Tile end;

        public PathFinder(Graphics g, List<Node> nodes)
        {
            CreateMap(g, nodes);
            var A = new At(nodes.First().Bounds.Center());
            var Z = new At(nodes.Last().Bounds.Center());
            A.Scale(-PageRenderOptions.GridSize);
            Z.Scale(-PageRenderOptions.GridSize);
            A.Offset(+2, 0);
            Z.Offset(-2, 0);
            this.start = map[A.Row, A.Col];
            this.start.Walkable = true;
            this.end = map[Z.Row, Z.Col];
            this.end.Walkable = true;
        }

        public Point[] FindPath(Graphics g)
        {
            Console.Clear();
            var ats = new List<At>
            {
                start.At,
                end.At
            };

            var q = new PriorityQueue<Tile>();
            q.Enqueue(this.start, 0);

            int step = 0;
            while (q.Count > 0)
            {
                var tile = q.Dequeue();
                if (tile.At == this.end.At)
                {
                    ats.Clear();
                    while (tile != null)
                    {
                        ats.Add(tile.At);
                        tile = tile.Parent;
                    }
                    ats.Reverse();
                    Console.WriteLine("FOUND {0}", ats.Count);
                    break;
                }
                if (tile.Walkable)
                {
                    if (step < 100)
                    {
                        g.DrawString(step.ToString(), new Font("Consolas", 8), Brushes.Blue, tile.At.Col * PageRenderOptions.GridSize, tile.At.Row * PageRenderOptions.GridSize);
                        step++;
                    }
                    tile.Walkable = false;
                    foreach (var next in tile.GetNexts(map, this.start, this.end))
                    {
                        q.Enqueue(next, next.Priority);
                    }
                }
            }

            var pts = new List<Point>();
            foreach (var at in ats)
            {
                at.Scale(PageRenderOptions.GridSize);
                pts.Add(new Point(at.Col, at.Row));
            }

            //DrawMap(g);

            return pts.ToArray();
        }

        public void DrawMap(Graphics g)
        {
            for (int row = 0, rowLen = map.GetLength(0); row < rowLen; row++)
            {
                for (int col = 0, colLen = map.GetLength(1); col < colLen; col++)
                {
                    if (map[row, col].Walkable == false)
                    {
                        g.DrawRectangle(Pens.Orange, col * PageRenderOptions.GridSize, row * PageRenderOptions.GridSize, 6, 6);
                    }
                }
            }
        }

        public struct At
        {
            public int Row { get; set; }

            public int Col { get; set; }

            public At(int row, int col)
            {
                this.Row = row;
                this.Col = col;
            }

            public At(Point pt)
            {
                this.Row = pt.Y;
                this.Col = pt.X;
            }

            public void Offset(int row, int col)
            {
                this.Row += row;
                this.Col += col;
            }

            public void Scale(int scale)
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
            }

            public static bool operator ==(At a, At z)
            {
                return a.Equals(z);
            }

            public static bool operator !=(At a, At z)
            {
                return !a.Equals(z);
            }

            public override bool Equals(object obj)
            {
                return obj is At other && this.Row == other.Row && this.Col == other.Col;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override string ToString()
            {
                return string.Format("[At Row={0}, Col={1}]", this.Row, this.Col);
            }

            public int GetRowEffort(At other)
            {
                return (int)Math.Sqrt(Math.Pow(this.Row - other.Row, 2));
            }

            public int GetColEffort(At other)
            {
                return (int)Math.Sqrt(Math.Pow(this.Col - other.Col, 2));
            }
        }

        public static class Move
        {
            public static readonly At Up = new At(-1, 0);
            public static readonly At Down = new At(+1, 0);
            public static readonly At Left = new At(0, -1);
            public static readonly At Right = new At(0, +1);
            public static readonly At[] All = new At[] { Up, Down, Left, Right };
        };

        public class Tile
        {
            public At At { get; private set; }

            public At Direction { get; private set; }

            public Tile Parent { get; private set; }

            public int Priority { get; private set; }

            public int RowEffort { get; private set; }

            public int ColEffort { get; private set; }
            
            public bool Walkable { get; set; }

            public Tile(int row, int col)
            {
                this.At = new At(row, col);
                this.RowEffort = int.MaxValue;
                this.ColEffort = int.MaxValue;
                this.Walkable = true;
            }

            public Tile[] GetNexts(Tile[,] map, Tile start, Tile end)
            {
                var nexts = new List<Tile>();
                foreach (var dir in Move.All)
                {
                    var nextRow = this.At.Row + dir.Row;
                    var nextCol = this.At.Col + dir.Col;
                    if (nextRow < 0) continue;
                    if (nextCol < 0) continue;
                    if (nextRow >= map.GetLength(0)) continue;
                    if (nextCol >= map.GetLength(1)) continue;
                    var next = map[nextRow, nextCol];
                    if (next.Walkable)
                    {
                        next.Priority = 0;
                        next.Direction = dir;
                        next.RowEffort = next.At.GetRowEffort(end.At);
                        next.ColEffort = next.At.GetColEffort(end.At);
                        if (next.Direction == this.Direction)
                        {
                            if (next.Direction == Move.Up || next.Direction == Move.Down)
                            {
                                if (next.RowEffort > this.RowEffort) // we walked too much
                                {
                                    next.Priority = next.RowEffort + next.ColEffort;
                                }
                                else
                                {
                                    next.Priority = next.RowEffort;
                                }
                            }
                            if (next.Direction == Move.Left || next.Direction == Move.Right)
                            {
                                if (next.ColEffort > this.ColEffort)
                                {
                                    next.Priority = next.RowEffort + next.ColEffort;
                                }
                                else if (next.ColEffort == 3)
                                {
                                    next.Priority = next.RowEffort + next.ColEffort;
                                }
                                else
                                {
                                    next.Priority = next.ColEffort;
                                }
                            }
                        }
                        else
                        {
                            next.Priority = next.RowEffort + next.ColEffort;
                        }
                        next.Parent = this;
                        nexts.Add(next);
                    }
                }

                nexts = nexts.OrderBy(node => node.Priority).ToList();
                return nexts.ToArray();
            }

            public override string ToString()
            {
                return string.Format("[Tile Row={0}, Col={1}, Walkable={2}]", this.At.Row, this.At.Col, this.Walkable);
            }
        }

        private void CreateMap(Graphics g, IEnumerable<Node> nodes)
        {
            var maxRow = 0;
            var maxCol = 0;
            foreach (var node in nodes)
            {
                maxRow = Math.Max(maxRow, node.Bounds.Bottom);
                maxCol = Math.Max(maxCol, node.Bounds.Right);
            }
            this.map = new Tile[maxRow, maxCol];
            for (var row = 0; row < maxRow; row++)
            {
                for (var col = 0; col < maxCol; col++)
                {
                    this.map[row, col] = new Tile(row, col);
                }
            }
            foreach (var node in nodes)
            {
                for (var row = node.Bounds.Y; row <= node.Bounds.Bottom; row += PageRenderOptions.GridSize)
                {
                    for (var col = node.Bounds.X; col <= node.Bounds.Right; col += PageRenderOptions.GridSize)
                    {
                        if (row < 0 || col < 0 || row > this.map.GetUpperBound(0) || col > this.map.GetUpperBound(1))
                            continue;
                        this.map[row / PageRenderOptions.GridSize, col / PageRenderOptions.GridSize].Walkable = false;
                        g.DrawRectangle(Pens.Purple, col, row, 4, 4);
                    }
                }
            }
        }
    }
}
