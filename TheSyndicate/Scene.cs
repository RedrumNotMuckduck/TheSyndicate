using System;
using System.Text;
using System.Text.RegularExpressions;
using TheSyndicate.Actions;

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
            
            //TextBox is instantiated to pass this.Text and get access to TextBox Width and Height properties 

            TextBox dialogBox = new TextBox(this.Text, Console.WindowWidth * 3 / 4, (this.Text.Length / (Console.WindowWidth - 8)) + 4);
            dialogBox.DrawDialogBox(this.Text);
            dialogBox.FormatText(this.Text);

            //returning dialogBox for information about height of dialog box

            return dialogBox; 
        }
        
        void RenderOptions(TextBox sceneTextBox)
        {
            int optionsBoxX = sceneTextBox.TextBoxX;
            int optionsBoxY = sceneTextBox.Height + sceneTextBox.TextBoxY + sceneTextBox.TextBufferY;

            //checks for end scene
            if (this.Options.Length > 0) 
            {
                RenderUserOptions(sceneTextBox, optionsBoxX, optionsBoxY);
            }
            else
            {
                RenderQuitMessage(sceneTextBox, optionsBoxX, optionsBoxY);
                player.EmptySaveStateJSONfile();
            }
        }

        private void RenderUserOptions(TextBox sceneTextBox, int optionsBoxX, int optionsBoxY)
        {
            sceneTextBox.SetBoxPosition(optionsBoxX, optionsBoxY);

            RenderInstructions(sceneTextBox, optionsBoxX, optionsBoxY);
            optionsBoxY += 6;

            for (int i = 0; i < this.Options.Length; i++)
            {
                sceneTextBox.SetBoxPosition(optionsBoxX, optionsBoxY);
                Console.WriteLine($"{i + 1}: {this.Options[i]}");
                optionsBoxY += 2;
            }
            sceneTextBox.SetBoxPosition(optionsBoxX, Console.WindowHeight - 2);
            Console.WriteLine($"Press 0 at any point to save");
        }

        private void RenderInstructions(TextBox sceneTextBox, int optionsX, int optionsY)
        {
            optionsY += 4;
            sceneTextBox.SetBoxPosition(optionsX, optionsY);
            Console.WriteLine("What will you do next? Enter the number next to the option and press enter:");
        }

        private void RenderQuitMessage(TextBox sceneTextBox, int optionsX, int optionsY)
        {
            optionsY += 4;
            sceneTextBox.SetBoxPosition(optionsX, optionsY);
            Console.WriteLine("You have reached the end of your journey. Press CTRL + C to end.");
            Console.ForegroundColor = ConsoleColor.Green;
        }

        void GetUserInput()
        {
            int selectedOption;

            do
            {
                //hides input text
                Console.ForegroundColor = ConsoleColor.Black;
                Int32.TryParse(Console.ReadLine(), out selectedOption);
            }
            while (!IsValidInput(selectedOption));

            //resets text color to green
            Console.ForegroundColor = ConsoleColor.Green;

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