using System;
using System.Collections.Generic;
using System.Text;

namespace TheSyndicate
{
    public class Board
    {
        private static int Width;
        private static int Height;
        private static int LocationX;
        private static int LocationY;
        private static ConsoleColor BorderColor;

        public Board()
        {
            if (GameEngine.Is_Windows)
            {
                //Console.SetWindowSize(200, 60); 
            }
            CreateBoard(120, 40, 6, 3, ConsoleColor.White);
            Draw();
            DrawLine(120, 7, 20);
            RenderGameTitle();
            RenderBattery();
            Console.ReadLine();
        }
        private static void CreateBoard(int width, int height, int locationX, int locationY, ConsoleColor borderColor)
        {
            Width = width;
            Height = height;
            LocationX = locationX;
            LocationY = locationY;
            BorderColor = borderColor;
        }
        public static void Draw()
        {
            string s = "╔";
            string space = "";
            string temp = "";
            for (int i = 0; i < Width; i++)
            {
                space += " ";
                s += "═";
            }

            for (int j = 0; j < LocationX; j++)
                temp += " ";

            s += "╗" + "\n";

            for (int i = 0; i < Height; i++)
                s += temp + "║" + space + "║" + "\n";

            s += temp + "╚";
            for (int i = 0; i < Width; i++)
                s += "═";

            s += "╝" + "\n";

            Console.ForegroundColor = BorderColor;
            Console.CursorTop = LocationY;
            Console.CursorLeft = LocationX;
            Console.Write(s);

            Console.ResetColor();
        }

        public static void RenderContent(int locationX, int locationY, string text)
        {
            Console.CursorTop = locationY;
            Console.CursorLeft = locationX;
            Console.Write(text); 
        }
        public static void DrawLine(int Width, int locationX, int locationY)
        {
            string s = ""; 
            for( int i = 1; i<= Width; i++)
            {
                s += "_"; 
            }

            Console.CursorTop = locationY;
            Console.CursorLeft = locationX;
            Console.Write(s); 

        }
        public static void RenderGameTitle()
        {
            RenderContent(35, 5, " ▀▀█▀▀ █░░█ █▀▀   █▀▀ █░░█ █▀▀▄ █▀▀▄ ░▀░ █▀▀ █▀▀█ ▀▀█▀▀ █▀▀");
            RenderContent(35, 6, " ░░█░░ █▀▀█ █▀▀   ▀▀█ █▄▄█ █░░█ █░░█ ▀█▀ █░░ █▄▄█ ░░█░░ █▀▀");
            RenderContent(35, 7, " ░░▀░░ ▀░░▀ ▀▀▀   ▀▀▀ ▄▄▄█ ▀░░▀ ▀▀▀░ ▀▀▀ ▀▀▀ ▀░░▀ ░░▀░░ ▀▀▀");
        }

        public static void RenderBattery()
        {
            RenderContent(35, 10, "█████████████████████████████████████████████");
            RenderContent(35, 11, "██  ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒  ██");
            RenderContent(35, 12, "██  ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒  ████");
            RenderContent(35, 13, "██  ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒  ████");
            RenderContent(35, 14, "██  ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒  ██");
            RenderContent(35, 15, "█████████████████████████████████████████████"); 
        }
    }
}
