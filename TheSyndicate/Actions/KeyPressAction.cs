using System;
using System.Diagnostics;

namespace TheSyndicate.Actions
{
    enum SpacebarOrTab { Neither, Spacebar, Tab }

    class KeyPressAction : IAction
    {
        private static int SECONDS_TO_PRESS_KEYS = 3;
        private static int TIMES_KEYS_MUST_BE_PRESSED = 20;
        private static string INSTRUCTIONS = $"In order to successfully complete your action you must alternate pressing the Spacebar and Tab keys at least {TIMES_KEYS_MUST_BE_PRESSED} times in {SECONDS_TO_PRESS_KEYS} seconds.";   
        private Stopwatch Stopwatch { get; }
        private ConsoleKey CurrentKeyPressed { get; set; }
        private SpacebarOrTab LastKeyPressed {get; set;}
        private int SpacebarAndTabPresses { get; set; }
        
        public KeyPressAction()
        {
            this.LastKeyPressed = SpacebarOrTab.Neither;
            this.SpacebarAndTabPresses = 0;
            this.Stopwatch = new Stopwatch();
        }

        public void ExecuteAction()
        {
            RenderInstructions();
            WaitForPlayerToPressEnter();
            HaveUserAlternatePressingSpacebarAndTab();
            RenderEndMessage();
        }
        
        private void RenderInstructions()
        {
            Console.Clear();
            Console.WriteLine(INSTRUCTIONS);
        }

        private void WaitForPlayerToPressEnter()
        {
            Console.WriteLine("Press ENTER to continue.");
            ConsoleKey userInput = Console.ReadKey(true).Key;
            while (userInput != ConsoleKey.Enter)
            {
                userInput = Console.ReadKey(true).Key;
            }
        }

        // https://stackoverflow.com/questions/5945533/how-to-execute-the-loop-for-specific-time
        private void HaveUserAlternatePressingSpacebarAndTab()
        {
            Console.Clear();
            Console.WriteLine("START!!!");
            this.Stopwatch.Start();
            while (this.Stopwatch.Elapsed <= TimeSpan.FromSeconds(SECONDS_TO_PRESS_KEYS))
            {
                if (KeyPressedIsSpacebarOrTab())
                {
                    SpacebarAndTabPresses++;
                    ToggleLastKeyPressed();
                }
            }
            this.Stopwatch.Stop();
        }

        private bool KeyPressedIsSpacebarOrTab()
        {
            SetCurrentKeyPressed();
            return IsCurrentKeyValid(ConsoleKey.Tab, SpacebarOrTab.Tab) ||
                   IsCurrentKeyValid(ConsoleKey.Spacebar, SpacebarOrTab.Spacebar);
        }

        private void SetCurrentKeyPressed()
        {
            if (Console.KeyAvailable) 
            {
                this.CurrentKeyPressed = Console.ReadKey(true).Key;
            }
        }

        private bool IsCurrentKeyValid(ConsoleKey consoleKey, SpacebarOrTab validKeyPressed)
        {
            return this.CurrentKeyPressed == consoleKey &&
                this.LastKeyPressed != validKeyPressed;
        }

        private void ToggleLastKeyPressed()
        {
            if (CurrentKeyPressed == ConsoleKey.Tab)
            {
                this.LastKeyPressed = SpacebarOrTab.Tab;
            }
            else if (CurrentKeyPressed == ConsoleKey.Spacebar)
            {
                this.LastKeyPressed = SpacebarOrTab.Spacebar;
            }
        }

        private void RenderEndMessage()
        {
            Console.Clear();
            if (DidPlayerSucceed())
            {
                Console.WriteLine($"Congratulations! You alternated pressing Tab and Spacebar {SpacebarAndTabPresses} time(s).");
            }
            else
            {
                Console.WriteLine("Darn, you were too slow. Looks like you're not going to make it.");
            }
            WaitForPlayerToPressEnter();
        }

        public int GetIndexOfDestinationBasedOnUserSuccessOrFail()
        {
            return DidPlayerSucceed() ? 1 : 0;
        }

        public bool DidPlayerSucceed()
        {
            return SpacebarAndTabPresses >= TIMES_KEYS_MUST_BE_PRESSED;
        }
    }
}
