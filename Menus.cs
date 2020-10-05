using System;
using System.IO;
using System.Net;
using static System.Console;
using static System.Threading.Thread;

namespace verbine_rpg
{
    static class Menus
    {
        public static void Startup()
        {
            for ( ; ;)
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
                        // BeginGame();
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
            }
            exit:
            return;
        }

        /* public static void BeginGame() // Go here if exitId is "start".
        {

        } */

        public static void Options() // Go here if exitId is "option".
        {
            Configuration.VerifyConfig();
            var config = Configuration.LoadConfig(); // Load current saved configuration into memory
            for (;;)
            {
                Sleep(200);
                Clear();
                WriteLine("Options Menu\n-------------");
                WriteLine("U for Update Check, Q to go back."); // Add writelines here for menu
                var menuGenerate = ReadKey(true);
                switch (menuGenerate.Key)
                {
                    case ConsoleKey.U:
                        string onlver = null;
                        string locver = null;
                        var success = false;
                        Clear();
                        using (var client = new WebClient())
                        {
                            try
                            {
                                onlver = client.DownloadString("https://raw.githubusercontent.com/remona-minett/verbine-rpg/master/ver.txt");
                                success = true;
                            }
                            catch (Exception)
                            {
                                WriteLine("Unable to connect. Try again later!");
                                WriteLine("Press any key to continue.");
                            }
                        }

                        if (success)
                        {
                            WriteLine("Current Version: 0.1.0 Alpha");
                            WriteLine("Available Version: " + onlver);
                            WriteLine("Press any key to continue.");
                            ReadKey(true);
                        }
                        break;
                    case ConsoleKey.Q:
                        goto exit;
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
