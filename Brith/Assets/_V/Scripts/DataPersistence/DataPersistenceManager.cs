using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace V.Tool.SaveLoadSystem
{
    public class DataPersistenceManager : MonoBehaviour
    {
        public static DataPersistenceManager Instance { get; private set;}

        [Header("File Storage")]
        [SerializeField] private string fileName;  
        [SerializeField] private bool isUseEncryption;


        private GameData gameData;

        private List<IDataPersistable> dataPersistableList;

        private FileDataHandler fileDataHandler;

        #region Unity
        private void Awake() 
        {
            if(Instance != null)
            {
                Debug.LogError("More than one DataPersistence Manager");
            }    

            Instance = this;
        }

        private void Start() 
        {
            fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName, isUseEncryption);

            dataPersistableList = FindAllDataPersistance();
            
            LoadGame();    
        }

        /// <summary>
        /// -----Polish (改成玩家可以手動存) 結束存檔
        /// </summary>
        private void OnDisable()
        {
            SaveGame();
        }
        #endregion

        /// <summary>
        /// // 找出場景所有有 IDataPersistable 的 Mono
        /// </summary>
        private List<IDataPersistable> FindAllDataPersistance()
        {
            IEnumerable<IDataPersistable> _dataPersistableList = FindObjectsOfType<MonoBehaviour>()
                .OfType<IDataPersistable>();

            return new List<IDataPersistable>(_dataPersistableList);
        }
        
        #region Start & Save & Load
        public void StartNewGame()
        {
            gameData = new GameData();
        }

        public void LoadGame()
        {
            // Load save data from json
            gameData = fileDataHandler.LoadJson();

            // if data == null, start a new game
            if(gameData == null)
            {
                Debug.LogWarning("There is no save game data found, start a new game");

                StartNewGame();
            }

            // 讀取傳入的 GameData
            foreach(IDataPersistable _dataPersistable in dataPersistableList)
            {
                _dataPersistable.LoadData(gameData);
            }
        }

        public void SaveGame()
        {
            // 修改並存除傳入的 GameData
            foreach(IDataPersistable _dataPersistable in dataPersistableList)
            {
                _dataPersistable.SaveData(ref gameData);
            }
            
            // Save the data to json
            fileDataHandler.SaveAsJson(gameData); 
        }
        #endregion
    }
}
