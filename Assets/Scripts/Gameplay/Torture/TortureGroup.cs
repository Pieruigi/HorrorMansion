using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace CSA.Gameplay
{
    public class TortureGroup : MonoBehaviour
    {
      
        public UnityAction OnVictimKilled;
        public UnityAction OnVictimSaved;

        [SerializeField]
        List<GameObject> victimPrefabs;

        [SerializeField]
        Transform victimSpawnPoint;

        [SerializeField]
        GameObject killer;

        [SerializeField]
        float victimLifeTime = 10;

        [SerializeField]
        Collider lookAtTrigger;

        [SerializeField]
        float lookAtDistance = 2f;

        [SerializeField]
        Collider saveTrigger;

        [SerializeField]
        float saveDistance = 1.5f;

        bool awaitForKilling = false;
        bool saving = false;
        GameObject player;

        Animator[] animators;
        GameObject victim;
        string animIdleParam = "Idle";
        string animKillParam = "Kill";
        string animJoyParam = "Joy";
        string animSadParam = "Sad";

        // Start is called before the first frame update
        protected virtual void Start()
        {
            Initialize();
            ChooseVictim();
            InitializeAnimators();
            StartTorturing();
        }

        protected virtual void Update()
        {
            // Is lifetime expired or victim already dead?
            if ((victimLifeTime<0 && !awaitForKilling) || saving)
                return;

            // The victim is still alive
            if (!awaitForKilling) // You can still save the victim
            {
                CheckLifeTime();   
            }
            else // The victim is doomed
            {
                AwaitForKilling();
            }     
        }


        /// <summary>
        /// Override this method to kill the victim ( play animations, vfx, etc )
        /// </summary>
        /// <returns></returns>
        protected async virtual Task DoKillTheVictim()
        {

            await Task.Delay(System.TimeSpan.FromSeconds(3)); // Do some killing here
        }

        /// <summary>
        /// Override this method to save the victim ( play animations, vfx, etc )
        /// </summary>
        /// <returns></returns>
        protected async virtual Task DoSaveTheVictim()
        {
            await Task.Delay(System.TimeSpan.FromSeconds(3)); // Do some killing here
        }

        
        void Initialize()
        {
            player = GameObject.FindGameObjectWithTag(Tags.Player);
            // Activate the save trigger 
            saveTrigger.enabled = true;
            // Deactivate the look at trigger
            lookAtTrigger.enabled = false;
        }

        void CheckLifeTime()
        {
            victimLifeTime -= Time.deltaTime;

            if (victimLifeTime < 0)
            {
                // Deactivate the save trigger 
                saveTrigger.enabled = false;
                // Activate the look at trigger
                lookAtTrigger.enabled = true;
                // Await for the player
                awaitForKilling = true;
            }
        }

        void CheckForSaving()
        {
            if (victimLifeTime < 0)
                return;

        }

        /// <summary>
        /// We want the player to see how the victim is going to be killed
        /// </summary>
        void AwaitForKilling()
        {
            // Compute distance between collider and player
            float distance = Vector3.ProjectOnPlane(player.transform.position - lookAtTrigger.transform.position, Vector3.up).magnitude;
            // Is the player within the minimum range ?
            if(distance < lookAtDistance)
            {
                // Raycast
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit hitInfo;
                if(Physics.Raycast(ray, out hitInfo, distance))
                {
                    // Check if we hit the collider
                    if(hitInfo.collider == lookAtTrigger)
                    {
                        // Ok, kill the victim
                        awaitForKilling = false;
                        KillTheVictim();
                    }
                }
            }
        }

        void ChooseVictim()
        {
            Debug.Log($"[{nameof(TortureGroup)} - Choosing a new victim...]");
            // Create a new random victim
            victim = Instantiate(victimPrefabs[Random.Range(0, victimPrefabs.Count)], victimSpawnPoint);
            victim.transform.localPosition = Vector3.zero;
            victim.transform.localRotation = Quaternion.identity;
        }

        void InitializeAnimators()
        {
            animators = GetComponentsInChildren<Animator>();
        }

        protected virtual void StartTorturing()
        {
            Debug.Log($"[{nameof(TortureGroup)} - Start torturing...]");
            
        }
     
        /// <summary>
        /// Called internally when life time is expired and the player is close
        /// </summary>
        async void KillTheVictim()
        {
            await DoKillTheVictim();

            OnVictimKilled?.Invoke();
        }

        
        /// <summary>
        /// Called externally by the player clicking on the save trigger
        /// </summary>
        public async void SaveTheVictim()
        {
            saving = true;
            await DoSaveTheVictim(); // Set the victim free here

            OnVictimSaved?.Invoke();
        }
     

    }

}
