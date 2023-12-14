using AndreyGraphics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSA.Gameplay
{
    public class BloodExplosion : MonoBehaviour
    {
        [SerializeField]
        List<GameObject> meatList;

        [SerializeField]
        float rate = .25f;

        int count = 0;
        System.DateTime lastSpawnTime;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if(count < meatList.Count)
            {
                if((System.DateTime.Now-lastSpawnTime).TotalSeconds > rate)
                {
                    meatList[count].SetActive(true);
                    lastSpawnTime = System.DateTime.Now;
                    count++;
                }
                
            }
        }
    }

}
