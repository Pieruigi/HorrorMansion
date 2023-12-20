using CSA.Builder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace CSA
{
    public class GameManager : Singleton<GameManager>
    {
        
        bool inGame = false;
        bool loading = false;

        const int menuSceneBuildIndex = 0;
        const int gameSceneBuildIndex = 1;

        int currentSeed; // Caching to show the player the current seed ( or for debug purposes )

        //Theme theme = Theme.Default;
        //public Theme Theme
        //{
        //    get { return theme; }
        //}

        protected override void Awake()
        {
            base.Awake();
            SceneManager.sceneLoaded += HandleOnSceneLoaded;
        }

        public void StartGame()
        {
            SceneManager.LoadScene(gameSceneBuildIndex);
        }

        //public void StartGame()
        //{
        //    int seed = (int)DateTime.Now.Ticks;
        //    StartGame(seed);
        //}



        //public void StartGame(int seed)
        //{
            
        //    if (inGame || loading)
        //        return;

        //    loading = true;
            
        //    SceneManager.LoadSceneAsync(gameSceneBuildIndex, LoadSceneMode.Single).completed += (op) => 
        //    {
        //        loading = false;
        //        inGame = true;

        //        Debug.Log($"Game scene loaded, initializing random state with seed {seed}");
        //        // Set the seed and build the level
        //        UnityEngine.Random.InitState(seed);
        //        currentSeed = seed;
        //        LevelBuilder.Instance.Build();
               
        //    };
        //}

        void HandleOnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            switch (scene.buildIndex)
            {
                case menuSceneBuildIndex:
                    inGame = false;
                    break;
                case gameSceneBuildIndex:
                    inGame = true;
                    
                    // Set the seed and build the level
                    int seed = (int)DateTime.Now.Ticks;
                    Debug.Log($"Game scene loaded, initializing random state with seed {seed}");
                    UnityEngine.Random.InitState(seed);
                    currentSeed = seed;
                    LevelBuilder.Instance.Build();
                    break;
            }
        }
        
    }

}
