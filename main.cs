using System;

namespace verbine_rpg
{
    static class Core
    {
        static void Main()
        {
            Startup.Begin();
            Menu.Startup(); // Make this method exit only through BeginGame(), somehow.
        }
    }
}
