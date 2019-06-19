namespace Leetcode.hifreq2018.graph
{
    namespace p1
    {
        public class Solution
        {
            private int count;
            private const char island = '1';
            private const char sea = '0';
            public int NumIslands(char[][] grid)
            {
                if (grid == null || grid.Length == 0 || grid[0].Length == 0) return 0;
                count = 0;
                int rows = grid.Length;
                int cols = grid[0].Length;
                bool[][] visited = new bool[rows][];
                for (int i = 0; i < rows; i++)
                {
                    visited[i]=new bool[cols];
                }

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (!visited[i][j] && grid[i][j] == island)
                        {
                            count++;
                            TagNeighbors(grid,i,j,visited);
                        }
                    }
                }

                return count;
            }

            private void TagNeighbors(char[][] grid, int i, int j,bool[][] visited)
            {
                int rows = grid.Length;
                int cols = grid[0].Length;
                if (i < 0 || i == rows || j < 0 || j == cols) return;
                if (!visited[i][j] && grid[i][j] == island)
                {
                    visited[i][j] = true;
                    TagNeighbors(grid,i+1,j,visited);
                    TagNeighbors(grid,i-1,j,visited);
                    TagNeighbors(grid,i,j+1,visited);
                    TagNeighbors(grid,i,j-1,visited);
                }
            }
        }
    }
}