using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AiVisualisation
{
    public class ObjectGod
    {
        public Grid DaddyGrid { get; set; }
        public Random rnd = new Random();
        public ObjectGod(Grid grid) 
        {
            DaddyGrid = grid;
        }

        public void InsertObject(string name, int width, int length)
        {
            int x = rnd.Next(DaddyGrid.Columns.GetLength(0));
            int y = rnd.Next(DaddyGrid.Columns.GetLength(1));


            (int width, int length) pos = SwapOrientation(width, length);

            while (!DaddyGrid.InsertObject(new GridObject(name, false), x, y, pos.width, pos.length))
            {
                x = rnd.Next(DaddyGrid.Columns.GetLength(0));
                y = rnd.Next(DaddyGrid.Columns.GetLength(1));
            }
        }
        public (int,int) SwapOrientation(int width,int length)
        {
            if (rnd.Next(2) == 0)
            {
                int oldWidth = width;
                width = length;
                length = oldWidth;
            }

            return (width,length);
        }
    }
}
