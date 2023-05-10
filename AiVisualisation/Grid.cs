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
                    Console.Write(Columns[i, y].Name + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
