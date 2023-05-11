using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AiVisualisation
{
    public class Roomba
    {
        public int XLoc;
        public int YLoc;
        private Grid DaddyGrid;
        public GridObject roomba = new GridObject("Roomba", false);
        public Roomba(Grid grid)
        {
            DaddyGrid = grid;
            (YLoc, XLoc) = DaddyGrid.FindBase();
          
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
     
            Regex rx = new Regex(@"C|c|O|o");
            // check if the traverse is possible
            if (rx.IsMatch(DaddyGrid.Columns[YLoc + yIncr, XLoc + xIncr].GetChar().ToString()))
            {
               
                // if yes, move the Roomba.
                DaddyGrid.Columns[YLoc + yIncr, XLoc + xIncr] = roomba;
                
                //Checks if starting tile
                if (DaddyGrid.Columns[YLoc,XLoc].GetChar() != 'B')
                {
                    // clean the tile
                    DaddyGrid.Columns[YLoc, XLoc] = new GridObject("Clean", false);
                }

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
