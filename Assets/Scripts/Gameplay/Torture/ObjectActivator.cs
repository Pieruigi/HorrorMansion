using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSA.Gameplay
{
    public class ObjectActivator : MonoBehaviour
    {
        [SerializeField]
        List<GameObject> gameObjectList;

        private void Awake()
        {
            
        }

        public void ActivateGameObject(int index)
        {
            gameObjectList[index].SetActive(true);
        }

        public void DeactivateGameObject(int index)
        {
            gameObjectList[index].SetActive(false);
        }
        
        //public void Create(GameObject prefab)
    }

}
