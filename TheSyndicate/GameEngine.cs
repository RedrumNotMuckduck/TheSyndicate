using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace TheSyndicate
{
    class GameEngine
    {
        private string PATH_TO_STORY = @"..\..\..\assets\story.json";
        private Dictionary<string, Scene> Scenes { get; set; }
        private Scene CurrentScene { get; set; }
        private Player Player { get; set; }

        public GameEngine()
        {
            this.Player = Player.GetInstance();
            LoadScenes();
            LoadCurrentScene();
        }

        private void LoadScenes()
        {
            Scenes = GetScenes();
        }

        private Dictionary<string, Scene> GetScenes()
        {
            List<Scene> scenes = ConvertStoryFromJsonToScenes();
            Dictionary<string, Scene> sceneIdsToScene = new Dictionary<string, Scene>();
            
            foreach(Scene scene in scenes)
            {
                sceneIdsToScene[scene.Id] = scene;
            }

            return sceneIdsToScene;
        }

        // https://stackoverflow.com/questions/18192357/deserializing-json-object-array-with-json-net
        private List<Scene> ConvertStoryFromJsonToScenes()
        {
            string story = GetStoryFromFile();
            return JsonConvert.DeserializeObject<List<Scene>>(story);
        }

        private string GetStoryFromFile()
        {
            return File.ReadAllText(PATH_TO_STORY);
        }

        private void LoadCurrentScene()
        {
            CurrentScene = GetStartingScene();
        }

        private Scene GetStartingScene()
        {
            if (this.Player != null && this.Player.CurrentSceneId != null)
            {
                return GetSceneFromPlayer();
            }
            else
            {
                return GetFirstScene();
            }
        }

        private Scene GetSceneFromPlayer()
        {
            Scene startScene = null;
            foreach (KeyValuePair<string, Scene> scene in this.Scenes)
            {
                if (scene.Key.Equals(Player.CurrentSceneId))
                {
                    startScene = scene.Value;
                }
            }
            return startScene;
        }

        private Scene GetFirstScene()
        {
            Scene stateScene = null;
            foreach (KeyValuePair<string, Scene> scene in this.Scenes)
            {
                if (scene.Value.Start == true)
                {
                    stateScene = scene.Value;
                }
            }
            return stateScene;
        }

        public void Start()
        {
            ConsoleWindow.ShowWindow(ConsoleWindow.ThisConsole, ConsoleWindow.MAXIMIZE);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorVisible = false;
            while (CurrentScene.HasNextScenes())
            {
                PlayScene();
            }
            PlayFinalScene();
        }

        private void PlayScene()
        {
            CurrentScene.Play();
            CurrentScene = GetNextScene();
        }

        private Scene GetNextScene()
        {
            return this.Scenes[CurrentScene.ActualDestinationId];
        }

        private void PlayFinalScene()
        {
            string firstSceneId = GetFirstScene().Id;
            Player.ResetPlayerData(firstSceneId);
            CurrentScene.Play();
        }
    }
}
