using TheSyndicate.Actions;
using System; 

namespace TheSyndicate
{
    class Program
    {
        static void Main(string[] args)
        {

            //GameEngine gameEngine = new GameEngine();
            //gameEngine.Start();
            Console.SetWindowSize(200, 60);

            Board board = new Board();
            board.GenerateGameBoard(); 
        }
    }
}
