using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSA.Interfaces
{
    public interface IInteractable
    {
        void StartInteraction(object[] parameters = null);

        void StopInteraction(object[] parameters = null);

        bool IsInteractable();

    }

}
