using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSA.Gameplay
{
    public class FloorManager : Singleton<FloorManager>
    {
        [SerializeField]
        List<Floor> floors;

        public IList<Floor> Floors
        {
            get { return floors.AsReadOnly(); }
        }

        public int GetFloorIndex(Floor floor)
        {
            return floors.IndexOf(floor);
        }

        public void ClearAll()
        {
            foreach (Floor floor in floors)
                floor.ClearAll();
        }
    }

}
