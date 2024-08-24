using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

namespace Kidnapped.SaveSystem
{
    [System.Serializable]
    public abstract class Data
    {
        public string Code { get; set; }
    }
}
