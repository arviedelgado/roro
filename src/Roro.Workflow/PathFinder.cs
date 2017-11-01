using Roro.Activities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;

namespace Roro.Workflow
{
    public class PathFinder
    {
        private int width = 800 / PageRenderOptions.GridSize;
        private int height = 600 / PageRenderOptions.GridSize;
        private Tile[,] map;
        private Tile start;
        private Tile end;

        public PathFinder(Graphics g, List<Node> nodes)
        {
            CreateMap(g, nodes);
            var A = nodes.First().Bounds.Center();
            var Z = nodes.Last().Bounds.Center();
            A.Offset(0, +PageRenderOptions.GridSize * 3);
            Z.Offset(0, -PageRenderOptions.GridSize * 3);
            this.start = new Tile(A.X / PageRenderOptions.GridSize, A.Y / PageRenderOptions.GridSize);
            this.end = new Tile(Z.X / PageRenderOptions.GridSize, Z.Y / PageRenderOptions.GridSize);

        }

        public Point[] FindPath()
        {
            Console.Clear();

            var path = new List<Point>
            {
                start.Location,
                end.Location
            };

            var q = new Queue<Tile>();
            q.Enqueue(this.start);
            while (q.Count > 0)
            {
                var tile = q.Dequeue();
                if (tile.Location == this.end.Location)
                {
                    path.Clear();
                    while (tile != null)
                    {
                        path.Add(tile.Location);
                        tile = tile.Parent;
                    }
                    path.Reverse();
                    Console.WriteLine("FOUND {0}", path.Count);
                    break;
                }
                if (tile.Walkable)
                {
                    tile.Walkable = false;
                    foreach (var next in tile.GetNexts(map))
                    {
                        q.Enqueue(next);
                    }
                }
            }            
            for (var i = 0; i < path.Count; i++)
            {
                var p = path[i];
                path[i] = new Point(p.X * PageRenderOptions.GridSize, p.Y * PageRenderOptions.GridSize);
                Console.WriteLine(p);
            }
            return path.ToArray();
        }

        public class Tile
        {
            private static Point[] moves = new Point[] {
                new Point(0, -1),
                new Point(0, +1),
                new Point(-1, 0),
                new Point(+1, 0),
            };

            public Point Location { get; }

            public Tile Parent { get; set; }

            public bool Walkable { get; set; }

            public Tile(int x, int y)
            {
                this.Location = new Point(x, y);
                this.Walkable = true;
            }
            public Tile[] GetNexts(Tile[,] map)
            {
                var nexts = new List<Tile>();
                foreach (var move in moves)
                {
                    var nextX = this.Location.X + move.X;
                    var nextY = this.Location.Y + move.Y;
                    if (nextX < 0) continue;
                    if (nextY < 0) continue;
                    if (nextX >= map.GetLength(0)) continue;
                    if (nextY >= map.GetLength(1)) continue;
                    var next = map[nextX, nextY];
                    if (next.Walkable)
                    {
                        next.Parent = this;
                        nexts.Add(next);
                    }
                }
                return nexts.ToArray();
            }
            public override string ToString()
            {
                return string.Format("[Tile X={0}, Y={1}, Walkable={2}]", this.Location.X, this.Location.Y, this.Walkable);
            }
        }

        private void CreateMap(Graphics g, IEnumerable<Node> nodes)
        {
            this.map = new Tile[width, height];
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    this.map[x, y] = new Tile(x, y);
                }
            }
            foreach (var node in nodes)
            {
                for (var x = node.Bounds.X; x <= node.Bounds.Right; x += PageRenderOptions.GridSize)
                {
                    for (var y = node.Bounds.Y; y <= node.Bounds.Bottom; y += PageRenderOptions.GridSize)
                    {
                        this.map[x / PageRenderOptions.GridSize, y / PageRenderOptions.GridSize].Walkable = false;
                        g.DrawRectangle(Pens.Purple, x - 2, y - 2, 4, 4);
                    }
                }
            }
        }
    }
}
