using System;
using System.Diagnostics;
using System.Threading;

namespace TheSyndicate.Actions
{
    enum Attack { LeftHook, RightHook, LaserBeam }
    enum Dodge { DodgeRight, DodgeLeft, Duck, NoDodge }

    class FightAction : IAction
    {
        private static int SECONDS_USER_HAS_TO_DODGE = 1;
        private static int TIMES_USER_MUST_DODGE_ATTACKS = 3;
        private static int NUMBER_OF_ATTACKS_TO_DEFEND_AGAINST = 5;
        private static string INSTRUCTIONS = $"HALT. YOU ARE NOT AUTHORIZED TO ACCESS THIS LOCATION!!\n\nYou turn to find a relic of the war between The Syndicate and humans, a Watchman robot. You've gotten yourself into a fight! You're a lover, not a fighter though so you refuse to hurt your opponent. Looks like you're going to have to dodge.\nYour opponent will attack you {NUMBER_OF_ATTACKS_TO_DEFEND_AGAINST} time(s) and you must successfully dodge {TIMES_USER_MUST_DODGE_ATTACKS} time(s). \nYou will have {SECONDS_USER_HAS_TO_DODGE} second(s) to respond by pressing the correct arrow key.\nIf your opponent throws a left hook, you must dodge right (right arrow key)\nIf your opponent throws a right hook, you must dodge left (left arrow key)\nIf your opponent shoots a laser beam, you must duck(down arrow key)";
        private static int NumberOfTypesOfAttacks = Attack.GetNames(typeof(Attack)).Length;
        private Stopwatch Stopwatch { get; set; }
        private Random Random { get; }
        private int SuccessfullDodges { get; set; }
        private Attack CurrentAttack { get; set; }
        private Dodge CurrentDodge { get; set; }
        private ConsoleKey CurrentKeyPressed { get; set; }

        public FightAction()
        {
            this.SuccessfullDodges = 0;
            this.Random = new Random();
        }

        public void ExecuteAction()
        {
            Console.CursorVisible = false;
            RenderInstructions();
            WaitForPlayerToPressEnter();
            CountdownToFight();
            Fight();
            RenderEndMessage();
            Console.CursorVisible = true;
        }

        private void RenderInstructions()
        {
            TextBox instructions = new TextBox(INSTRUCTIONS, Console.WindowWidth / 3, 2, Console.WindowWidth / 3, Console.WindowHeight / 4);
            Console.Clear();
            instructions.SetBoxPosition(instructions.TextBoxX, instructions.TextBoxY);
            instructions.FormatText(INSTRUCTIONS);
        }

        private void WaitForPlayerToPressEnter()
        {
            string enterPrompt = "Press ENTER to continue.";
            Console.SetCursorPosition(Console.WindowWidth / 2 - enterPrompt.Length / 2, Console.WindowHeight - (Console.WindowHeight / 5));
            Console.WriteLine(enterPrompt);

            ConsoleKey userInput = Console.ReadKey(true).Key;
            while (userInput != ConsoleKey.Enter)
            {
                userInput = Console.ReadKey(true).Key;
            }
        }

        private void CountdownToFight()
        {
            Console.Clear();
            FlashCountdown(AsciiArt.ThreeAscii);
            Thread.Sleep(700);
            FlashCountdown(AsciiArt.TwoAscii);
            Thread.Sleep(700);
            FlashCountdown(AsciiArt.OneAscii);
        }

        private void Fight()
        {
            for (int i = 0; i < NUMBER_OF_ATTACKS_TO_DEFEND_AGAINST; i++)
            {
                CurrentDodge = Dodge.NoDodge;
                RenderFightOptions();
                SetCurrentAttack();
                Console.SetCursorPosition(Console.WindowWidth / 2 - 18, Console.WindowHeight / 2);
                Console.WriteLine($"Opponent's attack: {CurrentAttack}");
                if (CurrentAttack == Attack.LaserBeam)
                {
                    Flash(AsciiArt.LaserBeamAscii);
                }
                else if (CurrentAttack == Attack.LeftHook)
                {
                    Flash(AsciiArt.LeftHookAscii);
                }
                else if (CurrentAttack == Attack.RightHook)
                {
                    Flash(AsciiArt.RightHookAscii);
                }
                SetCurrentDodge();
                Thread.Sleep(100);

                if (UserSuccessfullyDodged())
                {
                    SuccessfullDodges++;
                }
            }
        }


