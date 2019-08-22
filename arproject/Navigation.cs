using System;
using System.Collections.Generic;
using System.Linq;

namespace Leetcode.arproject
{
    public class Navigation
    {
        public static void Test()
        {
            var maze = new int[][]
            {
                new []{0,0,0,0,0},
                new []{1,1,0,1,1},
                new []{1,1,0,0,0},
                new []{0,0,0,1,0},
                new []{0,1,0,0,1}
            };
            var starter = new Tuple<int,int>(4,0);
            var end = new Tuple<int,int>(0,4);
            var instructions = Navigation.Instance.Instructions(maze, starter.Item1, starter.Item2, 1, end.Item1, end.Item2);
            Console.WriteLine($"");
            foreach (var instruction in instructions)
            {
                string inst;
                if (instruction == 0)
                {
                    inst = "Move Forward";
                }
                else if (instruction == 1)
                {
                    inst = "Turn Left";
                }
                else if (instruction == 2)
                {
                    inst = "Turn Right";
                }
                else throw new NotSupportedException($"Error instruction:{instruction}");
                Console.Write($"{inst}\t");
            }
        }
        private Navigation()
        {
            
        }
        private static Navigation _instance;

        static Navigation()
        {
            _instance = new Navigation();
        }
        public static Navigation Instance => _instance;
        private const int MAX = int.MaxValue>>1;
        private static int DistanceToEnd(int x, int y, int endX, int endY)
        {
            return Math.Abs(x - endX) + Math.Abs(y - endY);
        }
        
        public class Vertex:IComparable<Vertex>
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Vertex Previous { get; set; }
            public int Distance { get; set; }
            public int HeuristicDistance => Distance + DistanceToEnd(X, Y, _endX, _endY);
            public Vertex()
            {
                Distance = -1;
            }

            public int CompareTo(Vertex other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(null, other)) return 1;
                var d = DistanceToEnd(X, Y, _endX, _endY) + Distance;
                var dother = DistanceToEnd(other.X, other.Y, _endX,_endY) + other.Distance;
                return -(d - dother);
            }
        }

        private static int _endX;
        private static int _endY;
        private int[][] _maze;
        private Vertex[][] _map; 
        private int[][] _arounds = new[] {new[] {-1, 0}, new[] {0, 1}, new[] {1, 0}, new[] {0, -1}};
        //always has one path
        public List<int> Instructions(int[][] maze,int x,int y,int startDirection,int endX,int endY)//maze:1-block,0-available
        {
            var rows = maze.Length;
            var cols = maze[0].Length;
            _maze = maze;
            _endX = endX;
            _endY = endY;
            _map = new Vertex[rows][];
            FindPath(x, y, rows, cols);
            if(_map[endX][endY].Previous==null)return new List<int>();
            var pathLinked = new LinkedList<Vertex>();
            var p = _map[endX][endY];
            while (p != null)
            {
                pathLinked.AddFirst(p);
                p = p.Previous;
            }

            var path = pathLinked.ToList();
            return ConstructInstructions(path, startDirection);
        }
        //0:move ,1 turn left, 2 turn right
        private List<int> ConstructInstructions(List<Vertex> path,int direction)
        {
            var rs = new List<int>();
            var starter = path[0];
            var starterNext = path[1];
            var firstTurn = 0;
            for (var j = 0; j <= 3; j++)
            {
                var d = (direction + j) % 4;
                var around = _arounds[d];
                if (starterNext.X == starter.X + around[0] && starterNext.Y == starter.Y + around[1])
                {
                    firstTurn = j;
                    direction = d;
                    break;
                }
            }

            switch (firstTurn)
            {
                case 0: rs.Add(0);
                    break;
                case 1:rs.Add(2); rs.Add(0);
                    break;
                case 2:rs.Add(2);rs.Add(2);rs.Add(0);
                    break;
                case 3:rs.Add(1);rs.Add(0);
                    break;
            }
            for (var i = 1; i < path.Count-1; i++)
            {
                var node = path[i];
                var next = path[i + 1];
                var turns = -1;
                for (var j = 0; j <= 3; j++)
                {
                    var d = (direction + j) % 4;
                    var around = _arounds[d];
                    if (next.X == node.X + around[0] && next.Y == node.Y + around[1])
                    {
                        turns = j;
                        direction = d;
                        break;
                    }
                }
                if(turns == -1 || turns == 2)throw new  NotSupportedException($"Turns can't be {turns}");
                if (turns == 0)
                {
                    rs.Add(0);
                }
                else if (turns == 1)
                {
                    rs.Add(2);
                    rs.Add(0);
                }
                else
                {
                    rs.Add(1);
                    rs.Add(0);
                }
            }
            return rs;
        }

        private void FindPath(int x, int y, int rows, int cols)
        {
            for (var i = 0; i < rows; i++)
            {
                _map[i] = new Vertex[cols];
                for (var j = 0; j < cols; j++)
                {
                    if (_maze[x][y] == 0)
                    {
                        _map[i][j] = new Vertex {Distance = MAX, Previous = null, X = i, Y = j};
                    }
                    else
                    {
                        _map[i][j] = new Vertex {Distance = -1, Previous = null, X = i, Y = j};
                    }
                }
            }

            _map[x][y].Distance = 0;
            var linked = new List<Vertex>();
            linked.Add(_map[x][y]);
            while (_map[_endX][_endY].Previous == null)
            {
                var nearest = linked[0];
                foreach (var vertex in linked)
                {
                    if (vertex.HeuristicDistance < nearest.HeuristicDistance)
                    {
                        nearest = vertex;
                    }
                }

                linked.Remove(nearest);
                for (var i = 0; i < _arounds.Length; i++)
                {
                    var around = _arounds[i];
                    if (Valid(nearest.X + around[0], nearest.Y + around[1]))
                    {
                        var xx = nearest.X + around[0];
                        var yy = nearest.Y + around[1];
                        if (_map[xx][yy].Distance > nearest.Distance + 1)
                        {
                            _map[xx][yy].Distance = nearest.Distance + 1;
                            _map[xx][yy].Previous = nearest;
                            linked.Add(_map[xx][yy]);
                        }
                    }
                }
            }

            var p = _map[_endX][_endY];
            while (p != null)
            {
                Console.Write($"({p.X},{p.Y}) <- ");
                p = p.Previous;
            }
        }

        private bool Valid(int x, int y)
        {
            var rows = _map.Length;
            var cols = _map[0].Length;
            if (x < 0 || y < 0 || x == rows || y == cols || _maze[x][y] == 1) return false;
            return true;
        }
        private Vertex ConstructVertex(int[][] maze,int x,int y,int endX,int endY,int distance,Vertex previous)
        {
            var rows = maze.Length;
            var cols = maze[0].Length;
            if (x < 0 || y < 0 || x == rows || y == cols || maze[x][y] == 1) return null;
            return new Vertex{X = x,Y = y,Distance = distance,Previous = previous};
        }
        
        
    }
}