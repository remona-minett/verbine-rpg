using System.IO;
using static System.IO.Directory;

namespace verbine_rpg
{
    static class Configuration
    {
        public static string[] Load()
        {
            string[] config = {"no data"};
            var wd = GetCurrentDirectory(); SetCurrentDirectory(wd);
            if (File.Exists("gameConfiguration.ini")) config = File.ReadAllLines("gameConfiguration.ini");
            return config;
        }

        public static string[] Save()
        {
            string[] config = {"no data"};
            return config;
        }

        public static string[] CreateData() // Creates or destroys and recreates the settings file.
        {
            string[] config = {"no data"};
            config[0] = "Verbine Configuration File";
            config[1] = 
        }
    }
}
