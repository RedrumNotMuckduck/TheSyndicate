using System;
using System.Collections.Generic;
using System.Text;

namespace TheSyndicate
{
    public class Scene
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string[] Options { get; set; }
        public Dictionary<int, string> Destinations { get; set; }
        public Scene ActualDestination { get; set; }
        public bool Start { get; set; }


        public Scene(string id, string text, string[] options, Dictionary<int, string> destinations, Scene actualDestination, bool start)
        {
            this.Id = id;
            this.Text = text;
            this.Options = options;
            this.Destinations = destinations;
            this.ActualDestination = actualDestination;
            this.Start = start;
        }

        void RenderText()
        {
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
            ConsoleKeyInfo keyPress;
            do
            {
                keyPress = Console.ReadKey(true);
                Console.WriteLine(IsValidInput(keyPress.KeyChar));
            }
            while (!IsValidInput(keyPress.KeyChar));

            int selectedOption = keyPress.KeyChar - 49;
            GetDestination(selectedOption);
        }

        bool IsValidInput(char keyPress)
        {
            if (this.Options.Length == 3)
                return (keyPress > 48 && keyPress < 52) ? true : false;
            else if (this.Options.Length == 2)
                return (keyPress > 48 && keyPress < 51) ? true : false;
            else
                return false;
        }

        void ClearConsole()
        {
            
        }

        string GetDestination(int selectedOption)
        {
            string sceneId = this.Destinations[selectedOption];
            return sceneId;
        }

        void Save()
        {

        }

        void SetCurrent()
        {

        }
    }
}
