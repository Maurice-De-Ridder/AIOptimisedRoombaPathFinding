using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AiVisualisation
{
    public class RandomSearch
    {

        public static int RandomCleaningAI(Grid grid, Roomba roomba, int StartX, int StartY)
        {
            Random rnd = new Random();
            int rows = grid.Columns.GetLength(0);
            int cols = grid.Columns.GetLength(1);

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(new Node(StartX, StartY, 0));

            int tilesToClean = grid.CalculateTileAmount('o');

            int tiles = 0;
            int totalTilesPassed = 0;

            while (tiles < tilesToClean)
            {
                Console.Clear();
                Node current = queue.Dequeue();
                grid.VisualizeGrid();

                int nextX = rnd.Next(-1, 2);
                int nextY = rnd.Next(-1, 2);

                int newRow = current.Row + nextX;
                int newCol = current.Column + nextY;

                //Generate random positions untill x,y has not been visited.
                while (!CanDoWhileLoop(grid, newRow, newCol))
                {
                    nextX = rnd.Next(-1, 2);
                    nextY = rnd.Next(-1, 2);
                    newRow = current.Row + nextX;
                    newCol = current.Column + nextY;
                }

                ///Check of de volgende tile een O is.
                if (!(current.Row == StartX && current.Column == StartY))
                {
                    grid.Columns[current.Column, current.Row] = new GridObject("Clean", false);
                }

                if (grid.Columns[newCol, newRow].GetChar() == 'o')
                {
                    tiles++;
                }
                grid.Columns[newCol, newRow] = roomba.roomba;
                queue.Enqueue(new Node(newRow, newCol, current.Tiles + 1));
                totalTilesPassed++;
            }

            return totalTilesPassed;
        }
        
        public static bool CanDoWhileLoop(Grid grid, int newRow, int newCol)
        {
            if (!IsValidPos(grid, newRow, newCol))
            {
                return false;
            }
            if (!IsInBounds(grid, newRow, newCol))
            {
                return false;
            }
            return true;
        }
        public static bool IsValidPos(Grid grid, int row, int col)
        {
            int rows = grid.Columns.GetLength(0);
            int cols = grid.Columns.GetLength(1);

            Regex rx = new Regex(@"C|c|O|o|X|x");

            return IsInBounds(grid, row, col) && rx.IsMatch(grid.Columns[col, row].GetChar().ToString());
        }

        public static bool IsInBounds(Grid grid, int row, int col)
        {
            int rows = grid.Columns.GetLength(0);
            int cols = grid.Columns.GetLength(1);
            return row >= 0 && row < rows && col >= 0 && col < cols;
        }




    }
}
