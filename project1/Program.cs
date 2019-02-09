using System;
using Newtonsoft.Json;


namespace project1
{
    class Program
    {
        static void Main(string[] args)
        {
            Display display = new Display();
            display.Welcome();
            display.run();
        }
    }

}
