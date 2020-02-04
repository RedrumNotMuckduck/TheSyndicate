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

        int TEXT_BOX_X_DEFAULT = 2;
        int TEXT_BOX_Y_DEFAULT = 2;
        
        Regex NEW_LINE_PATTERN = new Regex($"\n");

        public int Width { get; set; }
        public int Height { get; set; }
        public int TextBufferX { get; set; }
        public int TextBufferY { get; set; }
        public int TextBoxX { get; set; }
        public int TextBoxY { get; set; }
        public int NewLines { get; set; }

        public TextBox(string text = "", int width = 100, int height = 20)
        {
            this.Width = width;
            this.Height = height;
            this.TextBufferX = 2;
            this.TextBufferY = 2;
            this.TextBoxX = (Console.WindowWidth - this.Width)/2;
            this.TextBoxY = TEXT_BOX_Y_DEFAULT;
        }

        public void SetBoxPosition(int xCoord = 0, int yCoord = 0)
        {
            Console.SetCursorPosition(xCoord, yCoord);
        }

        public void DrawDialogBox(string text)
        {
            StringBuilder box = new StringBuilder();
            // gets the number of \n in scene text to resize text box size to account for new lines in scene texts
            MatchCollection matches = NEW_LINE_PATTERN.Matches(text);
            this.Height += matches.Count/2;

            DrawBoxTop(box);
            DrawBoxSides(box);
            DrawBoxBottom(box);
        }

        public void DrawBoxTop(StringBuilder box)
        {
            box.Clear();
            box.Append(TOP_LEFT_CORNER);
            // this.Width - 2 for corner characters
            box.Append(HORIZONTAL_LINE, this.Width - 2);
            box.Append(TOP_RIGHT_CORNER);
            SetBoxPosition(TextBoxX, TextBoxY);
            Console.Write(box);
            TextBoxY++;
        }

        public void DrawBoxSides(StringBuilder box)
        {
            int count = 0;

            while (count++ < this.Height)
            {
                box.Clear();
                box.Append(VERTICAL_LINE);
                //this.Width - 2 for right and left box borders
                box.Append(' ', this.Width - 2);
                box.Append(VERTICAL_LINE);
                SetBoxPosition(TextBoxX, TextBoxY);
                Console.WriteLine(box);
                TextBoxY++;
            }
        }

        public void DrawBoxBottom(StringBuilder box)
        {
            box.Clear();
            box.Append(BOTTOM_LEFT_CORNER);
            //this.Width - 2 for corner characters
            box.Append(HORIZONTAL_LINE, this.Width - 2);
            box.Append(BOTTOM_RIGHT_CORNER);
            SetBoxPosition(TextBoxX, TextBoxY);
            Console.WriteLine(box);
            TextBoxY = TEXT_BOX_Y_DEFAULT;
        }

        public void FormatText(string text)
        {
            StringBuilder boxText = new StringBuilder();
            int lineWidth = this.Width - (TextBufferX * 2);
            int textLength = text.Length;
            int newLineIndex;
            int startIndex = 0;
            int endIndex = lineWidth;
            int textStartX = TextBufferX + TextBoxX;
            int textStartY = TextBufferY + TextBoxY;

            while (startIndex < textLength)
            {
                //TODO: Use this for line
                //string dashChar;

                newLineIndex = NEW_LINE_PATTERN.Match(text, startIndex, endIndex - startIndex).Index;

                //TODO: Need to work on getting \n to render properly, currently using SetBoxPosition to handle new lines.
                //TODO: Lines 118-129 need work to handle when \n is at 0 index. Probably due to \n reading as 1 character and not two.

                //if (newLineIndex < endIndex && newLineIndex == 0 && (startIndex + newLineIndex + 2) < textLength)
                //{
                //    boxText.Append(' ', TextBoxX + TextBufferX);
                //    boxText.Append(text, startIndex, newLineIndex + 2);
                //    SetBoxPosition(textStartX, textStartY);
                //    Console.Write(boxText);
                //    startIndex += 2;
                //    endIndex = startIndex + lineWidth > textLength ? textLength : startIndex + lineWidth;
                //    textStartY++;
                //    boxText.Clear();
                //}
                //else

                //checks if \n appears before maximum lineWidth, if so, then renders up to \n and continues to next line
                if (newLineIndex < endIndex && newLineIndex != 0)
                {
                    boxText.Append(text, startIndex, newLineIndex - startIndex);
                    SetBoxPosition(textStartX, textStartY);
                    Console.Write(boxText);
                    startIndex = newLineIndex + 2;
                    endIndex = startIndex + lineWidth > textLength ? textLength : startIndex + lineWidth;
                    textStartY++;
                    boxText.Clear();
                }
                else
                {
                    boxText.Append(text, startIndex, endIndex - startIndex);
                    SetBoxPosition(textStartX, textStartY);
                    Console.Write(boxText);
                    startIndex = endIndex;
                    endIndex = startIndex + lineWidth > textLength ? textLength : startIndex + lineWidth;
                    textStartY++;
                    boxText.Clear();
                }
            }
        }
    }
}
