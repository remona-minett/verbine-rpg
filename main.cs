namespace verbine_rpg
{
    static class Core
    {
        static void Main()
        {
            Startup.Begin(); // Basic information and short pause.
            Menus.Startup(); // Displays main menu and it's options.
            Configuration.ListCharacters(); // debugging
        }
    }
}
