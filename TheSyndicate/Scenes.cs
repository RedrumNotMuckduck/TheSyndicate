using System;
using System.Collections.Generic;
using System.Text;

namespace TheSyndicate
{
    public class Scenes
    {
        public string Id { get; private set; }
        public string Text { get; private set; }
        public string[] Options { get; private set; }
        public Dictionary<int, string> Destinations { get; private set; }
        public string ActualDestinationId { get; private set; }
        public bool Start { get; private set; }


        public Scenes(string id, string text, string[] options, Dictionary<int, string> destinations, Scenes actualDestinationId, bool start)
        {
            this.Id = id;
            this.Text = text;
            this.Options = options;
            this.Destinations = destinations;
            this.ActualDestinationId = actualDestinationId;
            this.Start = start;
        }

        void RenderText()
        {
            ClearConsole();
            Console.WriteLine(this.Text);
        }
        
        void RenderOptions()
        {
            for(int i = 0; i < this.Options.Length; i++) 
            {
                Console.WriteLine($"{i + 1}: {this.Options[i]}");
            }
        }

        void GetUserInput()
        {
            int selectedOption;

            do
            {
                Int32.TryParse(Console.ReadLine(), out selectedOption);
            }
            while (!IsValidInput(selectedOption));

            GetDestination(selectedOption);
        }

        bool IsValidInput(int selectedOption)
        {
            int numberOfOptions = this.Options.Length;
            return (selectedOption > 0 && selectedOption <= numberOfOptions) ? true : false;
        }

        void ClearConsole()
        {
            Console.Clear();
        }

        string GetDestination(int selectedOption)
        {
            this.ActualDestinationId = this.Destinations[selectedOption - 1];
            return ActualDestinationId;
        }

        void Save()
        {

        }

        void SetCurrent()
        {

        }
    }
}
