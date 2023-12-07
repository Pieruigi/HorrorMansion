using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace CSA.Gameplay
{
    public class VictimGroup : MonoBehaviour
    {
        public static UnityAction<VictimGroup> OnRelease;
        public static UnityAction<VictimGroup> OnKidnapped;
        public static UnityAction<VictimGroup> OnKilled;
        public static UnityAction<VictimGroup> OnSaved;

        [SerializeField]
        List<Victim> victims;


        TortureDevice tortureDevice;
        
        Victim currentVictim = null;
        
        // Start is called before the first frame update
        void Start()
        {
            RegisterVictimCallbacks();
        }

     
        void RegisterVictimCallbacks()
        {
            Victim.OnKidnapped += (v) => { };
            Victim.OnSaved += (v) => { if(currentVictim = v) Release(); };
            Victim.OnKilled += (v) => { if(currentVictim = v) Release(); };
        }

        public void KidnapVictim()
        {
            currentVictim = victims[Random.Range(0, victims.Count)];

            StartKilling();

            OnKidnapped?.Invoke(this);
        }

        public void Release()
        {
            currentVictim = null;
        }

        void StartKilling()
        {
            // Do something here
            bool killed = false;
            if(killed)
            {
                Release();
                OnKilled?.Invoke(this);
            }
            else
            {
                Release();
                OnSaved?.Invoke(this);
            }
        }
    }

}
