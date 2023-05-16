using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiVisualisation
{
    public class Grid : ICloneable
    {
        public GridObject[,] Columns { get; set; }

        public Grid() { }

        public Grid(GridObject[,] other) 
        { 
            Columns = (GridObject[,])other.Clone();
        }

        public object Clone()
        {
            int rows = Columns.GetLength(1);
            int cols = Columns.GetLength(0);

            GridObject[,] cloneGrid = new GridObject[rows, cols]; 

            for(int i = 0; i < rows;i++)
            {
                for(int j = 0; j < cols; j++)
                {
                    cloneGrid[i,j] = Columns[i,j];
                }
            }
            return new Grid(cloneGrid);
        }


        public void InstantiateGrid(int sizeY, int sizeX)
        {
            Columns = new GridObject[sizeY,sizeX];

            for (int i = 0; i < Columns.GetLength(0); i++)
            {
                for (int y = 0; y < Columns.GetLength(1); y++)
                {
                    Columns[i, y] = new GridObject("o", true);
                }
            }
        }

        public void ClearGrid()
        {
            for (int i = 0; i < Columns.GetLength(0); i++)
            {
                for (int y = 0; y < Columns.GetLength(1); y++)
                {
                    if (Columns[i, y].GetChar() == 'C')
                    {
                        Columns[i, y] = new GridObject("o", true);
                    }
                }
            }
        }

        public bool InsertObject(GridObject gridObject, int x, int y, int width, int length)
        {
            try
            {
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (Columns[x + i, y + j].GetChar() != 'o')
                        {
                            return false;
                        }
                    }
                }

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Columns[x+i, y+j] = gridObject;
                    }
                }

                return true;
            }
            catch (Exception)
            {
                // will throw exception if the random gen is out of bounds
                return false;
            }
           
        }
        public (int,int) FindBase()
        {
            try
            {
                for (int i = 0; i < Columns.GetLength(0); i++)
                {
                    for (int j = 0; j < Columns.GetLength(1); j++)
                    {
                        if (Columns[i, j].GetChar() == 'B')
                        {
                            return (i, j);
                        }
                    }
                }
                return (0, 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return (0, 0);
            }
            
        }
        public void VisualizeGrid()
        {
            for (int i = 0; i < Columns.GetLength(0); i++)
            {
                for (int y = 0; y < Columns.GetLength(1); y++)
                {
                    Console.Write(Columns[i, y].GetChar() + " ");
                }
                Console.WriteLine();
            }
        }

        public int CalculateTileAmount(char charToCalculate)
        {
            int counter = 0;

            for (int i = 0; i < Columns.GetLength(0); i++)
            {
                for (int y = 0; y < Columns.GetLength(1); y++)
                {
                    if (Columns[i, y].GetChar() == charToCalculate)
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }

    }
}
