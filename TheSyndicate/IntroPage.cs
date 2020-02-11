using System;
using System.Collections.Generic;
using System.Text;
using System.Threading; 

namespace TheSyndicate
{
    public class IntroPage
    {
        private static void Render(int cursorLeft, int cursorTop, string content, ConsoleColor displayColor)
        {
            Console.ForegroundColor = displayColor;
            Console.CursorLeft = cursorLeft;
            Console.CursorTop = cursorTop;
            Console.Write(content);
            Console.ResetColor();
        }

        public void DisplayIntroScene()
        {
            ConsoleColor RobotColor = ConsoleColor.White;
            for (int i = 0; i < 200; i++)
            {
                Render(i, 10, "             _____", RobotColor);
                Render(i, 11, "            /_____\\", RobotColor);
                Render(i, 12, "       ____[\\`---'/]____", RobotColor);
                Render(i, 13, "      /\\ #\\ \\_____/ /# /\\", RobotColor);
                Render(i, 14, "     /  \\# \\_.---._/ #/  \\", RobotColor);
                Render(i, 15, "    /   /|\\  |   |  /|\\   \\", RobotColor);
                Render(i, 16, "   /___/ | | |   | | | \\___\\", RobotColor);
                Render(i, 17, "   |  |  | | |---| | |  |  |", RobotColor);
                Render(i, 18, "   |__|  \\_| |_#_| |_/  |__|", RobotColor);
                Render(i, 19, "   //\\\\  <\\ _//^\\\\_ />  //\\\\", RobotColor);
                Render(i, 20, "   \\||/  |\\//// \\\\\\\\/|  \\||/", RobotColor);
                Render(i, 21, "         |   |   |   |", RobotColor);
                Render(i, 22, "         |---|   |---|", RobotColor);
                Render(i, 23, "         |---|   |---|", RobotColor);
                Render(i, 24, "         |   |   |   |", RobotColor);
                Render(i, 25, "         |___|   |___|", RobotColor);
                Render(i, 26, "         /   \\   /   \\", RobotColor);
                Render(i, 27, "        |_____| |_____|", RobotColor);
                Render(i, 28, "        |HHHHH| |HHHHH|", RobotColor);

                Thread.Sleep(200);
                ClearMovement();
                i += 5;
            }
        }
        public void DisplayGoodEndingScene()
        {
            ConsoleColor RobotColor = ConsoleColor.White;

            for (int i = 0; i < 200; i++)
            {
                if (i > 200 / 2)
                {
                    ClearHeart();
                    RobotColor = ConsoleColor.Red;
                    Render(i, 10, "             _____", RobotColor);
                    Render(i, 11, "            /_____\\", RobotColor);
                    Render(i, 12, "       ____[\\`---'/]____", RobotColor);
                    Render(i, 13, "      /\\ #\\ \\_____/ /# /\\", RobotColor);
                    Render(i, 14, "     /  \\# \\_.---._/ #/  \\", RobotColor);
                    Render(i, 15, "    /   /|\\  |   |  /|\\   \\", RobotColor);
                    Render(i, 16, "   /___/ | | |   | | | \\___\\", RobotColor);
                    Render(i, 17, "   |  |  | | |---| | |  |  |", RobotColor);
                    Render(i, 18, "   |__|  \\_| |_#_| |_/  |__|", RobotColor);
                    Render(i, 19, "   //\\\\  <\\ _//^\\\\_ />  //\\\\", RobotColor);
                    Render(i, 20, "   \\||/  |\\//// \\\\\\\\/|  \\||/", RobotColor);
                    Render(i, 21, "         |   |   |   |", RobotColor);
                    Render(i, 22, "         |---|   |---|", RobotColor);
                    Render(i, 23, "         |---|   |---|", RobotColor);
                    Render(i, 24, "         |   |   |   |", RobotColor);
                    Render(i, 25, "         |___|   |___|", RobotColor);
                    Render(i, 26, "         /   \\   /   \\", RobotColor);
                    Render(i, 27, "        |_____| |_____|", RobotColor);
                    Render(i, 28, "        |HHHHH| |HHHHH|", RobotColor);

                    Thread.Sleep(500);
                    ClearMovement();
                }
                else
                {
                    DisplayHeart();
                    Render(i, 10, "             _____", RobotColor);
                    Render(i, 11, "            /_____\\", RobotColor);
                    Render(i, 12, "       ____[\\`---'/]____", RobotColor);
                    Render(i, 13, "      /\\ #\\ \\_____/ /# /\\", RobotColor);
                    Render(i, 14, "     /  \\# \\_.---._/ #/  \\", RobotColor);
                    Render(i, 15, "    /   /|\\  |   |  /|\\   \\", RobotColor);
                    Render(i, 16, "   /___/ | | |   | | | \\___\\", RobotColor);
                    Render(i, 17, "   |  |  | | |---| | |  |  |", RobotColor);
                    Render(i, 18, "   |__|  \\_| |_#_| |_/  |__|", RobotColor);
                    Render(i, 19, "   //\\\\  <\\ _//^\\\\_ />  //\\\\", RobotColor);
                    Render(i, 20, "   \\||/  |\\//// \\\\\\\\/|  \\||/", RobotColor);
                    Render(i, 21, "         |   |   |   |", RobotColor);
                    Render(i, 22, "         |---|   |---|", RobotColor);
                    Render(i, 23, "         |---|   |---|", RobotColor);
                    Render(i, 24, "         |   |   |   |", RobotColor);
                    Render(i, 25, "         |___|   |___|", RobotColor);
                    Render(i, 26, "         /   \\   /   \\", RobotColor);
                    Render(i, 27, "        |_____| |_____|", RobotColor);
                    Render(i, 28, "        |HHHHH| |HHHHH|", RobotColor);

                    Thread.Sleep(500);
                    ClearMovement();
                }
                i += 5;
            }
        }

