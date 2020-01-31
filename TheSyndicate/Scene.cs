using System;
using System.Text;
using System.Text.RegularExpressions;

namespace TheSyndicate
{
    public class Scene
    {
        Regex NEW_LINE_PATTERN = new Regex(@"\n");
        public string Id { get; private set; }
        public string Text { get; private set; }
        public string[] Options { get; private set; }
        public string[] Destinations { get; private set; }
        public string ActualDestinationId { get; private set; }
        public bool Start { get; private set; }

        public Scene(string id, string text, string[] options, string[] destinations, bool start)
        {
            this.Id = id;
            this.Text = text;
            this.Options = options;
            this.Destinations = destinations;
            this.ActualDestinationId = null;
            this.Start = start;
        }

        public void Play()
        {
            RenderText();
            RenderOptions();
            GetUserInput();
        }

        void RenderText()
        {
            ClearConsole();
            FormatTextForTextBox();
            //Console.WriteLine(this.Text);
        }
        
        void FormatTextForTextBox()
        {
            StringBuilder boxText = new StringBuilder();
            double maxLineWidth = Console.WindowWidth - 10;
            double lineCount = Math.Ceiling(this.Text.Length/maxLineWidth);
            int lineWidth = Convert.ToInt32(maxLineWidth);
            int textLength = this.Text.Length;
            int newLineIndex;
            int startIndex = 0;
            int endIndex = lineWidth;

            //for (int i = 1; i <= lineCount; i++)
            while (startIndex < textLength)
            {
                //int startIndex = (i * lineWidth) - lineWidth;
                //int endIndex = i * lineWidth > textLength ? textLength - 1 : i * lineWidth;
                string dashChar;

                //if (i < lineCount)
                //{
                    newLineIndex = NEW_LINE_PATTERN.Match(this.Text, startIndex, textLength - startIndex).Index;
                    if (newLineIndex < endIndex && newLineIndex !=0)
                    {
                        boxText.Append(this.Text, startIndex, newLineIndex - startIndex + 1);
                        startIndex = newLineIndex + 2;
                        endIndex = startIndex + lineWidth > textLength ? textLength : startIndex + lineWidth;
                    }
                    else
                    {
                    //dashChar = !this.Text[endIndex].Equals(" ") && endIndex != textLength ? "-" : "";
                    //dashChar = endIndex >= textLength ? "" : (!this.Text[endIndex + 1].Equals(" ") ? "-" : "");
                        boxText.Append(this.Text, startIndex, endIndex - startIndex).AppendLine();
                        //.Append(dashChar).AppendLine();
                        startIndex = endIndex;
                        endIndex = startIndex + lineWidth > textLength ? textLength : startIndex + lineWidth;
                    }
                //}
                //else
                //{
                //    newLineIndex = NEW_LINE_PATTERN.Match(this.Text, startIndex, endIndex).Index;
                //    boxText.Append(this.Text, startIndex, endIndex);
                //}
            }
            //Console.SetCursorPosition(10, 4);
            Console.WriteLine(boxText);
        }

        //Line break point using this.Text.lastindexof(" ")

        void RenderOptions()
        {
            if (this.Options.Length > 0) 
            {
                RenderUserOptions();
            }
            else
            {
                RenderQuitMessage();
            }
        }

        private void RenderUserOptions()
        {
            RenderInstructions();
            for (int i = 0; i < this.Options.Length; i++)
            {
                Console.WriteLine($"{i + 1}: {this.Options[i]}\n");
            }
        }

        private void RenderInstructions()
        {
            Console.WriteLine("\n\nWhat will you do next? Enter the number next to the option and press enter:\n");
        }

        private void RenderQuitMessage()
        {
            Console.WriteLine("\n\nYou have reached the end of your journey. Press CTRL + C to end.");
        }

        void GetUserInput()
        {
            int selectedOption;

            do
            {
                Int32.TryParse(Console.ReadLine(), out selectedOption);
            }
            while (!IsValidInput(selectedOption));

            SetDestinationId(selectedOption);
        }

        bool IsValidInput(int selectedOption)
        {
            int numberOfOptions = this.Options.Length;
            return selectedOption > 0 && selectedOption <= numberOfOptions;
        }

        void ClearConsole()
        {
            Console.Clear();
        }

        void SetDestinationId(int selectedOption)
        {
            this.ActualDestinationId = this.Destinations[selectedOption - 1];
        }
        
        public bool HasNextScenes()
        {
            return Destinations.Length > 0;
        }
    }
}
