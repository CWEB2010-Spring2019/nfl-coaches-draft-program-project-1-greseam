using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace project1
{
    class Display
    {
        private int testVal = 0;
        private double budget = 95000000;
        private double totalPrice = 0;
        
        List<Player> playerList = new List<Player>();
        List<int> usedOptions = new List<int>();
        List<int> verifyInts = new List<int>();
        private int End = 5;
        private int runOnce = 0;
        private double newBudget = 0;

        int availableCheck = 0;
        const int startX = 19;
        const int startY = 3;
        const int startY2 = 1;
        const int startY3 = 2;
        const int optionsPerLine = 5;
        const int spacingPerLine = 20;
        const int spacingByLine = 2;

        int currentSelection = 0;

        ConsoleKey key;

        
        public Display()
        {
            playerList = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(@"playerDataFile.dat")); //uses a JSON file to read in data
        }
        public void run()
        {
            while (End >= 0 )
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
            TitlesY();
            TitlesX();
            do
            {
               //the Do While was
                //inspired by someones code i found online
                for (int i = 0; i < playerList.Count; i++)
                {
                    Console.SetCursorPosition(startX + (i % optionsPerLine) * spacingPerLine,
                        startY + (i / optionsPerLine) * spacingByLine); //console position change, allows to manipulate where text is displayed
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
                  
                    Console.ResetColor();
                }
                extraColums(startY2, playerList[currentSelection].school);
                extraColums(startY3, playerList[currentSelection].price.ToString("C"));
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
            } while ((key != ConsoleKey.Enter || availableCheck == 0) && End >= 0);

            runOnce = 0;
            while (runOnce == 0 && End >=0)
            {
                Console.Clear();
                totalPrice = totalPrice + playerList[currentSelection].price;
                budget = budget - playerList[currentSelection].price;
                newBudget = budget;
                if (newBudget <= 0)
                {
                    budgetCheck();
                    runOnce--;
                }

                if (runOnce == 0)
                {
                    verifyInts.Add(currentSelection);
                    usedOptions.Add(currentSelection);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n\n\t\tYou have selected " + playerList[currentSelection].name +
                                      ", has a salary of: " +
                                      playerList[currentSelection].price.ToString("C") + " from " +
                                      playerList[currentSelection].school);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n\t\tYour remaining budget is " + (newBudget).ToString("C") +
                                      " and you can pick " +
                                      (End - 1) + " more players.");
                    Console.ResetColor();
                    Console.WriteLine(
                        "\n\nPress 'X' to verify selection\nHit any other key to coninue.\nHit Escape to exit the program.");

                    ConsoleKey Input = Console.ReadKey().Key;
                    if (Input == ConsoleKey.Escape)
                    {
                        checkESC();
                        runOnce--;
                    }
                    else if (Input == ConsoleKey.X)
                    {
                        verifySelcetion();
                        runOnce--;
                    }
                    else
                    {
                        runOnce--;
                    }
                }
            }
        }

        public void Welcome()
        {
            Console.CursorVisible = false;
            Console.WriteLine("Hello Welcome to the 2019 NFL Draft!!!\nTo select a player use your keyboards Arrow buttons and Enter.\n\tYour starting budget is 95 million dollars.\n\nTo start the draft hit a key that is not ESC.\nTo exit simply hit the 'ESC' key\n\n");
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

        public void runAgain()
        {
            runOnce = 1;
            while (runOnce == 1)
            {
                if (End == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Clear();
                    Console.WriteLine("You can not pick any more players\n");
                    Console.ResetColor();
                    verifySelcetion();
                }
                
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
                    verifyInts.Clear();
                    Console.Clear();
                    End = 5;
                    budget = 95000000;
                    newBudget = 0;
                    totalPrice = 0;
                    runOnce--;
                    break;
                }
                else
                {
                    Console.Clear();
                    verifySelcetion();
                }
            }
            
        }

        public void checkESC()
        {
            runOnce = 1;
            while (runOnce == 1)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou hit the Esc key, are you sure you would like to leave? (Y/N)");
                Console.ResetColor();
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
                    runOnce--;
                }
                else
                {
                }
            }
        }

        private void extraColums(int Y,string var)
        {
            
                for (int i = 0; i < playerList.Count; i++)
                {
                    Console.SetCursorPosition(1 , Y);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(var);
                    Console.ResetColor();
                    Console.ResetColor();
                }
            
        }
        private void TitlesY()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < playerList.Count; i++)
            {
                Console.SetCursorPosition(1 , 3 + (i / optionsPerLine) * spacingByLine);
                Console.WriteLine(playerList[0+((i/5)%8)].position);
                testVal++;
            }
            Console.ResetColor();
        }

        private void TitlesX()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < playerList.Count; i++)
            {
                Console.SetCursorPosition(19 + (i % 5) * spacingPerLine, 1);
                if (playerList[0 + i % 5 * 8].pick == "1")
                {
                    Console.WriteLine("Best");
                }
                else if (playerList[0 + i % 5 * 8].pick == "2")
                {
                    Console.WriteLine("Second");
                }
                else if (playerList[0 + i % 5 * 8].pick == "3")
                {
                    Console.WriteLine("Third");
                }
                else if (playerList[0 + i % 5 * 8].pick == "4")
                {
                    Console.WriteLine("Fourth");
                }
                else if (playerList[0 + i % 5 * 8].pick == "5")
                {
                    Console.WriteLine("Fifth");
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }
            Console.ResetColor();
        }
        

        private void budgetCheck()
        {
            runOnce = 1;
            while (runOnce == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                Console.WriteLine("\nThe Player you tryed to select exceeded your budget!\nWould you like to try again? (Y/N)");
                Console.ResetColor();
                ConsoleKey tryAgain = Console.ReadKey().Key;
                if (tryAgain == ConsoleKey.N)
                {
                    Console.Clear();
                    Console.WriteLine("Thank You for using the 2019 NFL draft.\nHave a nice day");
                    End = -1;
                    runOnce--;
                }
                else if (tryAgain == ConsoleKey.Y)
                {
                    Console.Clear();
                    usedOptions.Clear();
                    verifyInts.Clear();
                    budget = 95000000;
                    newBudget = 0;
                    totalPrice = 0;
                    End = 5;
                    runOnce--;
                }
                else
                {
                }
            }
        }
        void verifySelcetion()
        {
            End = -1;
            Console.Clear();
            Console.WriteLine("Here are your Selected players for your draft");
            if (newBudget >= (95000000 - 65000000) && (playerList[currentSelection].pick == "1" || playerList[currentSelection].pick == "2" || playerList[currentSelection].pick == "3"))
            {
                Console.WriteLine("\nGreat job your selection is cost effective");
            }
            for (int i = 0; i < verifyInts.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                 Console.SetCursorPosition(startX + (i % verifyInts.Count) * spacingPerLine,
                startY );
                 Console.WriteLine(playerList[verifyInts[i]].name);
                 Console.SetCursorPosition(startX + (i % verifyInts.Count) * spacingPerLine,
                     startY+1);
                 Console.WriteLine(playerList[verifyInts[i]].school);
                 Console.SetCursorPosition(startX + (i % verifyInts.Count) * spacingPerLine,
                     startY+2);
                 Console.WriteLine(playerList[verifyInts[i]].price.ToString("C"));
                 Console.ResetColor();
                 Console.ForegroundColor = ConsoleColor.Yellow;
                 Console.WriteLine("\n\nYour Remaining budget is " + newBudget.ToString("C"));
                 Console.WriteLine("Your Draft total cost is " + totalPrice.ToString("C"));
                 Console.ResetColor();
                 
            }
            runAgain();
            
        }

    }


}
