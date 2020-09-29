using System;
using System.Threading;
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
                Sleep(200);
                Clear();
                WriteLine("Verbine RPG Main Menu\n---------------------");
                WriteLine("S to start, Q to quit."); // O for options,
                // Add writelines here for menu
                var menuGenerate = ReadKey(true);
                switch (menuGenerate.Key)
                {
                    case ConsoleKey.S: // "Start Game"
                    {
                        WriteLine("Starting...");
                        BeginGame();
                        break;
                    }
                    case ConsoleKey.O: // "Options"
                    {
                        /* WriteLine("Loading options...");
                        Options(); */
                        goto default; // No options to configure, so disabled. Replace with "break;" when enabling.
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

        public static void BeginGame() // Go here if exitId is "start".
        {
            // Needs a menu
        }

        public static void Options() // Go here if exitId is "option". TODO: Method name change reflection
        {
            Configuration.VerifyConfig(); // Needs a menu
            ReadKey(); // debug feature
        }
    }
}
