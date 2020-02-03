using System;
using System.Text;
using System.Text.RegularExpressions;
using TheSyndicate.Actions;

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
        public IAction Action { get; set; }

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
            TextBox sceneTextBox = RenderText();
            RenderOptions(sceneTextBox);
            GetUserInput();
        }

        TextBox RenderText()
        {
            ClearConsole();
            TextBox dialogBox = new TextBox(this.Text, Console.WindowWidth - 4, (this.Text.Length / (Console.WindowWidth - 8)) + 4);
            dialogBox.DrawDialogBox(this.Text);
            dialogBox.FormatText(this.Text);
            return dialogBox; //returning dialogBox for information about height of dialog box
        }
        
        void RenderOptions(TextBox sceneTextBox)
        {
            if (this.Options.Length > 0) 
            {
                RenderUserOptions(sceneTextBox);
            }
            else
            {
                RenderQuitMessage(sceneTextBox);
            }
        }

        private void RenderUserOptions(TextBox sceneTextBox)
        {
            RenderInstructions(sceneTextBox);
            for (int i = 0; i < this.Options.Length; i++)
            {
                sceneTextBox.SetBoxPosition( 4 , ((Console.WindowHeight * 3 / 4) + 2 + i));
                Console.WriteLine($"{i + 1}: {this.Options[i]}\n");
            }
        }

        private void RenderInstructions(TextBox sceneTextBox)
        {
            sceneTextBox.SetBoxPosition(4, Console.WindowHeight * 3 / 4);
            Console.WriteLine("What will you do next? Enter the number next to the option and press enter:");
        }

        private void RenderQuitMessage(TextBox sceneTextBox)
        {
            sceneTextBox.SetBoxPosition(4, Console.WindowHeight * 3 / 4);
            Console.WriteLine("You have reached the end of your journey. Press CTRL + C to end.");
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
            if (this.ActualDestinationId.Equals("fight"))
            {
                this.Action = new FightAction();
                Action.ExecuteAction();
                if (Action.DidPlayerSucceed())
                {
                    this.ActualDestinationId = "recyclerTruck";
                }
                else
                {
                    this.ActualDestinationId = "dead";
                }
            }
            else if (this.Id.Equals("upload") || 
                (this.Id.Equals("recyclerTruck") && this.ActualDestinationId.Equals("city")))
            {
                this.Action = new KeyPressAction();
                Action.ExecuteAction();
                if (!Action.DidPlayerSucceed())
                {
                    this.ActualDestinationId = "dead";
                }
            }
        }
        
        public bool HasNextScenes()
        {
            return Destinations.Length > 0;
        }
    }
}