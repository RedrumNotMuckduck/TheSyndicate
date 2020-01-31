using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TheSyndicate
{
    public class TextBox
    {
        char TOP_LEFT_CORNER = '\u2554';
        char TOP_RIGHT_CORNER = '\u2557';
        char BOTTOM_LEFT_CORNER = '\u255A';
        char BOTTOM_RIGHT_CORNER = '\u255D';
        char HORIZONTAL_LINE = '\u2550';
        char VERTICAL_LINE = '\u2551';
        private int Width { get; set; }
        private int Height { get; set; }

        public TextBox(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        void SetBoxPosition(int xCoord, int yCoord)
        {
            Console.SetCursorPosition(xCoord,yCoord);
        }

        public void DrawDialogBox()
        {
            StringBuilder box = new StringBuilder();
            DrawBoxTop(box);
            DrawBoxSides(box);
            DrawBoxBottom(box);
            Console.WriteLine(box);
        }

        public void DrawOptionsBox()
        {
            int height = (Console.WindowHeight/4) * 3;
            Console.SetCursorPosition(0, height);
            DrawDialogBox();
        }
        
        void DrawBoxTop(StringBuilder box)
        {
            box.Append(TOP_LEFT_CORNER);
            box.Append(HORIZONTAL_LINE, this.Width - 2);
            box.Append(TOP_RIGHT_CORNER).AppendLine();
        }

        void DrawBoxSides(StringBuilder box)
        {
            int count = 0;

            while (count++ < this.Height - 2)
            {
                box.Append(VERTICAL_LINE);
                box.Append(' ', this.Width - 2);
                box.Append(VERTICAL_LINE).AppendLine();
            }
        }

        void DrawBoxBottom(StringBuilder box)
        {
            box.Append(BOTTOM_LEFT_CORNER);
            box.Append(HORIZONTAL_LINE, this.Width - 2);
            box.Append(BOTTOM_RIGHT_CORNER);
        }
    }
}
