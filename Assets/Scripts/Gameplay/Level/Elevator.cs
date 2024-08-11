using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CSA.Gameplay
{
    public class Elevator : MonoBehaviour
    {
        [SerializeField]
        GameObject leftDoor, rightDoor;

        // Reacheable floors
        List<Floor> floors = new List<Floor>();

        bool doorIsOpen = false;
        float speed = 1.5f;

        public IList<Floor> Floors
        {
            get { return floors.AsReadOnly(); }
        }

        Floor currentFloor;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        async Task CloseDoors()
        {
            if (!doorIsOpen)
                return;
            
            float time = 1;
            leftDoor.transform.DOLocalMoveX(-0.3f, time, false).SetEase(Ease.OutBounce);
            rightDoor.transform.DOLocalMoveX(0.3f, time, false).SetDelay(.2f).SetEase(Ease.OutBounce);
            await Task.Delay(System.TimeSpan.FromSeconds(time * 1.2f));
            doorIsOpen = false;
            
        }

        async Task OpenDoors()
        {
            if (doorIsOpen)
                return;

            float time = 1f;
            leftDoor.transform.DOLocalMoveX(0.85f, time, false).SetEase(Ease.OutBounce);
            rightDoor.transform.DOLocalMoveX(-0.85f, time, false).SetDelay(.2f).SetEase(Ease.OutBounce);
            await Task.Delay(System.TimeSpan.FromSeconds(time * 1.2f));
            doorIsOpen = true;
        }

        async Task Move(Floor targetFloor)
        {
            if (currentFloor == targetFloor)
                return;
            
            Transform target = targetFloor.GetElevatorTargetAt(ElevatorManager.Instance.GetElevatorIndex(this));
            // Move to target position
            float time = Mathf.Abs(transform.position.y - target.position.y) / speed;
            transform.DOMoveY(target.position.y, time, false);
            await Task.Delay(System.TimeSpan.FromSeconds(time * 1.2f));
            currentFloor = targetFloor;
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

        public Floor GetFloorAt(int index)
        {
            return floors[index];
        }

        public async void MoveToFloor(Floor destinationFloor)
        {
            // Close doors if open
            await CloseDoors();

            // Move
            await Move(destinationFloor);

            // Open doors
            await OpenDoors();

        }



        public void ClearAll()
        {
            floors.Clear();
        }
    }

}
