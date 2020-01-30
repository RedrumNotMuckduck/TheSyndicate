namespace TheSyndicate
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleWindow.ShowWindow(ConsoleWindow.ThisConsole, ConsoleWindow.MAXIMIZE);
            GameEngine gameEngine = new GameEngine();
            gameEngine.Start();
        }
    }
}
