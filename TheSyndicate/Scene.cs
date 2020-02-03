using System;

namespace TheSyndicate
{
    public class Scene
    {
        Player player = Player.Instance();
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
            if (this.Options.Length > 0) 
            {
                RenderUserOptions();
            }     
            else 
            {
                player.EmptySaveStateJSONfile();
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
            Console.WriteLine($"Press 0 at any point to save");
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

            if (selectedOption == 0) 
            {
                //func 
                player.SaveIDFunc(this.Id);
                Console.WriteLine("Saved!");
                Console.WriteLine("Continue when you are ready!!!!");
                GetUserInput();
                

            } 
            else 
            {
                SetDestinationId(selectedOption);
            }
           
        }

        bool IsValidInput(int selectedOption)
        {
            int numberOfOptions = this.Options.Length;
            return selectedOption >= 0 && selectedOption <= numberOfOptions;
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