        private void DisplayHeart()
        {
            Render(200 / 2, 15, ",d88b.d88b,", ConsoleColor.Red);
            Render(200 / 2, 16, "88888888888", ConsoleColor.Red);
            Render(200 / 2, 17, "`Y8888888Y'", ConsoleColor.Red);
            Render(200 / 2, 18, "  `Y888Y'", ConsoleColor.Red);
            Render(200 / 2, 19, "    `Y'", ConsoleColor.Red);
        }
        private void ClearHeart()
        {
            Render(200 / 2, 15, "           ", ConsoleColor.White);
            Render(200 / 2, 16, "           ", ConsoleColor.White);
            Render(200 / 2, 17, "           ", ConsoleColor.White);
            Render(200 / 2, 18, "           ", ConsoleColor.White);
            Render(200 / 2, 19, "           ", ConsoleColor.White);
        }

        private void ClearMovement()
        {
            for (int j = 0; j < 200 - 5; j++)
            {
                Render(j, 10, "      ", ConsoleColor.Red);
                Render(j, 11, "      ", ConsoleColor.Red);
                Render(j, 12, "      ", ConsoleColor.Red);
                Render(j, 13, "      ", ConsoleColor.Red);
                Render(j, 14, "      ", ConsoleColor.Red);
                Render(j, 15, "      ", ConsoleColor.Red);
                Render(j, 16, "      ", ConsoleColor.Red);
                Render(j, 17, "      ", ConsoleColor.Red);
                Render(j, 18, "      ", ConsoleColor.Red);
                Render(j, 19, "      ", ConsoleColor.Red);
                Render(j, 20, "      ", ConsoleColor.Red);
                Render(j, 21, "      ", ConsoleColor.Red);
                Render(j, 22, "      ", ConsoleColor.Red);
                Render(j, 23, "      ", ConsoleColor.Red);
                Render(j, 24, "      ", ConsoleColor.Red);
                Render(j, 25, "      ", ConsoleColor.Red);
                Render(j, 26, "      ", ConsoleColor.Red);
                Render(j, 27, "      ", ConsoleColor.Red);
                Render(j, 28, "      ", ConsoleColor.Red);

                j += 5;
            }
        }
    }

}
