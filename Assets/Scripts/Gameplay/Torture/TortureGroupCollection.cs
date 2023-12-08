using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CSA.Gameplay
{
    public class TortureGroupCollection : MonoBehaviour
    {
        public UnityAction OnVictimKilled;
        public UnityAction OnVictimSaved;


        [SerializeField]
        List<TortureGroup> prefabs;

        TortureGroup currentGroup;

     
        void RegisterTortureGroupCallbacks(TortureGroup group)
        {
            group.OnVictimKilled += () => { OnVictimKilled?.Invoke(); };
            group.OnVictimSaved += () => { OnVictimSaved?.Invoke(); };
        }

        public void TortureSomeone()
        {
            Debug.Log($"[{nameof(TortureGroupCollection)} - TortureSome()]");
            // Destroty the old group if any
            if (currentGroup)
                Destroy(currentGroup.gameObject);

            // Create a new group
            currentGroup = Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
            RegisterTortureGroupCallbacks(currentGroup);
            Debug.Log($"[{nameof(TortureGroupCollection)} - New group chosen:{currentGroup.name}]");
        }

        public void Release()
        {
            Debug.Log($"[{nameof(TortureGroupCollection)} - Release()]");
            if (!currentGroup)
                return;
            Destroy(currentGroup.gameObject);
            currentGroup = null;
        }
      
    }

}
