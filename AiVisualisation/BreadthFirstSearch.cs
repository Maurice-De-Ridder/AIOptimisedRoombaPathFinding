using System.Text.RegularExpressions;

namespace AiVisualisation
{

    public class Node
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Tiles { get; set; }
        public Node(int row, int col, int tiles)
        {
            Row = row;
            Column = col;
            Tiles = tiles;
        }
    }
    public class BreadthFirstSearch
    {
        public static int[,] Directions = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

        public static int BFS(Grid grid, int StartX, int StartY, int EndX, int EndY)
        {
            int rows = grid.Columns.GetLength(0);
            int cols = grid.Columns.GetLength(1);
            
            bool[,] visited = new bool[rows, cols];
            visited[StartX, StartY] = true;

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(new Node(StartX, StartY, 0));

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                

                if (current.Row == EndX && current.Column == EndY)
                {
                    return current.Tiles;
                }
                else if(current.Row == StartX && current.Column == StartY)
                {
                    //Do nothing to not change the Roomba position
                }
                else
                {
                    //New GridObject which is a path option the Roomba can take, this is purely for visualization purposes

                    //grid.Columns[current.Column, current.Row] = new GridObject("GoOption", false);
                }

                for (int i = 0; i < Directions.GetLength(0); i++)
                {
                    int newRow = current.Row + Directions[i, 0];
                    int newCol = current.Column + Directions[i, 1];

                    if (IsValidPos(grid, newRow, newCol) && !visited[newRow, newCol])
                    {
                        visited[newRow, newCol] = true;
                        queue.Enqueue(new Node(newRow, newCol, current.Tiles + 1));
                    }
                    
                }
            }
            // If the target is unreachable
            return -1;
        }


        public static int BFSCleaningNoEnding(Grid grid, int StartX, int StartY)
        {
            int rows = grid.Columns.GetLength(0);
            int cols = grid.Columns.GetLength(1);

            bool[,] visited = new bool[rows, cols];
            visited[StartX, StartY] = true;

            int tiles = 0;

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(new Node(StartX, StartY, 0));
            
            while (queue.Count > 0)
            {
                Console.Clear();
                Node current = queue.Dequeue();
                tiles++;
                grid.VisualizeGrid();

                if (!(current.Row == StartX && current.Column == StartY))
                {
                    grid.Columns[current.Column, current.Row] = new GridObject("Clean", false);
                }
                for (int i = 0; i < Directions.GetLength(0); i++)
                {
                    int newRow = current.Row + Directions[i, 0];
                    int newCol = current.Column + Directions[i, 1];
                   
                    if (IsValidPos(grid, newRow, newCol) && !visited[newRow, newCol])
                    {
                        
                        visited[newRow, newCol] = true;
                        queue.Enqueue(new Node(newRow, newCol, current.Tiles + 1));
                    }
                }
            }
            // If the target is unreachable
            return tiles;
        }


        public static bool IsValidPos(Grid grid, int row, int col)
        {
            int rows = grid.Columns.GetLength(0);
            int cols = grid.Columns.GetLength(1);

            Regex rx = new Regex(@"C|c|O|o|X|x|B|b");

            return IsInBounds(grid,row,col) && rx.IsMatch(grid.Columns[col, row].GetChar().ToString());
        }

        public static bool IsInBounds(Grid grid, int row,int col)
        {
            int rows = grid.Columns.GetLength(0);
            int cols = grid.Columns.GetLength(1);
            return row >= 0 && row < rows && col >= 0 && col < cols;
        }
    }

}