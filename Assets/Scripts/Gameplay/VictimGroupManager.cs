using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSA.Gameplay
{
    public class VictimGroupManager : MonoBehaviour
    {
       
        [SerializeField]
        List<VictimGroup> victimGroups;

        int victimCount = 0;
        VictimGroup currentGroup = null;

        int savedCount = 0;

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
            KidnapNewVictim();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void Initialize()
        {
            VictimGroup.OnKilled += (vg) => { KidnapNewVictim(); };
            VictimGroup.OnSaved += (vg) => { savedCount++; KidnapNewVictim(); };
            victimGroups = new List<VictimGroup>(FindObjectsOfType<VictimGroup>());

        }

        void KidnapNewVictim()
        {
            // Create a list of available group indices 
            List<VictimGroup> availables = new List<VictimGroup>();
            for(int i=0; i< victimGroups.Count; i++)
            {
                if (victimGroups[i] != currentGroup) // We don't use the last one if any
                    availables.Add(victimGroups[i]);
            }

            // Choose a new prefab
            currentGroup = availables[Random.Range(0, availables.Count)];
            currentGroup.KidnapVictim();
       
        }



     
    }

  

}
