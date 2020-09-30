using System;
using System.Runtime.InteropServices;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;

namespace verbine_rpg
{
    static class Menu
    {
        public static void Startup()
        {
            for (;;)
            {
                Sleep(200);
                Clear();
                WriteLine("Verbine RPG Main Menu\n---------------------");
                WriteLine("S to start, O for options, Q to quit."); // Add writelines here for menu
                var menuGenerate = ReadKey(true);
                switch (menuGenerate.Key)
                {
                    case ConsoleKey.S: // "Start Game"
                    {
                        WriteLine("Starting...");
                        BeginGame();
                        goto exit;
                    }
                    case ConsoleKey.O: // "Options"
                    {
                        WriteLine("Loading options...");
                        Options();
                        break;
                    }
                    case ConsoleKey.Q: // "Quit"
                    {
                        Clear();
                        WriteLine("Thanks for playing!");
                        Sleep(500);
                        Environment.Exit(0);
                        break;
                    }
                    default: // Any key not defined by the above.
                    {
                        WriteLine("Invalid key, please try another.");
                        break;
                    }
                }
                exit:
                return;
            }
        }

        public static void BeginGame() // Go here if exitId is "start".
        {

            return;
        }

        public static void Options() // Go here if exitId is "option".
        {
            Configuration.VerifyConfig();
            var config = Configuration.LoadConfig(); // Load current saved configuration into memory
            for (;;)
            {
                Sleep(200);
                Clear();
                WriteLine("Options Menu\n-------------");
                WriteLine("DEBUG, Q to go back."); // Add writelines here for menu
                var menuGenerate = ReadKey(true);
                switch (menuGenerate.Key)
                {
                    default:
                        WriteLine("Invalid key, please try another.");
                        break;
                }
            }
            exit:
            return;
        }
    }
}
