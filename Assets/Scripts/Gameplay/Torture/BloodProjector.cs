using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CSA.Gameplay
{
    public class BloodProjector : MonoBehaviour
    {
        [SerializeField]
        float time = .5f;

        [SerializeField]
        float delay = 0f;

        Projector projector;
        float farClipPlaneMax = 3f;
        //float farClipPlane = 0;

        float elapsedTime = 0;
        bool loop = false;

        private void Update()
        {
            if (!loop)
                return;

            elapsedTime += Time.deltaTime;
            projector.farClipPlane = Mathf.Lerp(0, farClipPlaneMax,  elapsedTime / time);
            // Completed ?
            if (elapsedTime >= time)
                loop = false;
           
        }

        void OnEnable()
        {
            Init();
        }

        async void Init()
        {
            projector = GetComponent<Projector>();
            farClipPlaneMax = projector.farClipPlane;
            projector.farClipPlane = 0;
            
            await Task.Delay(System.TimeSpan.FromSeconds(delay));
            // Start
            elapsedTime = 0;
            loop = true;
        }

    }

}
