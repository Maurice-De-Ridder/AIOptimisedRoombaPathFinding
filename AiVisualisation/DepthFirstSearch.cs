using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AiVisualisation
{
    public class DepthFirstSearch
    {

        // {0,0} First is left right, second is up down.
        // Example left right: -1 is left, 1 is right
        // Example up down: -1 is up, 1 is down.
        public static int[,] Directions = { { 1, 0 }, { 0, -1 }, { 0, 1 }, { -1, 0 } };


        public static int DFSCleaningAI(Grid grid, Roomba roomba, int StartX, int StartY)
        {
            int rows = grid.Columns.GetLength(0);
            int cols = grid.Columns.GetLength(1);

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(new Node(StartX, StartY, 0));

            int tilesToClean = grid.CalculateTileAmount('o');
            int tiles = 0;
            int totalTilesPassed = 0;

            bool continueLoop = true;

            while(continueLoop) 
            {
                Console.Clear();
                Node current = queue.Dequeue();
                grid.VisualizeGrid();


                //First go below if possible
                int dir = 0;

                int newRow = current.Row + Directions[dir,1];
                int newCol = current.Column + Directions[dir,0];


                //Change directions if not possible, following order is necessary down, left, Right,Up
                while (!CanDoWhileLoop(grid, newRow, newCol))
                {
                    dir++;
                    if(dir > 3)
                    {
                        continueLoop= false;
                        newRow = current.Row + 0;
                        newCol = current.Column + 0;
                        break;
                    }
                    else
                    {
                        newRow = current.Row + Directions[dir, 1];
                        newCol = current.Column + Directions[dir, 0];
                    }
                }


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

                if(tiles >= tilesToClean)
                {
                    continueLoop = false;
                }

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
            Regex rx = new Regex(@"O|o|X|x");
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
