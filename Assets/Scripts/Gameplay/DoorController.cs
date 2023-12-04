using CSA.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSA.Gameplay
{
    public class DoorController : MonoBehaviour, IInteractable
    {

        [SerializeField]
        bool locked = false;

        [SerializeField]
        Transform pivot;

        float minAngle = 0;
        float maxAngle = 90f;

        GameObject player;
        GameObject handle;
        bool interacting = false;
        Vector3 lastHandlePosition;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (!interacting)
                    StartInteraction(new object[] { Vector3.zero });
                else
                    StopInteraction();
            }
#endif

            if (!interacting)
                return;

            // Get the handle movement
            Vector3 handleMove = handle.transform.position - lastHandlePosition;
            handleMove.y = 0;
            if(handleMove.magnitude > 0)
            {
                // Get the vector from the pivot to the handle, that should be the new door orientation
                Vector3 direction = handle.transform.position - pivot.position;
                direction.y = 0;
                // Compute the angle between the pivot forward direction and the new direction
                float angle = Vector3.SignedAngle(-pivot.right, direction, Vector3.up);
                Debug.Log($"Angle:{angle}");
                // Rotate the door
                transform.forward = Quaternion.Euler(0f, angle, 0f) * transform.forward;
            }
            
            
            



            lastHandlePosition = handle.transform.position;

        }

        GameObject GetPlayer()
        {
            if (!player)
                player = GameObject.FindGameObjectWithTag("Player");

            return player;
        }

        public bool IsInteractable()
        {
            return !locked && !interacting;
        }

        /// <summary>
        /// Param1: the interaction vector position
        /// </summary>
        /// <param name="parameters"></param>
        public void StartInteraction(object[] parameters)
        {
            // We first check for interaction enabled
            if (!IsInteractable())
                return;

            interacting = true;

            // We create an object to handle the door
            Vector3 handlePosition = (Vector3)parameters[0];
            handle = new GameObject("Handle");
            handle.transform.position = handlePosition;
            lastHandlePosition = handle.transform.position;
            // Set the player as parent
            if (GetPlayer())
                handle.transform.parent = GetPlayer().transform;

        }

        public void StopInteraction()
        {
            // Just destroy the handle
            Destroy(handle);

            interacting = false;
        }
    }

}
