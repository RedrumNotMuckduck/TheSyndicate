using System;
using System.Threading;

namespace TheSyndicate
{
    public class Animations
    {
        private static int WindowSize = 180;
        private ConsoleColor Red = ConsoleColor.Red;
        private Player player = Player.GetInstance();

        private static void Render(int cursorLeft, int cursorTop, string content, ConsoleColor displayColor = ConsoleColor.White)
        {
            Console.ForegroundColor = displayColor;
            Console.CursorLeft = cursorLeft;
            Console.CursorTop = cursorTop;
            Console.Write(content);
            Console.ResetColor();
        }

        public static void DisplayIntroScene()
        {
            DisplayGameTitle();
            DisplayGameMessage(); 
            for (int cursorLeft = 0; cursorLeft < WindowSize; cursorLeft+=5)
            {
                DisplayRobot(cursorLeft);
                Thread.Sleep(400);
                ClearMovement();
            }
        }

        public void DisplayDeadEnding()
        {
            player.SetBatteryToFullPower();
            int MAX_BATTERY_POWER = 5;
            for (int i = 0; i < MAX_BATTERY_POWER; i++)
            {
                player.UpdateBatteryImage();
                Thread.Sleep(400);
                player.DecrementBatteryPowerByOne();
            }
        }

        public void DisplayLoveYourselfEnding()
        {
            for (int cursorLeft = 0; cursorLeft < WindowSize; cursorLeft+=5)
            {
                //The heart is 11px wide, so we subtract 11 so that the robot turns red when the heart is overlapped
                if (cursorLeft > WindowSize / 2 - 11)
                {
                    ClearHeart();
                    DisplayRobot(cursorLeft, Red);
                    break;
                }
                else
                {
                    DisplayHeart();
                    DisplayRobot(cursorLeft);
                    Thread.Sleep(400);
                    ClearMovement();
                }
            }
        }

        private void DisplayHeart()
        {
            Render(WindowSize / 2, 15, ",d88b.d88b,", Red);
            Render(WindowSize / 2, 16, "88888888888", Red);
            Render(WindowSize / 2, 17, "`Y8888888Y'", Red);
            Render(WindowSize / 2, 18, "  `Y888Y'", Red);
            Render(WindowSize / 2, 19, "    `Y'", Red);
        }

        private void ClearHeart()
        {
            Render(WindowSize / 2, 15, "           ");
            Render(WindowSize / 2, 16, "           ");
            Render(WindowSize / 2, 17, "           ");
            Render(WindowSize / 2, 18, "           ");
            Render(WindowSize / 2, 19, "           ");
        }

        private static void ClearMovement()
        {
            //Iterate through Window size - 5 to avoid out of bound exception
            for (int cursorLeft = 0; cursorLeft < WindowSize - 5; cursorLeft+=5)
            {
                Render(cursorLeft, 10, "      ");
                Render(cursorLeft, 11, "      ");
                Render(cursorLeft, 12, "      ");
                Render(cursorLeft, 13, "      ");
                Render(cursorLeft, 14, "      ");
                Render(cursorLeft, 15, "      ");
                Render(cursorLeft, 16, "      ");
                Render(cursorLeft, 17, "      ");
                Render(cursorLeft, 18, "      ");
                Render(cursorLeft, 19, "      ");
                Render(cursorLeft, 20, "      ");
                Render(cursorLeft, 21, "      ");
                Render(cursorLeft, 22, "      ");
                Render(cursorLeft, 23, "      ");
                Render(cursorLeft, 24, "      ");
                Render(cursorLeft, 25, "      ");
                Render(cursorLeft, 26, "      ");
                Render(cursorLeft, 27, "      ");
                Render(cursorLeft, 28, "      ");
            }
        }

        private static void DisplayGameTitle()
        {
            Render(WindowSize / 3, 35, "╔╦╗┬ ┬┌─┐  ┌─┐┬ ┬┌┐┌┌┬┐┬┌─┐┌─┐┌┬┐┌─┐");
            Render(WindowSize / 3, 36, " ║ ├─┤├┤   └─┐└┬┘│││ ││││  ├─┤ │ ├┤ ");
            Render(WindowSize / 3, 37, " ╩ ┴ ┴└─┘  └─┘ ┴ ┘└┘─┴┘┴└─┘┴ ┴ ┴ └─┘");
        }

        private static void DisplayGameMessage()
        {
            Render(WindowSize / 2, 40, "╔═╗  ╦═╗┌─┐┌┐ ┌─┐┌┬┐┌─┐  ╦  ┌─┐┬  ┬┌─┐  ╔═╗┌┬┐┌─┐┬─┐┬ ┬");
            Render(WindowSize / 2, 41, "╠═╣  ╠╦╝│ │├┴┐│ │ │ └─┐  ║  │ │└┐┌┘├┤   ╚═╗ │ │ │├┬┘└┬┘");
            Render(WindowSize / 2, 42, "╩ ╩  ╩╚═└─┘└─┘└─┘ ┴ └─┘  ╩═╝└─┘ └┘ └─┘  ╚═╝ ┴ └─┘┴└─ ┴ "); 
        }

        private static void DisplayRobot(int cursorLeft, ConsoleColor color = ConsoleColor.White)
        {
            Render(cursorLeft, 10, "             _____", color);
            Render(cursorLeft, 11, "            /_____\\", color);
            Render(cursorLeft, 12, "       ____[\\`---'/]____", color);
            Render(cursorLeft, 13, "      /\\ #\\ \\_____/ /# /\\", color);
            Render(cursorLeft, 14, "     /  \\# \\_.---._/ #/  \\", color);
            Render(cursorLeft, 15, "    /   /|\\  |   |  /|\\   \\", color);
            Render(cursorLeft, 16, "   /___/ | | |   | | | \\___\\", color);
            Render(cursorLeft, 17, "   |  |  | | |---| | |  |  |", color);
            Render(cursorLeft, 18, "   |__|  \\_| |_#_| |_/  |__|", color);
            Render(cursorLeft, 19, "   //\\\\  <\\ _//^\\\\_ />  //\\\\", color);
            Render(cursorLeft, 20, "   \\||/  |\\//// \\\\\\\\/|  \\||/", color);
            Render(cursorLeft, 21, "         |   |   |   |", color);
            Render(cursorLeft, 22, "         |---|   |---|", color);
            Render(cursorLeft, 23, "         |---|   |---|", color);
            Render(cursorLeft, 24, "         |   |   |   |", color);
            Render(cursorLeft, 25, "         |___|   |___|", color);
            Render(cursorLeft, 26, "         /   \\   /   \\", color);
            Render(cursorLeft, 27, "        |_____| |_____|", color);
            Render(cursorLeft, 28, "        |HHHHH| |HHHHH|", color);
        }
    }
}
