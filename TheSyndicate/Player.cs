using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace TheSyndicate
{
    public class Player
    {
        private static Player _instance;
        private const int MAXIMUM_BATTERY_POWER = 1; //Max should never exceed 6
        private static string PATH_TO_SAVE_STATE { get; set; }
        public string CurrentSceneId { get; private set; }
        public int BatteryPower { get; set; }
        private static string HealthBar { get; set; }

        [JsonConstructor]
        private Player(string currentSceneId = null,
                       int batteryPower = MAXIMUM_BATTERY_POWER)
        {
            this.CurrentSceneId = currentSceneId;
            this.BatteryPower = batteryPower;
            PATH_TO_SAVE_STATE = SetPathToSaveState();
        }

        private string SetPathToSaveState()
        {
            if (GameEngine.Is_Windows)
            {
                return @"..\..\..\assets\SaveState.json";
            }
            else
            {
                return @"../../../assets/SaveState.json";
            }
        }

        public static Player GetInstance()
        {
            if (_instance == null)
            {
                _instance = GetPlayerFromSaveState();
            }
            return _instance;
        }

        private static Player GetPlayerFromSaveState()
        {
            try
            {
                return ConvertSaveStateToPlayer();
            }
            catch
            {
                return new Player();
            }
        }

        private static Player ConvertSaveStateToPlayer()
        {
            string savedPlayerDataAsJson = GetSaveState();
            return JsonConvert.DeserializeObject<Player>(savedPlayerDataAsJson);
        }

        public static string GetSaveState()
        {
            return File.ReadAllText(PATH_TO_SAVE_STATE);
        }

        public void SavePlayerData(string currentSceneId)
        {
            CurrentSceneId = currentSceneId;
            WritePlayerToFile();
        }

        public void ResetPlayerData(string firstSceneId)
        {
            CurrentSceneId = firstSceneId;
            SetBatteryToFullPower();
            WritePlayerToFile();
        }

        private void WritePlayerToFile()
        {
            string playerDataAsJson = ConvertPlayerToJson();
            File.WriteAllText(PATH_TO_SAVE_STATE, playerDataAsJson);
        }

        private string ConvertPlayerToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void RenderHealthBar()
        {
            TextBox healthBarBox = new TextBox(HealthBar, Console.WindowWidth * 3 / 4, 2, (Console.WindowWidth - (Console.WindowWidth * 3 / 4)) / 2, 2);
            healthBarBox.FormatText(HealthBar);
        }

        public void UpdateBatteryImage()
        {
            //Max amount of "life" being displayed is 37 characters long
            //We set MAX_BATTERY_POWER to 6 at begining of game
            //Therefore the number of '▒' characters to display (int amountOfPowerToDisplay) is 6 * 6 + 1 = 37
            int amountOfPowerToDisplay = 6 * this.BatteryPower + 1;
            //And the number of white spaces to display as the player loses
            //life is the difference between max life (37) and amountOfPowerToDisplay
            int amountOfSpacesToDisplay = 37 - amountOfPowerToDisplay;
            StringBuilder currentHealth = new StringBuilder();
            currentHealth.Append('█', 43);
            for (int i = 0; i < 4; i++)
            {
                currentHealth.Append("\n██ ");
                currentHealth.Append('▒', amountOfPowerToDisplay);
                currentHealth.Append(' ', amountOfSpacesToDisplay);
                currentHealth.Append(" ██");
                if (i == 1 || i == 2) currentHealth.Append('█',2);
            }
            currentHealth.Append('\n');
            currentHealth.Append('█', 43);

            HealthBar = currentHealth.ToString();
            RenderHealthBar();
        }

        public void SetBatteryToFullPower()
        {
            this.BatteryPower = MAXIMUM_BATTERY_POWER;
        }

        public void IncrementBatteryPowerByOne()
        {
            this.BatteryPower++;
        }

        public void DecrementBatteryPowerByOne()
        {
            this.BatteryPower--;
        }
    }
}
