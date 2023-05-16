using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AiVisualisation
{
    public class DepthFirstSearch
    {
        public class NewNode
        {
            public int Row { get; set; }
            public int Column { get; set; }
            public int Tiles { get; set; }

            public List<NewNode> PreviousNodes { get; set; }
            public NewNode(int row, int col, int tiles,List<NewNode> previousNodes)
            {
                Row = row;
                Column = col;
                Tiles = tiles;
                this.PreviousNodes = previousNodes;
            }
        }

        public class Coordinate
        {
            public int row { get; set;}
            public int col { get; set;}
        }

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
                while (!CanDoWhileLoop2(grid,newRow, newCol))
                {
                    dir++;
                    if(dir > 3)
                    {
                        //ZORG ERVOOr dat die een DFS doet op de eerst volgende lege tile en begin daar weer verder

                        //For loop om te checken waar de dichtsbijzijnde O is.
                        try
                        {
                            Coordinate c = FindNearestDirtyTile(grid, current.Row, current.Column);
                            TraverseToNearestDirtyTile(grid, c, roomba, current.Row, current.Column);
                            dir = 0;
                        }
                        catch(MissingMemberException MSG)
                        {
                            Console.WriteLine(MSG);
                            continueLoop = false;
                            newRow = current.Row + 0;
                            newCol = current.Column + 0;
                            break;
                        }
                        
                        //For loop traversed naar de dichtsbijzijnde 0, gebruik parameters van hudige plaats roomba en van de 0
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

                if(tiles > tilesToClean)
                {
                    continueLoop = false;
                }

            }
            return totalTilesPassed;
        }


        public static Coordinate FindNearestDirtyTile(Grid grid, int currentX, int currentY)
        {

            for (int i = currentY; i < grid.Columns.GetLength(0); i++)
            {
                for (int y = currentX; y < grid.Columns.GetLength(1); y++)
                {
                    if(grid.Columns[i,y].GetChar() == 'o')
                    {
                        return new Coordinate{ col = i, row = y};
                    }
                }
            }

            throw new MissingMemberException("Missing dirty tile");
        }


        public static void TraverseToNearestDirtyTile(Grid grid, Coordinate dirty,Roomba roomba, int currentX, int currentY)
        {

            //Determines if roomba should traverse left or right.
            int rowDistance = dirty.row - currentX;
            int colDistance = dirty.col - currentY;
            for (int x = 0; x < 4; x++)
            {
                // 1. traverse in the grid
                int newPotentialRow = currentX - Directions[x, 1];
                int newPotentialCol = currentY - Directions[x, 0];

                int potentialRowDistance = dirty.row - newPotentialRow;
                int potentialColDistance = dirty.col - newPotentialCol;

                // 2. check if after the traversal the distance has gotten smaller
                // 2.1 if yes, traverse to that point
                if(IsInBounds2(grid, newPotentialRow, newPotentialCol))
                {
                    if (grid.Columns[newPotentialCol, newPotentialRow].GetChar() == 'o')
                    {
                        x = 5;
                    }
                    //Check if distance is Plus or minus
                    if (rowDistance >= 0 && colDistance > 0 || rowDistance > 0 && colDistance >= 0)
                    {
                        if (potentialRowDistance < rowDistance || potentialColDistance < colDistance)
                        {
                            grid.Columns[currentY, currentX] = new GridObject("Clean", false);
                            grid.Columns[newPotentialCol, newPotentialRow] = roomba.roomba;

                            currentY = newPotentialCol;
                            currentX = newPotentialRow;
                            Console.Clear();
                            grid.VisualizeGrid();
                            if(x != 5)
                            {
                                x = 0;
                            }
                        }
                    }
                    else
                    {
                        if (potentialRowDistance > rowDistance || potentialColDistance > colDistance)
                        {
                            grid.Columns[currentY, currentX] = new GridObject("Clean", false);
                            grid.Columns[newPotentialCol, newPotentialRow] = roomba.roomba;

                            currentY = newPotentialCol;
                            currentX = newPotentialRow;
                            Console.Clear();
                            grid.VisualizeGrid();
                            if (x != 5)
                            {
                                x = 0;
                            }
                        }
                    }
                }
   
                // 2,2 if no, do nothing 
            }
  

        }

        public static bool CanDoWhileLoop2(Grid grid, int newRow, int newCol)
        {
            if (!IsValidPos2(grid, newRow, newCol))
            {
                return false;
            }
            if (!IsInBounds2(grid, newRow, newCol))
            {
                return false;
            }
            return true;
        }
        public static bool IsValidPos2(Grid grid, int row, int col)
        {
            Regex rx = new Regex(@"O|o|X|x");
            return IsInBounds2(grid, row, col) && rx.IsMatch(grid.Columns[col, row].GetChar().ToString());
        }

        public static bool IsInBounds2(Grid grid, int row, int col)
        {
            int rows = grid.Columns.GetLength(0);
            int cols = grid.Columns.GetLength(1);
            return row >= 0 && row < rows && col >= 0 && col < cols;
        }




        public static List<List<NewNode>> DFSAI(Grid og, Roomba roomba, int StartX, int StartY)
        {
            Grid grid = (Grid) og.Clone();
            int rows = grid.Columns.GetLength(0);
            int cols = grid.Columns.GetLength(1);


            int tilesToClean = grid.CalculateTileAmount('o');
            int totalTilesPassed = 0;
            List<List<NewNode>> route = new List<List<NewNode>>();
            bool[,] visited = new bool[rows, cols];

            Queue<NewNode> queue = new Queue<NewNode>();
            List<NewNode> previousNodeList = new List<NewNode>();
            queue.Enqueue(new NewNode(StartX, StartY, 0, previousNodeList));

            while (queue.Count > 0)
            {
                Console.Clear();
                NewNode current = queue.Dequeue();
                current.PreviousNodes.Add(current);
                grid.Columns[current.Column, current.Row] = roomba.roomba;
                grid.VisualizeGrid();
                visited[current.Row, current.Column] = true;
                
                for (int i = 0; i < 4; i++)
                {
                    int newRow = current.Row + Directions[i, 1];
                    int newCol = current.Column + Directions[i, 0];

                    if (IsValidPos(grid, current.Row, current.Column, newRow, newCol) && !visited[newRow, newCol])
                    {
                        List<NewNode> newNodes = new List<NewNode>(current.PreviousNodes);
                        newNodes.Add(current);
                        queue.Enqueue(new NewNode(newRow, newCol, current.Tiles + 1, newNodes));
                    } 

                    totalTilesPassed++;
                    Console.WriteLine("Tiles: " + current.Tiles);
                }
                grid.Columns[current.Column, current.Row] = new GridObject("Clean", false);
                //Make sure the next one can clean it
                
                if(current.PreviousNodes.Count() + 3 >= tilesToClean)
                {
                    route.Add(current.PreviousNodes);
                }
                

            }
            return route;
        }


        public static bool CanDoWhileLoop(Grid grid, int currentX, int currentY, int newRow, int newCol)
        {
            if (!IsValidPos(grid, currentX, currentY, newRow, newCol))
            {
                return false;
            }
            if (!IsInBounds(grid, currentX, currentY, newRow, newCol))
            {
                return false;
            }
            return true;
        }
        public static bool IsValidPos(Grid grid, int currentX, int currentY, int row, int col)
        {
            Regex rx = new Regex(@"O|o|X|x|C|c");
            return IsInBounds(grid, currentX, currentY, row, col) && rx.IsMatch(grid.Columns[col, row].GetChar().ToString());
        }

        public static bool IsInBounds(Grid grid, int currentX, int currentY, int row, int col)
        {
            int rows = grid.Columns.GetLength(0);
            int cols = grid.Columns.GetLength(1);
            return row >= 0 && row < rows && col >= 0 && col < cols && Math.Abs(row - currentX) + Math.Abs(col - currentY) == 1;
        }

    }
}
