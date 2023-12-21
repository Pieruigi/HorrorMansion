using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSA.Gameplay
{
    public class ElevatorManager : Singleton<ElevatorManager>
    {
        [SerializeField]
        List<Elevator> elevators;

        public IList<Elevator> Elevators
        {
            get { return elevators.AsReadOnly(); }
        }

        public void ClearAll()
        {
            foreach (Elevator elevator in elevators)
                elevator.ClearAll();
                
        }

        public void Randomize()
        {
            //
            // Connecting floors
            //
            // Create a new list with all the available floors
            List<Floor> notConnectedFloors = new List<Floor>(FloorManager.Instance.Floors);
            // Choose a floor to start with to randomize the level
            Floor nextFloor = notConnectedFloors[Random.Range(0, notConnectedFloors.Count)];
            notConnectedFloors.Remove(nextFloor);
            // Get an elevator from the list
            Elevator nextElevator = elevators[Random.Range(0, elevators.Count)];
            // Add the selected floor to the selected elevator
            nextElevator.AddFloor(nextFloor);

            // Keeping track all the elevators we can be sure all of them will be used
            Elevator lastElevator = null;
            List<Elevator> usedElevators = new List<Elevator>();
            usedElevators.Add(nextElevator);

            // Loop through all the remaining floors
            while (notConnectedFloors.Count > 0)
            {
                // Get a new floor to connect to the last selected
                nextFloor = notConnectedFloors[Random.Range(0, notConnectedFloors.Count)];
                notConnectedFloors.Remove(nextFloor);
                // Connect both floors
                nextElevator.AddFloor(nextFloor);
                // Get any elevator different from the last two used elevators
                List<Elevator> tmpElevators = elevators.FindAll(e => e != nextElevator && e != lastElevator);
                lastElevator = nextElevator;
                nextElevator = tmpElevators[Random.Range(0, tmpElevators.Count)];

                // An elevator that has already been used means the next floor is reacheable from a previously
                // connected floor; in this way we can have less connections
                if (!usedElevators.Contains(nextElevator))
                {
                    usedElevators.Add(nextElevator);
                    // Add the new floor to the new elevator
                    nextElevator.AddFloor(nextFloor);
                }

            }

            // Set a starting floor for each elevator
            foreach(var e in elevators)
            {
                // Get the floor index relative to the elevator floor list
                int index = Random.Range(0, e.Floors.Count);
                // Move to the new floor
                e.MoveToFloor(e.GetFloorAt(index));
            }
        }

        public int GetElevatorIndex(Elevator elevator)
        {
            return elevators.IndexOf(elevator);
        }
    }

}
