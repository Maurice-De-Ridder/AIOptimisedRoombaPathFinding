using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiVisualisation
{
    public class Grid
    {
        public GridObject[,] Columns { get; set; }

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

        public bool InsertObject(GridObject gridObject, int x, int y, int width, int length)
        {
            try
            {
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (Columns[x + i, x + j].GetChar() != 'o')
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
    }
}
