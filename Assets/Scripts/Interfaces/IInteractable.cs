using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSA.Interfaces
{
    public interface IInteractable
    {
        void StartInteraction(object[] parameters);

        void StopInteraction();

        bool IsInteractable();
    }

}
