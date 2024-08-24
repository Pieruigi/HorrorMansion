using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kidnapped.SaveSystem
{
    

    public abstract class Savable: MonoBehaviour
    {
        public abstract object GetData();
        public abstract void SetData(object data);

        [SerializeField]
        string saveCode;
        public string SaveCode
        {
            get { return saveCode; }
        }
        

        protected virtual void OnEnable()
        {
            SaveManager.Instance.RegisterSavable(this);
        }

        protected virtual void OnDisable()
        {
            SaveManager.Instance.UnregisterSavable(this);
        }


    }

}
