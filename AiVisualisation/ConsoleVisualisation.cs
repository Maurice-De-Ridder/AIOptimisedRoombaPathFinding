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
        private Roomba Roomba;
        public void start()
        {

            CreateConsole();
            CreateRoomba();
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
            
            
            char inputChar = ' ';
            while (inputChar != 'c')
            {
                Console.WriteLine("Welcome to the Roomba game");
                Console.WriteLine("Press W to move up");
                Console.WriteLine("Press S to move down");
                Console.WriteLine("Press D to move to the right");
                Console.WriteLine("Press A to move to the lef");
                Console.WriteLine("Press C to exit");
                DaddyGrid.VisualizeGrid();
                Console.WriteLine("You have cleared " + DaddyGrid.CalculateTileAmount('C') + " tiles out of the " + DaddyGrid.CalculateTileAmount('o') + " dirty tiles" );

                try
                {
                    Roomba.HandleInput(Console.ReadLine().ToCharArray()[0]);
                }
                catch (Exception)
                {
                    Console.WriteLine("Input a valid character!");
                    Roomba.HandleInput(Console.ReadLine().ToCharArray()[0]);
                }

                Console.Clear();
            }
        }

        public void CreateRoomba()
        {
            Roomba = new Roomba(DaddyGrid, 0,0);
        }
    }
}
