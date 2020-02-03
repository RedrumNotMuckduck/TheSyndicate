using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TheSyndicate
{
    public class Player
    {
        private static Player _instance;
        public void SaveIDFunc(string id)
        {

            //write to json saveState
            string infoToSave = id;
            string infoSavedAsJSON = JsonConvert.SerializeObject(infoToSave);
            //Console.WriteLine(infoToSave);
            //Console.WriteLine(infoSavedAsJSON);
            File.WriteAllText(@"..\..\..\assets\SaveState.json", infoSavedAsJSON);


        }

        public void EmptySaveStateJSONfile()
        {
            var infoToDelete = 0;
            string infoSavedAsJSON = JsonConvert.SerializeObject(infoToDelete);
            File.WriteAllText(@"..\..\..\assets\SaveState.json", infoSavedAsJSON);
        }

        public static Player Instance()
        {
            if (_instance == null)
            {
                _instance = new Player();
            }

            return _instance;
        }
    }
}
