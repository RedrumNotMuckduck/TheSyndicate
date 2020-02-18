﻿using System;
using System.Threading;

namespace TheSyndicate
{
    public class Animations
    {
        private static int WindowSize = 180;
        private static ConsoleColor Red = ConsoleColor.Red;
        private ConsoleColor Yellow = ConsoleColor.Yellow;
        private ConsoleColor Blue = ConsoleColor.Blue;

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
            for (int cursorLeft = 0; cursorLeft < WindowSize; cursorLeft+=5)
            {
                DisplayRobot(cursorLeft, 10);
                Thread.Sleep(400);
                ClearMovement(10,29);
            }
        }

        public void DisplayUniqueEnding(string sceneID)
        {
            switch (sceneID)
            {
                case "loveOneself":
                    DisplayLoveYourselfEnding();
                    break;
                case "animal":
                    DisplayFoundDogEnding();
                    break;
                case "toaster":
                    DisplayFoundToasterEnding();
                    break;
                default:
                    DisplayDeadEnding();
                    break;
            }
        }

        public void DisplayDeadEnding()
        {
            player.SetBatteryToFullPower();
            int MAX_BATTERY_POWER = 4;
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
                DisplayHeart();
                //The heart is 11px wide, so we subtract 11 so that the robot turns red when the heart is overlapped
                if (cursorLeft > WindowSize / 2 - 11)
                {
                    ClearHeart();
                    DisplayRobot(cursorLeft, 20, Red);
                    break;
                }
                else
                {
                    DisplayRobot(cursorLeft, 20);
                    Thread.Sleep(400);
                    ClearMovement(20, WindowSize / 2);
                }
            }
        }

        private void DisplayHeart()
        {
            Render(WindowSize / 2, 25, ",d88b.d88b,", Red);
            Render(WindowSize / 2, 26, "88888888888", Red);
            Render(WindowSize / 2, 27, "`Y8888888Y'", Red);
            Render(WindowSize / 2, 28, "  `Y888Y'", Red);
            Render(WindowSize / 2, 29, "    `Y'", Red);
        }

        private void ClearHeart()
        {
            Render(WindowSize / 2, 25, "           ");
            Render(WindowSize / 2, 26, "           ");
            Render(WindowSize / 2, 27, "           ");
            Render(WindowSize / 2, 28, "           ");
            Render(WindowSize / 2, 29, "           ");
        }

        public void DisplayFoundDogEnding()
        {
            for (int cursorLeft = 0; cursorLeft < WindowSize; cursorLeft += 5)
            {
                DisplayDog();
                //The dog is 30px wide, so we subtract 30 so that the robot turns yellow when the dog is overlapped
                if (cursorLeft > WindowSize / 2 - 30)
                {
                    ClearDogorToaster();
                    DisplayRobot(cursorLeft, 20, Yellow);
                    break;
                }
                else
                {
                    DisplayRobot(cursorLeft, 20);
                    Thread.Sleep(400);
                    ClearMovement(20, WindowSize / 2 - 30);
                }
            }
        }

        public void DisplayFoundToasterEnding()
        {
            DisplayToaster();
            for (int cursorLeft = 0; cursorLeft < WindowSize; cursorLeft += 5)
            {
                //The toaster is 30px wide, so we subtract 30 so that the robot turns blue when the toaster is overlapped
                if (cursorLeft > WindowSize / 2 - 30)
                {
                    ClearDogorToaster();
                    DisplayRobot(cursorLeft, 20, Blue);
                    break;
                }
                else
                {
                    DisplayRobot(cursorLeft, 20);
                    Thread.Sleep(400);
                    ClearMovement(20, WindowSize / 2 - 30);
                }
            }
        }

        private void DisplayDog()
        {
            Render(WindowSize / 2, 30, "      |\\_ /|", Yellow);
            Render(WindowSize / 2, 31, "      | @ @   Woof!", Yellow);
            Render(WindowSize / 2, 32, "      |   <> _", Yellow);
            Render(WindowSize / 2, 33, "      | _ /\\------____((| |))", Yellow);
            Render(WindowSize / 2, 34, "      |               `--' |", Yellow);
            Render(WindowSize / 2, 35, " ____ | _       ___ |   | ___.'", Yellow);
            Render(WindowSize / 2, 36, "/ _/_____/ ____/ _______|", Yellow);
        }
        private void DisplayToaster()
        {
            Render(WindowSize / 2, 20, ".-(  )) ))-. ", Blue);
            Render(WindowSize / 2, 21, " |\"\"\"\"\"\"\"\"\"\"\\||", Blue);
            Render(WindowSize / 2, 22, " |#          ||", Blue);
            Render(WindowSize / 2, 23, " |#          ||", Blue);
            Render(WindowSize / 2, 24, " |           |;", Blue);
            Render(WindowSize / 2, 25, ".=== '----_------'", Blue);
            Render(WindowSize / 2, 26, "(______.- (_ =", Blue);
        }
        private void ClearDogorToaster()
        {
            Render(WindowSize / 2, 20, "                           ");
            Render(WindowSize / 2, 21, "                           ");
            Render(WindowSize / 2, 22, "                           ");
            Render(WindowSize / 2, 23, "                           ");
            Render(WindowSize / 2, 24, "                           ");
            Render(WindowSize / 2, 25, "                           ");
            Render(WindowSize / 2, 26, "                           ");

        }

        private void ClearMovement(int cursorTopStart, int cursorTopEnd)
        {
            //Iterate through Window size - 5 to avoid out of bound exception
            for (int cursorLeft = 0; cursorLeft < WindowSize - 5; cursorLeft+=5)
            {
                for (int cursorTop = cursorTopStart; cursorTop < cursorTopEnd; cursorTop++)
                {
                    Render(cursorLeft, cursorTop, "      ");
                }
            }
        }

        private void DisplayGameTitle()
        {
            Render(WindowSize / 3, 35, "╔╦╗┬ ┬┌─┐  ┌─┐┬ ┬┌┐┌┌┬┐┬┌─┐┌─┐┌┬┐┌─┐");
            Render(WindowSize / 3, 36, " ║ ├─┤├┤   └─┐└┬┘│││ ││││  ├─┤ │ ├┤ ");
            Render(WindowSize / 3, 37, " ╩ ┴ ┴└─┘  └─┘ ┴ ┘└┘─┴┘┴└─┘┴ ┴ ┴ └─┘");
        }

        private void DisplayGameMessage()
        {
            Render(WindowSize / 2, 40, "╔═╗  ╦═╗┌─┐┌┐ ┌─┐┌┬┐┌─┐  ╦  ┌─┐┬  ┬┌─┐  ╔═╗┌┬┐┌─┐┬─┐┬ ┬");
            Render(WindowSize / 2, 41, "╠═╣  ╠╦╝│ │├┴┐│ │ │ └─┐  ║  │ │└┐┌┘├┤   ╚═╗ │ │ │├┬┘└┬┘");
            Render(WindowSize / 2, 42, "╩ ╩  ╩╚═└─┘└─┘└─┘ ┴ └─┘  ╩═╝└─┘ └┘ └─┘  ╚═╝ ┴ └─┘┴└─ ┴ "); 
        }

        private void DisplayRobot(int cursorLeft, int cursorTop, ConsoleColor color = ConsoleColor.White)
        {
            Render(cursorLeft, cursorTop, "             _____", color);
            Render(cursorLeft, cursorTop + 1, "            /_____\\", color);
            Render(cursorLeft, cursorTop + 2, "       ____[\\`---'/]____", color);
            Render(cursorLeft, cursorTop + 3, "      /\\ #\\ \\_____/ /# /\\", color);
            Render(cursorLeft, cursorTop + 4, "     /  \\# \\_.---._/ #/  \\", color);
            Render(cursorLeft, cursorTop + 5, "    /   /|\\  |   |  /|\\   \\", color);
            Render(cursorLeft, cursorTop + 6, "   /___/ | | |   | | | \\___\\", color);
            Render(cursorLeft, cursorTop + 7, "   |  |  | | |---| | |  |  |", color);
            Render(cursorLeft, cursorTop + 8, "   |__|  \\_| |_#_| |_/  |__|", color);
            Render(cursorLeft, cursorTop + 9, "   //\\\\  <\\ _//^\\\\_ />  //\\\\", color);
            Render(cursorLeft, cursorTop + 10, "   \\||/  |\\//// \\\\\\\\/|  \\||/", color);
            Render(cursorLeft, cursorTop + 11, "         |   |   |   |", color);
            Render(cursorLeft, cursorTop + 12, "         |---|   |---|", color);
            Render(cursorLeft, cursorTop + 13, "         |---|   |---|", color);
            Render(cursorLeft, cursorTop + 14, "         |   |   |   |", color);
            Render(cursorLeft, cursorTop + 15, "         |___|   |___|", color);
            Render(cursorLeft, cursorTop + 16, "         /   \\   /   \\", color);
            Render(cursorLeft, cursorTop + 17, "        |_____| |_____|", color);
            Render(cursorLeft, cursorTop + 18, "        |HHHHH| |HHHHH|", color);
        }
    }
}
