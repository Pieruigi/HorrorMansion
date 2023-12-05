using CSA.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CSA.UI
{
    public class InteractionClueUI : MonoBehaviour
    {
        [SerializeField]
        Color interactionColor;

        [SerializeField]
        Color defaultColor;

        Image image;
        
        private void Awake()
        {
            image = GetComponentInChildren<Image>();
            image.color = defaultColor;

        }

        // Start is called before the first frame update
        void Start()
        {
            
            FindObjectOfType<InteractionController>().OnInteractionClueShow += (show) => { image.color = show ? interactionColor : defaultColor; };
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
