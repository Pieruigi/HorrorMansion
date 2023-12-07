using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CSA.Gameplay
{
    public abstract class Victim : MonoBehaviour
    {
        public static UnityAction<Victim> OnKilled;
        public static UnityAction<Victim> OnSaved;
        public static UnityAction<Victim> OnKidnapped;

        public abstract void Kidnap(TortureDevice device);

        public abstract void Save();

        public abstract void Kill();

        
    }

}
