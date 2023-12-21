using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSA.Gameplay
{
    public class Elevator : MonoBehaviour
    {
        // Reacheable floors
        List<Floor> floors = new List<Floor>();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Add a floor that is reacheable by this elevator
        /// </summary>
        public void AddFloor(Floor floor)
        {
            // Ordering
            //int id = int.Parse(floor.gameObject.name.Substring(floor.gameObject.name.IndexOf("-") + 1));
            int id = FloorManager.Instance.GetFloorIndex(floor);
            int index = -1;
            for (int i = 0; i < floors.Count && index < 0; i++)
            {
                //int otherId = int.Parse(floors[i].gameObject.name.Substring(floors[i].gameObject.name.IndexOf("-") + 1));
                int otherId = FloorManager.Instance.GetFloorIndex(floors[i]);
                if (otherId > id)
                    index = i;
            }
            if (index < 0)
                floors.Add(floor);
            else
                floors.Insert(index, floor);
        }

        public void ClearAll()
        {
            floors.Clear();
        }
    }

}
