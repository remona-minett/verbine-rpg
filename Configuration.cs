using System;
using System.Collections.Generic;
using System.Globalization;
using static System.Console;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
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
            catch (Exception) { WriteLine("Something went wrong. Check your Appdata folder access and try again."); return null; }
            SetCurrentDirectory(vbfolder);
            return vbfolder;
        }

        public static string GetChardataFolder()
        {
            var vbfolder = GetAppdataFolder();
            SetCurrentDirectory(vbfolder);
            var cdfolder = Path.Combine(vbfolder, "saves");
            if (Exists(cdfolder)) return cdfolder; // Early termination if true
            try { CreateDirectory("saves"); }
            catch (Exception) { WriteLine("Something went wrong. Check your Appdata folder access and try again."); return null; }
            SetCurrentDirectory(cdfolder);
            return cdfolder;
        }

        public static string[] LoadConfig() // Loads config or failing that verifies it exists. You should call VerifyConfig() before this if you need more data to be returned than just the string[].
        {
            var vbfolder = GetAppdataFolder();
            SetCurrentDirectory(vbfolder);
            VerifyConfig();
            var config = File.Exists("gameConfiguration.ini") ? File.ReadAllLines("gameConfiguration.ini") : DefaultConfig();
            return config;
        }

        public static void SaveConfig(string[] conftosave) // Saves the configuration file with current settings. Does not toast if successful, however returns bool that can be acted upon. Already contains error message code for any failure.
        {
            var saved = false;
            var vbfolder = GetAppdataFolder();
            SetCurrentDirectory(vbfolder);
            try { if (File.Exists("gameConfiguration.ini")) { File.WriteAllLines("gameConfiguration.ini", conftosave); saved = true; }}
            catch (Exception) { saved = false; }
            if (saved) return;
            try { File.WriteAllLines("gameConfiguration.ini", conftosave); }
            catch (Exception) { WriteLine("Something went wrong. Check your Appdata folder access and try again."); Sleep(1000); }
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

        /* public static string[] SaveNewCharacter() // Creates character data files, offloading logic to another method.
        {
            var vbfolder = GetAppdataFolder();
            SetCurrentDirectory(vbfolder);
        } */

        public static string[] LoadCharacter(string charName) // Loads selected character data file.
        {
            var cdfolder = GetChardataFolder();
            SetCurrentDirectory(cdfolder);
            var charStats = new string[10];
            charStats = VerifyChar(charName);
            return charStats;
        }

        public static void SaveCharacter(string[] chartosave) // Saves or overwrites file of currently active character data. Does not confirm, use UX if needed.
        {
            var saved = false;
            var cdfolder = GetChardataFolder();
            SetCurrentDirectory(cdfolder);
            var charpath = Path.Combine(chartosave[0] + "char.ini");
            try { if (File.Exists(charpath)) { File.WriteAllLines(charpath, chartosave); saved = true; }}
            catch (Exception) { saved = false; }
            if (saved) return;
            try { File.WriteAllLines(charpath, chartosave); }
            catch (Exception) { WriteLine("Something went wrong. Check your Appdata folder access and try again."); Sleep(1000); }
        }

        public static void DeleteCharacter() // Dangerous, use ux warning! Unrecoverably deletes character file without confirmation
        {

        }

        static string[] VerifyChar(string charName) // Verifies the character file exists, if not, invokes creation logic. TODO: Add bad value checks.
        {
            var cdfolder = GetChardataFolder();
            SetCurrentDirectory(cdfolder);
            var charStats = new string[10];
            var charpath = Path.Combine(charName + "char.ini"); // e.g., "stevechar.ini"
            if (File.Exists(charpath)) { charStats = File.ReadAllLines(charpath); return charStats; } // If the file exists, then read it into an array and give it back to calling logic.
            charStats = NewCharSetup(charName); // If character file doesn't exist, create it with the entered name.
            return charStats; // Then give it back to calling logic.
        }

        static string[] NewCharSetup(string CharName) // Basic class. Only one available currently. Should create specific strengths and weaknesses in other classes when the option is created.
        {
            var charStats = new string[10];
            charStats[0] = CharName; // Name
            charStats[1] = "100"; // Current Health
            charStats[2] = "100"; // Maximum Health
            charStats[3] = "100"; // Current Spirit (magic)
            charStats[4] = "100"; // Maximum Spirit (magic)
            charStats[3] = "0"; // Strength (Attack damage is multiplied by X div 10 %)
            charStats[4] = "5"; // Defence (Incoming attack damage is reduced by this value (X div 10 %)
            charStats[5] = "0"; // Dexterity (Dodge change is multiplied by X div 10 %)
            charStats[6] = "0"; // Intelligence (Magic damage is multiplied by X div 10 %)
            charStats[7] = "0"; // Vitality (Health is multiplied by X div 10 %, additionally X div 5 % to resist On The Brink (<=0 hp))
            charStats[8] = "10"; // Critical Hit Chance (X%)
            charStats[9] = "0"; // Kills tracker
            SaveCharacter(charStats);
            return charStats;
        }

        public static string [] ListCharacters()
        {
            var cdfolder = GetChardataFolder();
            SetCurrentDirectory(cdfolder);
            var charList = new List<string>();
            foreach (var file in Directory.EnumerateFiles(cdfolder, "*char.ini"))
            {
                var name = File.ReadAllLines(file).Take(1).ToArray();
                charList.Add(name[0]);
            }
            for ( ; ;)
            {
                Clear();
                WriteLine("Please choose a character below, or Q to go back:\n");
                WriteLine(string.Join<string>("\n", charList));
                WriteLine(""); // carriage return at the end of character listing
                var selection = ReadLine().ToLower();
                if (selection == "q") return null; // "If x == null, break; ?" As of typing the caller for ListCharacters has not been created and this exits code 0.
                if (charList.Contains(selection, StringComparer.OrdinalIgnoreCase))
                {
                    var charStats = new string[10];
                    LoadCharacter(selection);
                    return charStats;
                }
                else
                {
                    WriteLine("Invalid input, please try again.");
                    Sleep(200);
                }
            }
        }
    }
}
