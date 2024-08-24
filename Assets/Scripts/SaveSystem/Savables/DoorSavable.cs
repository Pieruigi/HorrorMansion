using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Kidnapped.SaveSystem
{
    public class DoorSavable : Savable
    {
        

        public override void SetData(object data)
        {
            
        }

        public override object GetData()
        {
            return new DoorData() { code = SaveCode, IsLocked = true, IsOpen = false };

        }
    }

}
