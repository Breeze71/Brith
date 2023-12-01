using System;
using System.IO;
using UnityEngine;


namespace V.Tool.SaveLoadSystem
{   
    /// <summary>
    /// 處理 json 文件
    /// </summary>
    public class FileDataHandler
    {
        /// <summary>
        /// 目錄文件夾
        /// </summary>
        private string dataDirectoryPath = "";
        /// <summary>
        /// 文件名稱
        /// </summary>
        private string dataFileName = "";

        public FileDataHandler(string _dataDirectoryPath, string _dataFileName)
        {
            dataDirectoryPath = _dataDirectoryPath;
            dataFileName = _dataFileName;
        }

        public GameData LoadJson()
        {
            // 在任何 OS 中都 Equals to "dataDirectoryPath / dataFileName"
            string _fullPath = Path.Combine(dataDirectoryPath, dataFileName);
            GameData _loadedData = null;

            // 沒文件直接 return null
            if(!File.Exists(_fullPath))
            {
                Debug.LogWarning("not exist");
                return _loadedData;
            }

            // Try Load the File 
            try
            {
                string _dataToLoad = "";

                // Read the Serialize data from the file
                using(FileStream _strem = new FileStream(_fullPath, FileMode.Open))
                {
                    using (StreamReader _reader = new StreamReader(_strem))
                    {
                        _dataToLoad = _reader.ReadToEnd();
                    }
                }

                // Deserialize the data from Json to C# object
                _loadedData = JsonUtility.FromJson<GameData>(_dataToLoad);
            }

            catch(Exception _e)
            {
                Debug.LogError("Error when Load the file: " + _fullPath + "\n" + _e);
            }

            return _loadedData;
        }

        public void SaveAsJson(GameData _gameData)
        {
            // 在任何 OS 中都 Equals to "dataDirectoryPath / dataFileName"
            string _fullPath = Path.Combine(dataDirectoryPath, dataFileName);

            // Try Save the File
            try
            {
                // when not exist, Create the Directory the file will be written at
                Directory.CreateDirectory(Path.GetDirectoryName(_fullPath));

                // Serialize the C# data into Json
                string _dataToStore = JsonUtility.ToJson(_gameData, true);  // format true

                // Write the Serialize data to the file
                using(FileStream _strem = new FileStream(_fullPath, FileMode.Create))
                {
                    using(StreamWriter _writer = new StreamWriter(_strem))
                    {
                        _writer.Write(_dataToStore);
                    }
                }
            }
            catch(Exception _e)
            {
                Debug.LogError("Error when Save the file: " + _fullPath + "\n" + _e);
            }
        }
    }
}
