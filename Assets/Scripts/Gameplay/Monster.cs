#define USE_DISTANCE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSA.Gameplay
{
    public class Monster : MonoBehaviour
    {
        [SerializeField]
        float speed;

        GameObject player;

        
        bool acting = false;
#if USE_DISTANCE
        [SerializeField]
        float walkingDistance = 20f;
        //float currentWalkingDistance = 0;
        float walkingDistanceDefault;
        Vector3 lastPlayerPosition;
#else
        [SerializeField]
        float actingTime = 10f;
        System.DateTime lastActingTime;        
#endif   
        private void Awake()
        {
#if USE_DISTANCE
            walkingDistanceDefault = walkingDistance;
            RandomizeWalkingDistance();
#endif
        }

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag(Tags.Player);
            lastPlayerPosition = player.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (!acting)
            {
#if USE_DISTANCE
                // Compute how far player moved from the last time
                float dist = (player.transform.position - lastPlayerPosition).magnitude;
                walkingDistance -= dist;
                lastPlayerPosition = player.transform.position;
                if(walkingDistance < 0)
                {
                    // Start acting
                    acting = true;
                }
#else

#endif
            }
            else
            {

            }
        }

#if USE_DISTANCE
        void RandomizeWalkingDistance()
        {
            float err = walkingDistanceDefault * 1.2f;
            walkingDistance = walkingDistanceDefault + Random.Range(-err, err);
        }
#endif
    }

}

