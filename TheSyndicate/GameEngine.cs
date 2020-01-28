using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

namespace TheSyndicate
{
    class GameEngine
    {
        private string PATH_TO_STORY = @"\assets\story.json";
        private List<Scene> Scenes { get; set; }
        private Scene CurrentScene { get; set; }

        public GameEngine()
        {
            LoadScenes();
            LoadCurrentScene();
        }

        public void Start()
        {
            while (CurrentScene.HasNextScenes())
            {
                PlayScene();
            }
        }

        private void LoadScenes()
        {
            Scenes = ConvertStoryFromJsonToScenes();
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
            CurrentScene = GetFirstScene();
        }

        private Scene GetFirstScene()
        {
            return Scenes.Single(scene => scene.start == true);
        }

        private void PlayScene()
        {
            CurrentScene.Play();
            CurrentScene = GetNextScene();
        }

        private Scene GetNextScene()
        {
            return CurrentScene = CurrentScene.ActualDesination;
        }

    }
}
