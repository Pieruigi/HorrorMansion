using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace CSA.Gameplay
{
    public class TortureGroup : MonoBehaviour
    {
        //public static UnityAction<TortureGroup> OnRelease;
        //public static UnityAction<TortureGroup> OnKidnapped;
        public UnityAction OnVictimKilled;
        public UnityAction OnVictimSaved;


        [SerializeField]
        float victimLifeTime = 10;
        
        // Start is called before the first frame update
        void Start()
        {
            //RegisterVictimCallbacks();
            ChooseVictim();
        }

        void ChooseVictim()
        {
            Debug.Log($"[{nameof(TortureGroup)} - Choosing a new victim...]");
            StartTorturing();
        }

        protected async virtual void StartTorturing()
        {
            Debug.Log($"[{nameof(TortureGroup)} - Start torturing...]");
            bool killed = Random.Range(0,2) == 0 ? true : false;
            await Task.Delay(System.TimeSpan.FromSeconds(victimLifeTime));
            Debug.Log($"[{nameof(TortureGroup)} - Victim still alive:{!killed}]");
            if (killed)
                OnVictimKilled?.Invoke();
            else
                OnVictimSaved?.Invoke();

        }
     
       
     
    }

}
