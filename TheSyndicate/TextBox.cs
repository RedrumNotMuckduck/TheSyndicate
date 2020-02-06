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

        public string Text { get; private set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int TextBufferX { get; set; }
        public int TextBufferY { get; set; }
        public int TextBoxX { get; set; }
        public int TextBoxY { get; set; }
        public int NewLines { get; set; }

        public TextBox(string text = "", int width = 100, int height = 2)
        {
            this.Text = text;
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
            //MatchCollection matches = NEW_LINE_PATTERN.Matches(text);
            //this.Height += matches.Count;
            

            DrawBoxTop(box);
            DrawBoxSides(box);
            DrawBoxBottom(box);
        }

        public void DrawBoxTop(StringBuilder box)
        {
            box.Clear();
            box.Append(TOP_LEFT_CORNER);
            // this.Width - 2 accounts for corner characters
            box.Append(HORIZONTAL_LINE, this.Width - 2);
            box.Append(TOP_RIGHT_CORNER);
            SetBoxPosition(TextBoxX, TextBoxY);
            Console.Write(box.ToString());
            TextBoxY++;
        }

        public void DrawBoxSides(StringBuilder box)
        {
            int count = 0;

            while (count++ < this.Height)
            {
                box.Clear();
                SetBoxPosition(TextBoxX, TextBoxY);
                box.Append(VERTICAL_LINE);
                Console.Write(box.ToString());
                //this.Width - 2 for right and left box borders
                //box.Append(' ', this.Width - 2);
                
                //box.Append(VERTICAL_LINE);
                SetBoxPosition(TextBoxX + this.Width - 1, TextBoxY);
                Console.Write(box.ToString());
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
            Console.WriteLine(box.ToString());
        }

        public void FormatText(string text)
        {
            StringBuilder boxText = new StringBuilder();
            //TextBufferX * 2 for buffer on both sides
            int lineWidth = this.Width - (TextBufferX * 2);
            int newLineIndex;
            int startIndex = 0;
            int endIndex = lineWidth;
            int textStartX = TextBufferX + TextBoxX;
            int textStartY = TextBufferY + TEXT_BOX_Y_DEFAULT;
            int lastSpaceInALine;


            while (startIndex < text.Length)
            {
                //TODO: Use this for line
                //string dashChar;
                lastSpaceInALine = CheckForLastSpaceInALine(lineWidth, endIndex);

                newLineIndex = NEW_LINE_PATTERN.Match(text, startIndex, endIndex - startIndex).Index;

                //TODO: Need to work on getting \n to render properly, currently using SetBoxPosition to handle new lines.
                //TODO: Lines 118-129 need work to handle when \n is at 0 index. Probably due to \n reading as 1 character and not two. Might need $();

                //if (newLineIndex < endIndex && newLineIndex == 0 && (startIndex + newLineIndex + 1) < text.Length)
                //{
                //    boxText.Append(' ', TextBoxX + TextBufferX);
                //    boxText.Append(text, startIndex, newLineIndex + 1);
                //    SetBoxPosition(textStartX, textStartY);
                //    Console.Write(boxText.ToString());
                //    startIndex += 2;
                //    endIndex = startIndex + lineWidth > text.Length ? text.Length : startIndex + lineWidth;
                //    textStartY++;
                //    boxText.Clear();
                //}
                //else

                //checks if \n appears before maximum lineWidth, if so, then renders up to \n and continues to next line
                if (newLineIndex < endIndex && newLineIndex != 0)
                {
                    boxText.Append(text, startIndex, newLineIndex - startIndex);
                    SetBoxPosition(textStartX, textStartY);
                    Console.Write(boxText.ToString());
                    startIndex = newLineIndex + 1;
                    endIndex = startIndex + lineWidth > text.Length ? text.Length : startIndex + lineWidth;
                    textStartY++;
                    this.Height += 1;
                    boxText.Clear();
                }
                else if (lastSpaceInALine < endIndex && endIndex - startIndex >= lineWidth)
                {
                    boxText.Append(text, startIndex, lastSpaceInALine - startIndex);
                    SetBoxPosition(textStartX, textStartY);
                    Console.Write(boxText.ToString());
                    startIndex = lastSpaceInALine + 1;
                    endIndex = startIndex + lineWidth > text.Length ? text.Length : startIndex + lineWidth;
                    textStartY++;
                    this.Height += 1;
                    boxText.Clear();
                }
                else
                {
                    boxText.Append(text, startIndex, endIndex - startIndex);
                    SetBoxPosition(textStartX, textStartY);
                    Console.Write(boxText.ToString());
                    startIndex = endIndex;
                    endIndex = startIndex + lineWidth > text.Length ? text.Length : startIndex + lineWidth;
                    textStartY++;
                    this.Height += 1;
                    boxText.Clear();
                }
            }

            int CheckForLastSpaceInALine(int lineWidth, int endIndex)
            {
                return this.Text.LastIndexOf(" ", endIndex, lineWidth);
            }
        }
    }
}
