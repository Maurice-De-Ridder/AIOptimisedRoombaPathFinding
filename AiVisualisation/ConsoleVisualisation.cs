using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiVisualisation
{
    public class ConsoleVisualisation
    {
        private Grid DaddyGrid;
        public void start()
        {

            CreateConsole();
            CreateObjects();
            StartGame();
        }

        public void CreateConsole()
        {
            // create console
            DaddyGrid = new Grid();
            // mijn kamer dimensies
            DaddyGrid.InstantiateGrid(15, 15);
        }

        public void CreateObjects()
        {
            // create objects
            ObjectGod og = new ObjectGod(DaddyGrid);
            og.InsertObject("Plant", 2, 2);
            og.InsertObject("Plant", 2, 2);
            og.InsertObject("Sofa", 6, 2);
        }

        public void StartGame()
        {
            DaddyGrid.VisualizeGrid();
        }
    }
}
