//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.SceneManagement;

//namespace CSA
//{
//    public class GameManager : Singleton<GameManager>
//    {
//        public UnityAction OnGameStarted;
//        public UnityAction OnGameEnded;

     
//        int mainSceneIndex = 0;
//        bool inGame = false;
//        bool paused = false;
//        bool gameEnded = false;



//        private void Start()
//        {
//            SceneManager.sceneLoaded += OnSceneLoaded;
//        }

//        private void Update()
//        {
//            if (!inGame || paused || gameEnded)
//                return;

         
           
//        }

      

//        void LoadGameScene(int sceneIndex)
//        {
//            LoadScene(sceneIndex);
//        }

//        void LoadMainScene()
//        {
//            LoadScene(mainSceneIndex);
//        }

//        void LoadScene(int sceneIndex)
//        {
//            SceneManager.LoadScene(sceneIndex);
//        }

//        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//        {
//            if(scene.buildIndex == mainSceneIndex)
//            {
//                // Main scene
//                inGame = false;
//            }
//            else
//            {
//                // Game scene
//                paused = true;
//                inGame = true;
//                OnGameStarted?.Invoke();
//            }
//        }

//        void InitializeNewGame()
//        {
//            paused = false;
//            gameEnded = false;
//        }


//        public void StartNewGame(int sceneIndex, float gameDuration)
//        {
//            InitializeNewGame();
//            LoadGameScene(sceneIndex);
//        }


//    }

//}
