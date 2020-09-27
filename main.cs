using System;

namespace verbine_rpg
{
    static class Core
    {
        static void Main()
        {
            var exitId = "not set";
            Startup.Begin();
            exitId = Menu.Startup();
            switch (exitId)
            {
                case "start":
                    Menu.BeginGame();
                    break;
                case "option":
                    Menu.Options();
                    break;
            }
        }
    }
}
