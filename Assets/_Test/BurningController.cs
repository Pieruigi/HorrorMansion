using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningController : MonoBehaviour
{
    float burningSpeed = .1f;

    bool burning = false;
    float burningValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        Shader.SetGlobalFloat("StartBurning", 0f);
        Shader.SetGlobalFloat("StopBurning", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            burning = true;
            
        }

        if(burning)
        {
            burningValue += Time.deltaTime * burningSpeed;
            if(burningValue < 1f)
            {
                Shader.SetGlobalFloat("StartBurning", burningValue);
            }
            else
            {
                burning = false;
                Shader.SetGlobalFloat("StopBurning", 0);
            }
            
                
        }

    }
}
