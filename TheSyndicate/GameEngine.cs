﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Newtonsoft.Json;
using TheSyndicate.SoundEffects;

namespace TheSyndicate
{
    class GameEngine
    {
        private string PATH_TO_STORY { get; set; }
        private Dictionary<string, Scene> Scenes { get; set; }
        private Scene CurrentScene { get; set; }
        private Player Player { get; set; }
        private Animations Animations = new Animations();
        public static readonly bool Is_Windows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public GameEngine()
        {
            this.Player = Player.GetInstance();
            this.PATH_TO_STORY = SetPathToStory();
            LoadScenes();
            LoadCurrentScene();
        }

        public void Start()
        {
            ConsoleWindow.MaximizeWindow();
            Animations.DisplayIntroScene();
            Console.CursorVisible = true;
            while (CurrentScene.HasNextScenes() && Player.HasBatteryLife())
            {
                PlayScene();
            }
            PlayFinalScene();
        }

        private string SetPathToStory()
        {
            if (Is_Windows)
            {
                return @"assets\story.json";
            }
            else
            {
                return @"assets/story.json";
            }
        }

        private void LoadScenes()
        {
            Scenes = GetScenes();
        }

        private Dictionary<string, Scene> GetScenes()
        {
            List<Scene> scenes = ConvertStoryFromJsonToScenes();
            Dictionary<string, Scene> sceneIdsToScene = new Dictionary<string, Scene>();

            foreach (Scene scene in scenes)
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

        private void PlayScene()
        {
            Console.Clear();
            if (Is_Windows) { PlaySoundEffect(); }
            CurrentScene.Play();
            if (CurrentScene.ActualDestinationId != null)
            {
                CurrentScene = GetNextScene();
            }
        }

        private Scene GetNextScene()
        {
            return this.Scenes[CurrentScene.ActualDestinationId];
        }

        private void PlaySoundEffect()
        {
            // Sound Effects from SoundBible & Zapsplat 
            if (this.CurrentScene.Id == "animal") { Sounds.PlaySound(@"SoundEffects\SoundEffectBark.mp3"); }
            else if (this.CurrentScene.Id == "dead") { Sounds.PlaySound(@"SoundEffects\SoundEffectPowerDown.mp3"); }
            else if (this.CurrentScene.Id == "fight") { Sounds.PlaySound(@"SoundEffects\SoundEffectFight.mp3"); }
            else if (this.CurrentScene.Id == "forest") { Sounds.PlaySound(@"SoundEffects\SoundEffectNature.mp3"); }
            else if (this.CurrentScene.Id == "web") { Sounds.PlaySound(@"SoundEffects\SoundEffectUpload.mp3"); }
            else if (this.CurrentScene.Id == "recyclerTruck") { Sounds.PlaySound(@"SoundEffects\SoundEffectRecycler.mp3"); }
        }

        private void PlayFinalScene()
        {
            Console.Clear();
            string firstSceneId = GetFirstScene().Id;
            Player.ResetPlayerData(firstSceneId);
            PlayScene();
        }

    }
}
