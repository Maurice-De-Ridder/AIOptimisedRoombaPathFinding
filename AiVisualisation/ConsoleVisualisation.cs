using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AiVisualisation
{
    public class ConsoleVisualisation
    {
        private Grid DaddyGrid;
        private Roomba Roomba;
        private ObjectGod og;
        public void start()
        {
            CreateConsole();
            CreateRoomba();
            CreateObjects();
            StartGame();
            ReturnHome();
        }

        public void CreateConsole()
        {
            // create console
            DaddyGrid = new Grid();
            // mijn kamer dimensies
            DaddyGrid.InstantiateGrid(15, 15);
            og = new ObjectGod(DaddyGrid);
        }

        public void CreateObjects()
        {
            // create objects
            og.InsertObject("Plant", 2, 2);
            og.InsertObject("Plant", 2, 2);
            og.InsertObject("Sofa", 6, 2);
        }

        public void StartGame()
        {
            char inputChar = ' ';
            int counter = 0;
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
                Console.WriteLine("You have taken: " + counter + " Steps");

                inputChar = Console.ReadLine().ToCharArray()[0];
                try
                {
                    Roomba.HandleInput(inputChar);
                    counter++;
                }
                catch (Exception)
                {
                    Console.WriteLine("Input a valid character!");
                    Roomba.HandleInput(inputChar);
                }

                Console.Clear();
            }

        }

        public void ReturnHome()
        {
            int EndX, EndY;
            (EndY,EndX) = DaddyGrid.FindBase();
            Console.WriteLine();
            Console.WriteLine("Amount of tiles back to base");
            Console.WriteLine(BreadthFirstSearch.BFS(DaddyGrid,Roomba.XLoc,Roomba.YLoc,EndX,EndY) + " Tiles");
            
            DaddyGrid.VisualizeGrid();
        }

        public void CreateRoomba()
        {
            og.InsertObject("Base",1,1);
            Roomba = new Roomba(DaddyGrid);
        }
    }
}
