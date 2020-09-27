using System;
using static System.Console;
using static System.Threading.Thread;

namespace verbine_rpg
{
    static class Menu
    {
        public static string Startup()
        {
            for (;;)
            {
                Clear();
                string exitId;
                WriteLine("S to start, O for options, Q to quit.");
                // Add writelines here for menu
                var menuGenerate = ReadKey(true);
                switch (menuGenerate.Key)
                {
                    case ConsoleKey.S: // "Start Game"
                    {
                        WriteLine("Starting...");
                        exitId = "start";
                        return exitId;
                    }
                    case ConsoleKey.O: // "Options"
                    {
                        WriteLine("Loading options...");
                        exitId = "option";
                        return exitId;
                    }
                    case ConsoleKey.Q: // "Quit"
                    {
                        Clear();
                        WriteLine("Thanks for playing!");
                        Sleep(500);
                        Environment.Exit(0);
                        break;
                    }
                    default: // Any key not understood by the above.
                    {
                        WriteLine("Invalid key, please try again.");
                        break;
                    }
                }

            }
        }

        public static void BeginGame()
        {

        }

        public static void Options()
        {
            var config = Configuration.Load();
            if (config[0] != "no data")
            {

            }
            else if (config[0] == "no data") config = Configuration.CreateData();
        }
    }
}
