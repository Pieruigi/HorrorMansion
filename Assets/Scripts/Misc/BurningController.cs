using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kidnapped
{
    public class BurningController : MonoBehaviour
    {
       
        [SerializeField]
        float startBurningDuration = 5;

        [SerializeField]
        float stopBurningDuration = 30;

        [SerializeField]
        List<Renderer> renderers;

        bool burning = false;
        float elapsed = 0;

        // Start is called before the first frame update
        void Start()
        {
            //Shader.SetGlobalFloat("StartBurning", 0f);
            //Shader.SetGlobalFloat("StopBurning", 0f);

            ResetBurningShaders();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
               
                StartBurning();
            }
                        

            if (burning)
            {
                elapsed += Time.deltaTime;

                if(elapsed < startBurningDuration)
                {
                    SetStartBurning(elapsed / startBurningDuration);
                }
                else
                {
                    SetStopBurning((elapsed - startBurningDuration) / stopBurningDuration);
                    
                }

                if (elapsed > stopBurningDuration + startBurningDuration)
                    burning = false;
            }

        }

        void ResetBurningShaders()
        {
            foreach (var renderer in renderers)
            {
                renderer.material.SetFloat("StartBurning", 0);
                renderer.material.SetFloat("StopBurning", 0);
            }
        }

        public void StartBurning()
        {
            SetStartBurning(0);
            burning = true;
            elapsed = 0;
        }

        void SetStartBurning(float value)
        {
            foreach (var renderer in renderers)
            {
                renderer.material.SetFloat("StartBurning", value);
            }
        }

        void SetStopBurning(float value)
        {
            foreach (var renderer in renderers)
            {
                renderer.material.SetFloat("StopBurning", value);
            }
        }
    }

}
