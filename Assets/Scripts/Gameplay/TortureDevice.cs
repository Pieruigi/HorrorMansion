using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSA.Gameplay
{
    public abstract class TortureDevice : MonoBehaviour
    {
        public abstract void Start(Victim victim);

        public abstract void Stop();

    }

}
