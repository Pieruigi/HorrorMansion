using CSA.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSA.Builder
{
    public class LevelBuilder : Singleton<LevelBuilder>
    {


#if UNITY_EDITOR
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                Build();
        }
#endif

        public void Build()
        {
            Debug.Log("Building...");
            ClearAll();
            Initialize();
            
        }

        void ClearAll()
        {
            ElevatorManager.Instance.ClearAll();
            FloorManager.Instance.ClearAll();
        }

        void Initialize()
        {
            ElevatorManager.Instance.Initialize();
        }
        
    }

}
