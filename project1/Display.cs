using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace project1
{
    class Display
    {
        private bool draftTF = true;
        List<Player> playerList = new List<Player>();
        public Display()
        {
            playerList = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(@"playerDataFile.dat"));
        }
        public void run()
        {
            while (draftTF == true)
            {
                DisplayTable();
            }
        }

        public void DisplayTable()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                       // getPlayer(i, j).name;
                       // getPlayer(i,j).price;
                }
            }
        }

        public void Welcome()
        {

        }

        public void getPlayer(int i, int j)
        {
            foreach (Player player in playerList)
            {
                /*
                if (player.position == playerList[i])
                {
                    if (player.pick== Convert.ToString(j))
                    {
                        return player;
                    }
                }
                */
            }
        }
    }

}
