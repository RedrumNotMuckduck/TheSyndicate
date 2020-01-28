using System;
using System.Collections.Generic;
using System.Text;

namespace TheSyndicate
{
    public class Scene
    {
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

        void Save()
        {

        }

        void SetCurrent()
        {

        }
    }
}
