using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiVisualisation
{
    public class Roomba
    {
        private int XLoc;
        private int YLoc;
        private Grid DaddyGrid;
        public GridObject roomba = new GridObject("Roomba", false);
        public Roomba(Grid grid,int xLoc, int yLoc)
        {
            DaddyGrid = grid;
            XLoc = xLoc; 
            YLoc = yLoc;
            DaddyGrid.Columns[XLoc, YLoc] = roomba;
        }
        public void HandleInput(char input)
        {
            switch (input)
            {
                case 'w':
                    // up
                    TraverseGrid(0,-1);
                    break;
                case 'd':
                    // right
                    TraverseGrid(1, 0);
                    break;
                case 's':
                    // down
                    TraverseGrid(0, 1);
                    break;
                case 'a':
                    // left
                    TraverseGrid(-1, 0);
                    break;
            }
        }
        public void TraverseGrid(int xIncr, int yIncr)
        {
            // check if the traverse is possible
            if (DaddyGrid.Columns[YLoc+yIncr,XLoc+xIncr].GetChar() == 'o')
            {
               
                // if yes, move the Roomba.
                DaddyGrid.Columns[YLoc + yIncr, XLoc + xIncr] = roomba;
                // clean the tile
                DaddyGrid.Columns[YLoc, XLoc] = new GridObject("Clean", false);
                // update the internal Roomba location
                YLoc = YLoc + yIncr;
                XLoc = XLoc + xIncr;
            } 
            else
            {
                // if no, do nothing
            }
        }
    }
}
