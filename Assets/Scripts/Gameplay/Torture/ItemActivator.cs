using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CSA.Gameplay
{
    public class ItemActivator : MonoBehaviour
    {
        [SerializeField]
        string itemName;

        // Start is called before the first frame update
        void Start()
        {
            List<Transform> objects = new List<Transform>(GetComponentsInChildren<Transform>().Where(o=>o.CompareTag(Tags.Item)));
            foreach (var o in objects)
            {
                if (itemName.ToLower().Trim().Equals(o.gameObject.name.ToLower()))
                    o.gameObject.SetActive(true);
                else
                    o.gameObject.SetActive(false);
            }
                

            
        }

    }

}
