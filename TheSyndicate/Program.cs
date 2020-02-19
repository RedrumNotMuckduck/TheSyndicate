using TheSyndicate.Actions;
using System; 

namespace TheSyndicate
{
    class Program
    {
        static void Main(string[] args)
        {
            while (Scene.NewGame)
            {
                GameEngine gameEngine = new GameEngine();
                gameEngine.Start();
            }
        }
    }
}
