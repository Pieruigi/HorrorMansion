using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Kidnapped.SaveSystem
{
     

    public class SaveManager : Singleton<SaveManager>
    {
       
        //[System.Serializable]
        //public class Data
        //{
        //    [SerializeField]
        //    public List<Savable.Data> elements = new List<Savable.Data>();
        //}

        List<Savable> savables = new List<Savable>();

        string fileName = "save.txt";

        [SerializeField]
        DataCollection collection = new DataCollection();

        string sceneCode = "scn";

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
#if UNITY_EDITOR
            if(Input.GetKeyDown(KeyCode.S)) 
            {
                SaveGame();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                LoadGame();
            }
#endif
        }

        string GetJsonFromFileSystem()
        {
            string filePath = System.IO.Path.Combine(Application.persistentDataPath, fileName);
            // If the save file does't exist we create it
            if (!System.IO.File.Exists(filePath))
                System.IO.File.WriteAllText(filePath, "");
            // Load from file system first
            return System.IO.File.ReadAllText(System.IO.Path.Combine(Application.persistentDataPath, fileName));
            
        }

        public void RegisterSavable(Savable savable)
        {
            if (savables.Contains(savable))
                return;
            savables.Add(savable);
        }

        public void UnregisterSavable(Savable savable)
        {
            savables.Remove(savable);
        }

        public void SaveGame()
        {
            // Remove the old scene index
            collection.elements.RemoveAll(e=>e.Code == sceneCode);
            // Add the current scene index
            collection.elements.Add(new SceneData() { Code = sceneCode, Index = SceneManager.GetActiveScene().buildIndex });


            // Check all elements
            foreach (Savable savable in savables)
            {
                // Get the data to store
                Data data = savable.GetData() as Data;
                // Eventually remove the old data
                collection.elements.RemoveAll(e => e.Code == data.Code);
                // Store the new data
                collection.elements.Add(data);
            }
            string json = JsonConvert.SerializeObject(collection);
            Debug.Log($"Json:{json}");
            // Save to fs
            System.IO.File.WriteAllText(System.IO.Path.Combine(Application.persistentDataPath, fileName), json);
        }

        public void LoadGame()
        {
            // Load json from fs
            string json = GetJsonFromFileSystem();

            // Fill collection
            collection = JsonConvert.DeserializeObject<DataCollection>(json);


        }

        public void ClearAll()
        {
            collection.ClearAll();
            savables.Clear();
        }

        /// <summary>
        /// This method can be called just after LoadGame() to get the scene index.
        /// </summary>
        /// <returns></returns>
        public int GetSceneToLoad()
        {
            return (collection.elements.Find(e=>e.Code == sceneCode) as SceneData).Index;
        }
    }

}
