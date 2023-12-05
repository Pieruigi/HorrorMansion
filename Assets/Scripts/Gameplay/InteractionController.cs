using CSA.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CSA.Gameplay
{
    public class InteractionController : MonoBehaviour
    {
        public UnityAction<bool> OnInteractionClueShow;
        

        [SerializeField]
        float interactionRange = 1.5f;

        IInteractable interactable; 

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 origin = Camera.main.transform.position;
            Vector3 direction = Camera.main.transform.forward;
            Ray ray = new Ray(origin, direction);
            RaycastHit hitInfo;
            IInteractable hit = null;
            if(Physics.Raycast(ray, out hitInfo, interactionRange))
            {
                hit = hitInfo.collider.GetComponent<IInteractable>();
            }

            if(hit != null)
            {
                if(interactable == null) // We are not interacting yet
                {
                    if (hit.IsInteractable())
                    {
                        // We can interact, show some clue
                        OnInteractionClueShow?.Invoke(true);

                        if (Input.GetMouseButtonDown(0))
                        {
                            // Hide clue
                            OnInteractionClueShow?.Invoke(false);
                            // Start interaction
                            interactable = hit;
                            hit.StartInteraction(new object[] { hitInfo.point });
                        }
                    }
                }
                else // We are interacting ( we assume we have no chance to hit two different interactables at the same time )
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        // Stop interacting
                        interactable.StopInteraction();
                        interactable = null;
                    }
                }
            }
            else
            {
                OnInteractionClueShow?.Invoke(false);
                // We need to check if we were already interacting
                if (interactable != null)
                {
                    interactable.StopInteraction();
                    interactable = null;
                }
            }
        }
    }

}
