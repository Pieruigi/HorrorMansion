using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSA.Gameplay
{
    public class BloodyMapReceiver : MonoBehaviour
    {
        BloodyMapFader[] faders; 

        // Start is called before the first frame update
        void Start()
        {
            faders = GetComponentsInChildren<BloodyMapFader>();
        }

        public void ShowBloodyMap()
        {
            foreach (var fader in faders)
                fader.ShowBloodyMap();
        }
    }

}
