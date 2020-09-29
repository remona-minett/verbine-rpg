using System;
using static System.Console;
using System.IO;
using static System.IO.Directory;
using static System.Threading.Thread;

namespace verbine_rpg
{
    static class Configuration
    {
        public static string GetAppdataFolder() // Finds the application data folder, and if missing, creates application folder.
        {
            var adfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var vbfolder = Path.Combine(adfolder, "verbine-rpg");
            if (Exists(vbfolder)) return vbfolder; // Early termination if true
            SetCurrentDirectory(adfolder);
            try { CreateDirectory("verbine-rpg"); }
            catch (Exception) { WriteLine("Something went wrong. Check your Appdata folder access and try again."); }
            SetCurrentDirectory(vbfolder);
            return vbfolder;
        }
        public static string[] LoadConfig() // Loads config or failing that verifies it exists.
        {
            var vbfolder = GetAppdataFolder();
            SetCurrentDirectory(vbfolder);
            VerifyConfig();
            var config = File.Exists("gameConfiguration.ini") ? File.ReadAllLines("gameConfiguration.ini") : DefaultConfig();
            return config;
        }

        public static bool SaveConfig(string[] conftosave) // Saves the configuration file with current settings. Does not toast if successful, however returns bool that can be acted upon. Already contains error message code for any failure.
        {
            var saved = false;
            var vbfolder = GetAppdataFolder();
            SetCurrentDirectory(vbfolder);
            try { if (File.Exists(("gameConfiguration.ini"))) { File.WriteAllLines("gameConfiguration.ini", conftosave); saved = true; }}
            catch (Exception) { saved = false; }
            if (saved) return true;
            try { File.WriteAllLines("gameConfiguration.ini", conftosave); }
            catch (Exception) { WriteLine("Something went wrong. Check your Appdata folder access and try again."); Sleep(1000); return false; }
            return true;
        }

        public static void VerifyConfig() // Verifies the settings file exists, if not, invokes creation logic. TODO: Add bad value checks
        {
            var vbfolder = GetAppdataFolder();
            SetCurrentDirectory(vbfolder);
            if (File.Exists("gameConfiguration.ini")) { return; }
            DefaultConfig(); // If the file doesn't exist, invoke default settings logic.
        }

        public static string[] DefaultConfig() // Dangerous, use ux warning! Resets settings without confirmation
        {
            var vbfolder = GetAppdataFolder();
            SetCurrentDirectory(vbfolder);
            var config = new string[2];
            config[0] = "Verbine RPG Configuration File. Modifying these values can cause unpredicable behavior. Be sure you know what you're doing before you do it."; // Header
            config[1] = "0"; // unused
            SaveConfig(config);
            return config;
        }

        /* public static string[] CreateCharacter() // Creates character data files, offloading logic to another method.
        {
            var vbfolder = GetAppdataFolder();
            SetCurrentDirectory(vbfolder); // Where to save character data? Documents? Appdata? Working Directory?
        }

        public static string[] LoadCharacter() // Loads selected character data file.
        {
            var vbfolder = GetAppdataFolder();
            SetCurrentDirectory(vbfolder);
        }

        public static string[] SaveCharacter() // Saves or overwrites file of currently active character data.
        {
            var vbfolder = GetAppdataFolder();
            SetCurrentDirectory(vbfolder);
        }

        public static string[] DeleteCharacter() // Dangerous, use ux warning! Unrecoverably deletes character file without confirmation
        {

        } */
    }
}
