using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace TheSyndicate
{
    class GameEngine
    {
        Player player = Player.Instance();
        private string PATH_TO_STORY = @"..\..\..\assets\story.json";
        private Dictionary<string, Scene> Scenes { get; set; }
        private Scene CurrentScene { get; set; }


        public GameEngine()
        {
            LoadScenes();
            LoadCurrentScene();
        }

        public void Start()
        {
            ConsoleWindow.ShowWindow(ConsoleWindow.ThisConsole, ConsoleWindow.MAXIMIZE);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorVisible = true;
            while (CurrentScene.HasNextScenes())
            {
                PlayScene();
            }
            CurrentScene.Play();
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

        private string ConvertSaveIDFromJSON()
        {
            string savedID = File.ReadAllText(@"..\..\..\assets\SaveState.json");
            return savedID;
        }

        private string GetStoryFromFile()
        {
            return File.ReadAllText(PATH_TO_STORY);
        }

        private void LoadCurrentScene()
        {
            CurrentScene = GetFirstScene();
        }

        private Scene GetFirstScene()
        {
            Scene firstScene = null;
            string savedSceneId = ConvertSaveIDFromJSON();
            if (!savedSceneId.Equals("0"))
            {
                foreach (KeyValuePair<string, Scene> scene in this.Scenes)
                {
                    if (scene.Key.Equals(savedSceneId))
                    {
                        firstScene = scene.Value;
                    }
                }
                return firstScene;

            }
            else
            {
                foreach (KeyValuePair<string, Scene> scene in this.Scenes)
                {
                    if (scene.Value.Start == true)
                        firstScene = scene.Value;
                }
                return firstScene;
            }
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
    }
}
