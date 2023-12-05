#if TWO_WAY
using CSA.Interfaces;
using DG.Tweening;
using EvolveGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSA.Gameplay
{
    public class SimpleDoorController : MonoBehaviour, IInteractable
    {
        [SerializeField]
        float positiveAngle = 90f;

        [SerializeField]
        float negativewAngle = -90f;

        [SerializeField]
        bool locked = false;

        PlayerController playerController;

        bool open = false;
        bool busy = false;

        // Start is called before the first frame update
        void Start()
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {

        }


        public bool IsInteractable()
        {
            return !locked && !busy;
        }

        public void StartInteraction(object[] parameters = null)
        {
            if (!IsInteractable())
                return;

            Vector3 dir = transform.position - playerController.transform.position;
            dir.y = 0;
            float angle = 0f; 
            if (!open)
            {
                angle = positiveAngle;
                if (Vector3.Dot(transform.parent.forward, dir) < 0)
                {
                    angle = negativewAngle;
                }
            }
            
            busy = true;
            open = !open;
            transform.DOLocalRotate(Vector3.up * angle, 0.5f).onComplete += ()=> { busy = false; };

        }

        public void StopInteraction(object[] parameters = null)
        {
            
        }

    }

}
#else
using CSA.Interfaces;
using DG.Tweening;
using EvolveGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSA.Gameplay
{
    public class SimpleDoorController : MonoBehaviour, IInteractable
    {
        [SerializeField]
        float angle = 90f;

        [SerializeField]
        bool locked = false;

        [SerializeField]
        bool clockwise = false;

        PlayerController playerController;

        bool open = false;
        bool busy = false;

        // Start is called before the first frame update
        void Start()
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {

        }


        public bool IsInteractable()
        {
            return !locked && !busy;
        }

        public void StartInteraction(object[] parameters = null)
        {
            if (!IsInteractable())
                return;

            Vector3 dir = transform.position - playerController.transform.position;
            dir.y = 0;
            float angle = 0f;
            if (!open)
            {
                angle = this.angle;
                if (!clockwise)
                    angle *= -1;
                
            }

            busy = true;
            open = !open;
            transform.DOLocalRotate(Vector3.up * angle, 0.5f).onComplete += () => { busy = false; };

        }

        public void StopInteraction(object[] parameters = null)
        {

        }

    }

}
#endif