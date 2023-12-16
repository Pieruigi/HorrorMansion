using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CSA.Gameplay
{
    public class BloodyMapFader : MonoBehaviour
    {
        [SerializeField]
        float time = .5f;

        [SerializeField]
        float delay = 0f;

        Vector2 targetTiling;

        Material[] mats;
        Vector2[] tilings;

        string mapProperty = "_DetailAlbedoMap";
        Renderer rend;

        float elapsedTime = 0;
        bool loop = false;

        private void Awake()
        {
            // Create new materials
            rend = GetComponent<Renderer>();
            mats = new Material[rend.materials.Length];
            tilings = new Vector2[rend.materials.Length];
            for (int i = 0; i < mats.Length; i++)
            {
                mats[i] = new Material(rend.materials[i]);
                tilings[i] = mats[i].GetTextureScale(mapProperty);
                mats[i].SetTextureScale(mapProperty, Vector2.zero);
                
            }
                
            rend.materials = mats;
            // Hide mask

        }

      
        // Update is called once per frame
        void Update()
        {
            if (!loop)
                return;

            elapsedTime += Time.deltaTime;
            for (int i = 0; i < mats.Length; i++)
            {
                Vector2 currTiling = rend.materials[i].GetTextureScale(mapProperty);
                rend.materials[i].SetTextureScale(mapProperty, Vector2.Lerp(Vector2.zero, tilings[i], elapsedTime / time));
            }
            // Completed ?
            if (elapsedTime >= time)
                loop = false;
            
        }

        public async void ShowBloodyMap()
        {
            await Task.Delay(System.TimeSpan.FromSeconds(delay));
            elapsedTime = 0;
            loop = true;
            
        }
    }

}
