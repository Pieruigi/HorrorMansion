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

            // Fill the list of choices: all except the last chosen one if any
            List<TortureGroupCollection> choices = new List<TortureGroupCollection>();
            foreach(var c in collections)
            {
                if (c != current)
                    choices.Add(c);
            }

            // Choose a new torture collection
            current = choices[Random.Range(0, choices.Count)];
            // If we already used that group before we need to release it ( we are sure this way we are not close enough to see the
            // releasing process that would be eventually in another place )
            if (current.SomeoneHasDeadHere())
                current.Release();
            Debug.Log($"[{nameof(TortureGroupCollectionManager)} - New collection chosen:{current.name}]");
            current.TortureSomeone();
        }

       
    }

}