        private void RenderFightOptions()
        {
            string options = "Left Hook  --> Right Dodge (Right Arrow Key)\nRight Hook --> Left Dodge (Left Arrow Key)\nLaser Beam --> Duck (Down Arrow Key)";
            TextBox instructions = new TextBox(options, Console.WindowWidth / 3, 2, Console.WindowWidth / 2 - Console.WindowWidth / 6, Console.WindowHeight / 4);
            Console.Clear();
            instructions.SetBoxPosition(instructions.TextBoxX, instructions.TextBoxY);
            instructions.FormatText(options);
        }

        private void SetCurrentAttack()
        {
            CurrentAttack = (Attack)GetRandomNumberLessThanNumberOfAttacks();
        }

        private int GetRandomNumberLessThanNumberOfAttacks()
        {
            return Random.Next(0, NumberOfTypesOfAttacks);
        }

        private void SetCurrentDodge()
        {
            GetUserInput();
            if (CurrentKeyPressed == ConsoleKey.LeftArrow ||
                CurrentKeyPressed == ConsoleKey.RightArrow ||
                CurrentKeyPressed == ConsoleKey.DownArrow)
            {
                CurrentDodge = ConvertUserInputToDodge();
            }
        }

        static void Flash(string[] art)
        {
            for (int i = 0; i < art.Length; i++)
            {
                // Sets "art" to be under $"Opponent's attack: {CurrentAttack}"
                Console.SetCursorPosition(Console.WindowWidth / 2 - 20, Console.WindowHeight / 2 + 2 + i);
                Console.WriteLine(art[i]);
            }

            Thread.Sleep(1000);
            for (int i = 0; i < art.Length + 2; i++)
            {
                // Clears image from the bottom up
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                ClearCurrentConsoleLine();
            }
        }

        private void FlashCountdown(string[] art)
        {
            for (int i = 0; i < art.Length; i++)
            {
                // Centers the image 
                Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 5 + i);
                Console.WriteLine(art[i]);
            }

            Thread.Sleep(800);
            for (int i = 0; i < art.Length + 2; i++)
            {
                // Clears image from the bottom up
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                ClearCurrentConsoleLine();
            }
        }

        public static void ClearCurrentConsoleLine()
        {
            // Replaces text with empty string
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        private void GetUserInput()
        {
            Stopwatch = new Stopwatch();
            Stopwatch.Start();
            while (this.Stopwatch.Elapsed <= TimeSpan.FromSeconds(SECONDS_USER_HAS_TO_DODGE))
            {
                SetCurrentKeyPressed();
            }
            this.Stopwatch.Stop();
        }

        private void SetCurrentKeyPressed()
        {
            if (Console.KeyAvailable)
            {
                this.CurrentKeyPressed = Console.ReadKey(true).Key;
            }
        }

        private Dodge ConvertUserInputToDodge()
        {
            if (CurrentKeyPressed == ConsoleKey.LeftArrow)
            {
                return Dodge.DodgeLeft;
            }
            else if (CurrentKeyPressed == ConsoleKey.RightArrow)
            {
                return Dodge.DodgeRight;
            }
            else if (CurrentKeyPressed == ConsoleKey.DownArrow)
            {
                return Dodge.Duck;
            }
            else
            {
                return Dodge.NoDodge;
            }
        }

        private void RenderEndMessage()
        {
            Console.Clear();
            if (DidPlayerSucceed())
            {
                string successMessage = $"Phew, that was close! You successfully dodged {SuccessfullDodges} attack(s). You've still been caught but at least you live to see another day. Off to the reclamation center you go.";
                Console.SetCursorPosition(Console.WindowWidth / 2 - successMessage.Length / 2, Console.WindowHeight / 2);
                Console.WriteLine(successMessage);
            }
            else
            {
                string failMessage = $"Darn, you were too slow. You have lost battery power.";
                Console.SetCursorPosition(Console.WindowWidth / 2 - failMessage.Length / 2, Console.WindowHeight / 2);
                Console.WriteLine(failMessage);
            }
            WaitForPlayerToPressEnter();
        }

        private bool UserSuccessfullyDodged()
        {
            return (int)CurrentAttack == (int)CurrentDodge;
        }

        public int GetIndexOfDestinationBasedOnUserSuccessOrFail()
        {
            return DidPlayerSucceed() ? 1 : 0;
        }

        public bool DidPlayerSucceed()
        {
            return SuccessfullDodges >= TIMES_USER_MUST_DODGE_ATTACKS;
        }
    }
}
