using Newtonsoft.Json;
using System.IO;

namespace TheSyndicate
{
    public class Player
    {
        private static Player _instance;
        private const int MAXIMUM_BATTERY_POWER = 4;
        private static string PATH_TO_SAVE_STATE = @"..\..\..\assets\SaveState.json";
        public string CurrentSceneId { get; private set; }
        public int BatteryPower { get; set; }

        [JsonConstructor]
        private Player(string currentSceneId = null,
                       int batteryPower = MAXIMUM_BATTERY_POWER)
        {
            this.CurrentSceneId = currentSceneId;
            this.BatteryPower = batteryPower;
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
