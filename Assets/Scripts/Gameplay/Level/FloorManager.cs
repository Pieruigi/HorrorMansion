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

        /// <summary>
        /// The floor the player is currently in
        /// </summary>
        Floor currentFloor;

        protected override void Awake()
        {
            base.Awake();

            currentFloor = floors[0];
        }

        private void Start()
        {
            foreach (Floor f in floors)
                f.Deactivate();

            currentFloor.Activate();
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
