using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSA.Gameplay
{
    public class Floor : MonoBehaviour
    {
        [SerializeField]
        List<Transform> elevatorsTargets;

        public void ClearAll() { }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public Transform GetElevatorTargetAt(int index)
        {
            return elevatorsTargets[index];
        }
    }

}
