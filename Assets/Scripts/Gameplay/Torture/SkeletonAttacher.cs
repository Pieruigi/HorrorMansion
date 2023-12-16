using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CSA.Gameplay
{
    /// <summary>
    /// It can be used together with the ObjectActivator script
    /// </summary>

    public class SkeletonAttacher : MonoBehaviour
    {

        /// <summary>
        /// 0: victim
        /// 1: killer
        /// </summary>
        [SerializeField]
        string objectName;

        [SerializeField]
        string boneName;

        [SerializeField]
        Vector3 localPosition;

        [SerializeField]
        Vector3 localEulerAngles;


        private void OnEnable()
        {
            Transform target = FindTarget();
            if (!target)
                return;

            // Attach the object
            transform.parent = target;
            transform.localPosition = localPosition;
            transform.localEulerAngles = localEulerAngles;
        }

        Transform FindTarget()
        {
            Transform ret = null;
            for(int i=0; i<transform.root.childCount && !ret; i++)
            {
                if (objectName.ToLower().Equals(transform.root.GetChild(i).gameObject.name.ToLower()))
                    ret = transform.root.GetChild(i);
            }

            if(string.IsNullOrEmpty(boneName))
                return ret;

            // Find the bone
            return ret.GetComponentsInChildren<Transform>().Where(t => boneName.ToLower().Equals(t.gameObject.name.ToLower())).First();
            
        }
    }

}
