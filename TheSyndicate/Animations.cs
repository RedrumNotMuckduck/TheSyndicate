using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

namespace TheSyndicate
{
    public class Animations
    {
        private int WindowSize = 200;
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

        public void DisplayIntroScene()
        {
            DisplayGameTitle();
            DisplayGameMessage(); 
            for (int cursorLeft = 0; cursorLeft < this.WindowSize; cursorLeft+=5)
            {
                DisplayRobot(cursorLeft);
                Thread.Sleep(200);
                ClearMovement();
            }
        }

        public void DisplayDeadEnding()
        {
            player.SetBatteryToFullPower();
            for (int i = 0; i < player.BatteryPower; i++)
            {
                player.UpdateBatteryImage();
                Thread.Sleep(300);
                player.DecrementBatteryPowerByOne();
            }
        }

        public void DisplayLoveYourselfEnding()
        {
            for (int cursorLeft = 0; cursorLeft < this.WindowSize; cursorLeft+=5)
            {
                //The heart is 11px wide, so we subtract 11 so that the robot turns red when the heart is overlapped
                if (cursorLeft > this.WindowSize / 2 - 11)
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
            Render(this.WindowSize / 2, 15, ",d88b.d88b,", Red);
            Render(this.WindowSize / 2, 16, "88888888888", Red);
            Render(this.WindowSize / 2, 17, "`Y8888888Y'", Red);
            Render(this.WindowSize / 2, 18, "  `Y888Y'", Red);
            Render(this.WindowSize / 2, 19, "    `Y'", Red);
        }

        private void ClearHeart()
        {
            Render(this.WindowSize / 2, 15, "           ");
            Render(this.WindowSize / 2, 16, "           ");
            Render(this.WindowSize / 2, 17, "           ");
            Render(this.WindowSize / 2, 18, "           ");
            Render(this.WindowSize / 2, 19, "           ");
        }

        private void ClearMovement()
        {
            //Iterate through Window size - 5 to avoid out of bound exception
            for (int cursorLeft = 0; cursorLeft < this.WindowSize - 5; cursorLeft+=5)
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

        private void DisplayGameTitle()
        {
            Render(this.WindowSize / 3, 35, "╔╦╗┬ ┬┌─┐  ┌─┐┬ ┬┌┐┌┌┬┐┬┌─┐┌─┐┌┬┐┌─┐");
            Render(this.WindowSize / 3, 36, " ║ ├─┤├┤   └─┐└┬┘│││ ││││  ├─┤ │ ├┤ ");
            Render(this.WindowSize / 3, 37, " ╩ ┴ ┴└─┘  └─┘ ┴ ┘└┘─┴┘┴└─┘┴ ┴ ┴ └─┘");
        }

        private void DisplayGameMessage()
        {
            Render(this.WindowSize / 2, 40, "╔═╗  ╦═╗┌─┐┌┐ ┌─┐┌┬┐┌─┐  ╦  ┌─┐┬  ┬┌─┐  ╔═╗┌┬┐┌─┐┬─┐┬ ┬");
            Render(this.WindowSize / 2, 41, "╠═╣  ╠╦╝│ │├┴┐│ │ │ └─┐  ║  │ │└┐┌┘├┤   ╚═╗ │ │ │├┬┘└┬┘");
            Render(this.WindowSize / 2, 42, "╩ ╩  ╩╚═└─┘└─┘└─┘ ┴ └─┘  ╩═╝└─┘ └┘ └─┘  ╚═╝ ┴ └─┘┴└─ ┴ "); 
        }

        private void DisplayRobot(int cursorLeft, ConsoleColor color = ConsoleColor.White)
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
