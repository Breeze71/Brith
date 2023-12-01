using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class ReadLv : MonoBehaviour
    {
        public class LvData
        {
            public int[] RoomNumber;
            public int[] OneRoomEnemyNumber;
            public int EndRoomEnemy;
            public int StartRoomSceneEntity;
            public int[] OneRoomSceneEntity;
        }
        [HideInInspector]
        public LvData lvData = new LvData();
        public static ReadLv Instance { get; set; }
        [SerializeField]
        private List<LvProfile> lvProfiles;
        #region Lvdata


        #endregion
        #region LV
        private CellTech cellTech;
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one ReadLv");
            }
            Instance = this;
            Debug.Log("read");
            cellTech = GameObject.FindGameObjectWithTag("CellTag").GetComponent<CellTech>();
            
            ReadLvData(GetCurrentLevel());

        }
        public int GetCurrentLevel()
        {
            return cellTech.currentLevel;
        }
        #endregion
        void ReadLvData(int level)
        {
            LvProfile lvProfile = lvProfiles[level];
            lvData.RoomNumber = lvProfile.RoomNumber;
            lvData.OneRoomEnemyNumber = lvProfile.OneRoomEnemyNumber;
            lvData.EndRoomEnemy = lvProfile.EndRoomEnemy;
            lvData.StartRoomSceneEntity = lvProfile.StartRoomSceneEntity;
            lvData.OneRoomSceneEntity = lvProfile.OneRoomSceneEntity;
        }
    }
}
