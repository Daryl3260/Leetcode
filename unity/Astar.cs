using System;
using System.Collections.Generic;

namespace Leetcode.leetcode_cn.leetcode_cn.unity
{
    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Distance { get; set; }
        public Node previous { get; set; }
    }
    public class Astar:Dijkstra
    {
        public Astar()
        {
            _comparer = Comparer<Node>.Create((node0, node1) =>
            {
                var d0 = node0.Distance + Math.Abs(node0.X - _end.X) + Math.Abs(node0.Y - _end.Y);
                var d1 = node1.Distance + Math.Abs(node1.X - _end.X) + Math.Abs(node1.Y - _end.Y);
                return -(d0 - d1);
            });
        }
    }

    public class Dijkstra
    {
        protected  Node _start;
        protected  Node _end;
        protected Node[][] _map;
        protected  int _rows;
        protected  int _cols;
        protected Comparer<Node> _comparer;
        protected const int Max = int.MaxValue >> 1; 
        public Dijkstra()
        {
            _comparer = Comparer<Node>.Create(((node, node1) => -(node.Distance-node1.Distance)));
        }
        public List<Node> ShortestPath(int startX,int startY,int endX,int endY, int[][] map)//0:can pass,-1:blocked
        {
            _rows = map.Length;
            _cols = map[0].Length;
            _map = new Node[_rows][];
            for (int i = 0; i < _rows; i++)
            {
                _map[i] = new Node[_cols];
                for (int j = 0; j < _cols; j++)
                {
                    if (map[i][j] != 1)
                    {
                        _map[i][j] = new Node {X = i, Y = j, Distance = Max};
                    }
                    else
                    {
                        _map[i][j] = new Node {X = i, Y = j, Distance = -1};
                    }
                }
            }

            _start = _map[startX][startY];
            _end = _map[endX][endY];
            _start.Distance = 0;
//            _start = start;
//            _start.Distance = 0;
//            _end = end;
//            _visited= new bool[_rows][];
//            for (int i = 0; i < _rows; i++)
//            {
//                _visited[i]=new bool[_cols];
//            }
            var rs = new List<Node>();
//            PriorityQueue<Node> pq = new PriorityQueue<Node>(((node, node1) => _map[node.X][node.Y]-_map[node1.X][node1.Y]));
//            pq.Add(_start);
//            _visited[_start.X][_start.Y] = true;
            List<Node> sortedList = new List<Node>();
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    if (_map[i][j].Distance != -1)
                    {
                        sortedList.Add(_map[i][j]);
                    }
                }
            }
            sortedList.Sort(_comparer);
            while (true)
            {
                var top = sortedList[sortedList.Count - 1];
                sortedList.RemoveAt(sortedList.Count - 1);
                if (top.X == _end.X && top.Y == _end.Y)
                {
                    break;
                }
                UpdateNearNodes(top);
                sortedList.Sort(_comparer);
            }
            if(_end.Distance==Max)return new List<Node>();
            var p = _end;
            while (true)
            {
                rs.Add(p);
                if (p == _start) break;
                else
                {
                    p = p.previous;
                }
            }
            return rs;
        }

        private void UpdateNearNodes(Node top)
        {
            int x = top.X;
            int y = top.Y;
            UpdateNode(x-1, y, top);
            UpdateNode(x+1,y,top);
            UpdateNode(x,y-1,top);
            UpdateNode(x,y+1,top);
        }

        private void UpdateNode(int x, int y,Node prev)
        {
            var distance = prev.Distance;
            if (-1<x&&x<_rows&&-1<y&&y<_cols&& _map[x][y].Distance != -1 && _map[x][y].Distance > distance + 1)
            {
                _map[x][y].Distance = distance + 1;
                _map[x][y].previous = prev;
            }
        }
    }
}