using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kidnapped
{
    public class SimpleDoor : MonoBehaviour
    {
        [SerializeField]
        float openTime = 1;

        [SerializeField]
        float openAngle = 90;


        DoorController controller;
        Collider coll;

        private void Awake()
        {
            controller = GetComponentInParent<DoorController>();
            coll = GetComponent<Collider>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            DoorController.OnDoorOpened += HandleOnDoorOpened;
            DoorController.OnDoorOpenFailed += HandleOnDoorOpenFailed;
            DoorController.OnDoorClosed += HandleOnDoorClosed;
        }

        private void OnDisable()
        {
            DoorController.OnDoorOpened -= HandleOnDoorOpened;
            DoorController.OnDoorOpenFailed -= HandleOnDoorOpenFailed;
            DoorController.OnDoorClosed -= HandleOnDoorClosed;
        }

        private void HandleOnDoorOpenFailed(DoorController arg0)
        {
            Debug.Log("The door is locked and can't be opened");
        }

        private void HandleOnDoorClosed(DoorController controller)
        {
            Debug.Log("HandleOnCloseDoor");
            if (this.controller != controller)
                return;
            Vector3 endValue = transform.eulerAngles - Vector3.up * openAngle;
            Debug.Log($"Target angle:{endValue}");
            transform.DORotate(endValue, openTime, RotateMode.Fast);
        }

        private void HandleOnDoorOpened(DoorController controller)
        {
            if (this.controller != controller)
                return;
            // Disable collider to avoid hitting the player
            //coll.enabled = false;
            Vector3 endValue = transform.eulerAngles + Vector3.up * openAngle;
            transform.DORotate(endValue, openTime, RotateMode.Fast);//.onComplete += () => { coll.enabled = true; };
        }
    }

}
