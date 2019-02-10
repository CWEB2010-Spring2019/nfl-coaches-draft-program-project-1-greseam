using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace project1
{
    class Display
    {
        private double budget = 95000000;
        
        List<Player> playerList = new List<Player>();
        List<string> positionsList = new List<string>();
        List<string> placementList = new List<string>();
        List<int> usedOptions = new List<int>();
        private int End = 5;
        private int runOnce = 0;
        private double newBudget = 0;
        private bool canCancel = true;

        int availableCheck = 0;
        const int startX = 17;
        const int startY = 3;
        const int startY2 = 0;
        const int startY3 = 1;
        const int optionsPerLine = 5;
        const int spacingPerLine = 20;
        const int spacingByLine = 3;

        int currentSelection = 0;

        ConsoleKey key;

        
        public Display()
        {
            playerList = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(@"playerDataFile.dat"));
        }
        public void run()
        {
            while (End >= 0 && canCancel == true)
            {
                DisplayTable();
                End--;
                if (End == 0)
                {
                    runAgain();
                }
            }
        }

        public void DisplayTable()
        {
            Console.Clear();
            do
            {
               

                for (int i = 0; i < playerList.Count; i++)
                {
                    //Titles(2, 17, playerList[i].pick);
                    Console.SetCursorPosition(startX + (i % optionsPerLine) * spacingPerLine,
                        startY + (i / optionsPerLine) * spacingByLine);
                    try
                    {
                        if (i == currentSelection)
                        {
                            availableCheck = 0;
                            Console.ForegroundColor = ConsoleColor.Green;
                            availableCheck++;
                        }
                        foreach (int j in usedOptions)
                        {
                            if (i == currentSelection)
                            {
                                if (currentSelection == j)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    availableCheck--;
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (i == currentSelection)
                            Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(playerList[i].name);
                    extraColums(startY2, playerList[currentSelection].school);
                    extraColums(startY3, playerList[currentSelection].price.ToString("C"));
                    //extraColums();
                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            if (currentSelection % optionsPerLine > 0)
                                currentSelection--;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            if (currentSelection % optionsPerLine < optionsPerLine - 1)
                                currentSelection++;
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (currentSelection >= optionsPerLine)
                                currentSelection -= optionsPerLine;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (currentSelection + optionsPerLine < playerList.Count)
                                currentSelection += optionsPerLine;
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            checkESC();
                            break;
                        }
                }
            } while (key != ConsoleKey.Enter || availableCheck == 0);

            runOnce = 0;
            while (runOnce == 0)
            {
                Console.Clear();
                Console.WriteLine("\n\n\t\tYou have selected " + playerList[currentSelection].name + ", has a salary of: " +
                                  playerList[currentSelection].price.ToString("C")+" from "+playerList[currentSelection].school);
                budget = budget - playerList[currentSelection].price;
                newBudget = budget;
                Console.WriteLine("\nYour remaining budget is " + (newBudget).ToString("C") + " and you can pick " +
                                  (End - 1) + " more players.");
                if (newBudget >= 65000000 && End <= 2)
                {
                    Console.WriteLine("\nGreat job your selection is cost effective");
                }

                Console.WriteLine("\nHit Escape to exit the program.\nHit any other key to coninue.");
                usedOptions.Add(currentSelection);
                ConsoleKey Input = Console.ReadKey().Key;
                if (Input == ConsoleKey.Escape)
                {
                    checkESC();
                }
                else
                {
                    runOnce--;
                }
            }
        }

        public void Welcome()
        {
            Console.CursorVisible = false;
            Console.WriteLine("Hello Welcome to the 2019 NFL Draft!!!\nTo start the draft hit a key that is not ESC.\nTo exit simply hit the 'ESC' key\n\n");
            ConsoleKey Input = Console.ReadKey().Key;
            if (Input == ConsoleKey.Escape)
            {
                Console.Clear();
                End = -1;
                Console.WriteLine("Thank you for using this program, have a nice day.\nPlease come back at your earliest convenience to comlete your draft.\n");
            }
            else
            {
                End = 5;
                Console.Clear();
            }
        }

        public Player GetPlayer(int i, int j)
        {
            var somVale = new Player();
            managePositions();
            foreach (Player player in playerList)
            {
                if (player.position == positionsList[j])
                {
                    //if (player.pick == pi)
                   // {
                       // somVale = player;
                   // }

                }
                
            }

            return somVale;

        }

        public void managePositions()
        {
            Player player = new Player();
            for (int i = 0; i < 5; i++)
            {
                positionsList.Add(player.position);
            }
            
        }

        public void runAgain()
        {
            runOnce = 1;
            while (runOnce == 1)
            {

                Console.Clear();
                Console.WriteLine("You can not pick any more players\n");
                //implement a efficent choice msg
                Console.WriteLine("\nWould you like to draft again, but with different players? (Y/N)");
                ConsoleKey tryAgain = Console.ReadKey().Key;
                if (tryAgain == ConsoleKey.N)
                {
                    Console.Clear();
                    Console.WriteLine("Thank You for using the 2019 NFL draft.\nHave a nice day");
                    End = -1;
                    runOnce--;
                    break;
                }
                else if (tryAgain == ConsoleKey.Y)
                {
                    usedOptions.Clear();
                    Console.Clear();
                    End = 5;
                    budget = 95000000;
                    newBudget = 0;
                    runOnce--;
                    break;
                }
                else
                {

                }
            }
            
        }

        public void checkESC()
        {
            runOnce = 1;
            while (runOnce == 1)
            {
                Console.Clear();
                Console.WriteLine("\nyou hit the Esc key, are you sure you would like to leave? (Y/N)");
                ConsoleKey tryAgain = Console.ReadKey().Key;
                if (tryAgain == ConsoleKey.N)
                {
                    Console.Clear();
                    runOnce--;
                }
                else if (tryAgain == ConsoleKey.Y)
                {
                    Console.Clear();
                    Console.WriteLine("Thank You for using the 2019 NFL draft.\nHave a nice day");
                    End = -1;
                    canCancel = false;
                    runOnce--;
                }
                else
                {
                }
            }
        }

        private void extraColums(int Y, string varPlayer)
        {
            
                for (int i = 0; i < playerList.Count; i++)
                {
                    Console.SetCursorPosition(1 , Y );
                    Console.Write(varPlayer);
                    Console.ResetColor();
                }
            
        }
        private void Titles(int Y,int X, string varPlayer)
        {
           
            for (int i = 0; i < playerList.Count; i++)
            {
                    Console.SetCursorPosition(X + (i % 5) * spacingPerLine,Y );
                    Console.Write(varPlayer);
                    Console.ResetColor();

            }

        }
       
    }


}
