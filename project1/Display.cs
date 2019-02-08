using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace project1
{
    class Display
    {
        List<Player> playerList = new List<Player>();
        public Display()
        {
            playerList = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(@"playerDataFile.dat"));
        }
        public void run()
        {
            foreach (Player player in playerList)
            {
                Console.WriteLine(player.name);
            }
        }
    }

}
