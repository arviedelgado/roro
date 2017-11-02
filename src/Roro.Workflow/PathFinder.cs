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

            var q = new Queue<Tile>(new[] { this.start });

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
                    tile.Walkable = false;
                    foreach (var next in tile.GetNexts(map, this.start, this.end))
                    {
                        q.Enqueue(next);
                    }
                }
            }

            var pts = new List<Point>();
            foreach (var at in ats)
            {
                at.Scale(PageRenderOptions.GridSize);
                pts.Add(new Point(at.Col, at.Row));
            }

            DrawMap(g);

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


            public int GetEffort(At other)
            {
                var rowEffort = (int)Math.Sqrt(Math.Pow(this.Row - other.Row, 2));
                var colEffort = (int)Math.Sqrt(Math.Pow(this.Col - other.Col, 2));
                if (rowEffort == 0 || colEffort == 0)
                    return 0;
                return rowEffort + colEffort;
            }
        }

        public class Tile
        {
            private static At[] moves = new At[] {
                new At(0, +1), // right
                new At(+1, 0), // down
                new At(-1, 0), // up
                new At(0, -1), // left
            };

            public At At { get; private set; }

            public int Effort { get; private set; }

            public Tile Parent { get; private set; }

            public bool Walkable { get; set; }

            public Tile(int row, int col)
            {
                this.At = new At(row, col);
                this.Walkable = true;
            }

            public Tile[] GetNexts(Tile[,] map, Tile start, Tile end)
            {
                var nexts = new List<Tile>();
                foreach (var move in moves)
                {
                    var nextRow = this.At.Row + move.Row;
                    var nextCol = this.At.Col + move.Col;
                    if (nextRow < 0) continue;
                    if (nextCol < 0) continue;
                    if (nextRow >= map.GetLength(0)) continue;
                    if (nextCol >= map.GetLength(1)) continue;
                    var next = map[nextRow, nextCol];
                    if (next.Walkable)
                    {
                        next.Effort = next.At.GetEffort(start.At) + next.At.GetEffort(end.At);
                        next.Parent = this;
                        nexts.Add(next);
                    }
                }

                nexts = nexts.OrderBy(node => node.Effort).ToList();
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
                        this.map[row / PageRenderOptions.GridSize, col / PageRenderOptions.GridSize].Walkable = false;
                        g.DrawRectangle(Pens.Purple, col, row, 4, 4);
                    }
                }
            }
        }
    }
}
