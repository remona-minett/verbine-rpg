namespace verbine_rpg
{
    static class Core
    {
        static void Main()
        {
            Startup.Begin();
            var exitId = Menu.Startup();
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
