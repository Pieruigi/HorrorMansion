using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSA.Gameplay
{
    public class TortureGroupCollectionManager : MonoBehaviour
    {
        
        List<TortureGroupCollection> collections;

        TortureGroupCollection current;

        int killedCount = 0;
        int savedCount = 0;

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
            TortureSomeone();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Initialize()
        {
            // Fill the collection list
            collections = new List<TortureGroupCollection>(FindObjectsOfType<TortureGroupCollection>());
            // Register callbacks
            foreach(var collection in collections)
            {
                collection.OnVictimKilled += () => { killedCount++; TortureSomeone(); };
                collection.OnVictimSaved += () => { savedCount++; TortureSomeone(); };
            }
        }

        

        void TortureSomeone()
        {
            Debug.Log($"[{nameof(TortureGroupCollectionManager)} - TortureSome()]");

            // Release the old collection if any
            if (current)
                current.Release();

            // Fill the list of choices: all except the last chosen one if any
            List<TortureGroupCollection> choices = new List<TortureGroupCollection>();
            foreach(var c in collections)
            {
                if (c != current)
                    choices.Add(c);
            }

            // Choose a new torture collection
            current = choices[Random.Range(0, choices.Count)];
            Debug.Log($"[{nameof(TortureGroupCollectionManager)} - New collection chosen:{current.name}]");
            current.TortureSomeone();
        }

       
    }

}
