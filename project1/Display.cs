using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace project1
{
    class Display
    {
        List<Player> playerList = new List<Player>();
        List<string> positionsList = new List<string>();
        private int End = 5;
        
        public Display()
        {
            playerList = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(@"playerDataFile.dat"));
        }
        public void run()
        {
            while (End >= 0)
            {
                DisplayTable();

                if (End == 0)
                {
                    runAgain();
                }
            }
        }

        public void DisplayTable()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.WriteLine(GetPlayer(i, j).name);
                    Console.WriteLine(GetPlayer(i, j).school);
                    Console.WriteLine(GetPlayer(i, j).price);
                }
            }
        }

        public void Welcome()
        {
            Console.WriteLine("Hello Welcome to the 2019 NFL Draft!!!\nTo start the draft hit a key that is not X.\nTo exit simply hit the 'X' key\n\n");
            ConsoleKey Input = Console.ReadKey().Key;
            if (Input == ConsoleKey.X)
            {
                Console.Clear();
                End = -1;
                Console.WriteLine("Thank you for using this program, have a nice day.\nPlease come back later to comlete your draft.");
            }
            else
            {
                End = 5;
                Console.Clear();
            }
        }

        public Player GetPlayer(int i, int j)
        {
            foreach (Player player in playerList)
            {
                managePositions();
                if (player.position == positionsList[j] )
                {
                    if (player.pick== i)
                    {
                        return player;
                    }
                }
                
            }
        }

        public void managePositions()
        {
            positionsList.Clear();
            Player player = new Player();
            for (int i = 0; i < 5; i++)
            {
                positionsList.Add(player.position);
            }
            
        }

        public void runAgain()
        {

        }
    }

}
