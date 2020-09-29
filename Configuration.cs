using static System.Console;
using System.IO;
using static System.IO.Directory;
using static System.Threading.Thread;

namespace verbine_rpg
{
    static class Configuration
    {
        public static string[] LoadConfig()
        {
            string[] config = {"no data"};
            var wd = GetCurrentDirectory(); SetCurrentDirectory(wd);
            if (File.Exists("gameConfiguration.ini")) config = File.ReadAllLines("gameConfiguration.ini");
            return config;
        }

        public static void string[] SaveConfig(string[] conftosave) // Saves the configuration file
        {
            bool saved = false;
            var wd = GetCurrentDirectory(); SetCurrentDirectory(wd);
            if (File.Exists(("gameConfiguration.ini"))) { File.WriteAllLines("gameConfiguration.ini", conftosave); saved = true; }
            else saved = false;
            if (saved) { WriteLine("Successfully saved settings. Returning to menu."); Sleep(500); }
            else { WriteLine("Something went wrong. Check your folder access and try again."); }
        }

        public static string[] CreateConfig() // Creates or destroys and recreates the settings file.
        {
            string[] config = {"no data"};
            config[0] = "Verbine Configuration File"; // Header
            config[1] = "0"; // Unused
            return config;
        }

        public static string[] CreateCharacter() // Creates character data files.
        {
            var wd = GetCurrentDirectory(); SetCurrentDirectory(wd);
        }

        public static string[] LoadCharacter() // Loads selected character data file.
        {
            var wd = GetCurrentDirectory(); SetCurrentDirectory(wd);
        }

        public static string[] SaveCharacter() // Saves currently active character data.
        {
            var wd = GetCurrentDirectory(); SetCurrentDirectory(wd);
        }
    }
}
