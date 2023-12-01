using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using V.Tool.SaveLoadSystem;

namespace V
{
    public enum TechType
    {
        None,

        // Cell Init Amount
        /// <summary>
        /// 等接口
        /// </summary>
        Init_1_Plus1,
        Init_2_Plus1,
        Init_3_Plus2,
        Init_4_Plus4,

        #region 數值 Finish
        // Cell Max Health
        Hp_1_Plus4,
        Hp_2_Plus10,
        
        // Cell Atk
        Atk_1_Plus2,
        Atk_2_Plus5,

        // Speed
        Spd_1_Plus10,
        Spd_2_Plus20,

        // Defense
        Def_1_Plus5,
        #endregion

        // Collect Element Amount
        #region Finish
        Element_1_Plus5,
        #endregion
        
        // Player Skill
        /// <summary>
        /// 開關 button 即可
        /// </summary>
        #region Player Skill Finish
        ElementBurst,
        Slowdown,
        Endless,
        SpringUp,
        #endregion

        // Cell State
        #region Finish
        CellChaseEnemy,
        CellChaseElement,
        #endregion
    }

    /// <summary>
    ///  The Technology Tree of Cell
    /// </summary>
    public class CellTech : MonoBehaviour, IDataPersistable
    {
        public event Action<TechType> OnUnlockedNewTech;
        public event Action<int> OnChangeCurrentTechPoint;

        public List<TechType> unlockTechList;
        public int currentLevel;


        public CellTech()
        {
            unlockTechList = new List<TechType>();
        }

        public void UnlockNewTech(TechType _techType)
        {
            if(IsTechUnlocked(_techType))
            {   
                Debug.Log("tech has already unlock");
                return;
            }

            unlockTechList.Add(_techType);
            Debug.Log("Unlock " + _techType);
            
            OnUnlockedNewTech?.Invoke(_techType); // To - Do ④是否被解锁示意圈
        }

        /// <summary>
        /// 判別是否能使用該科技技能
        /// </summary>
        public bool IsTechUnlocked(TechType _techType)
        {
            return unlockTechList.Contains(_techType);
        }

        /// <summary>
        /// 前置需求
        /// </summary>
        private TechType GetTechRequirement(TechType _techType)
        {
            switch(_techType)
            {
                // 1 
                case TechType.Hp_1_Plus4: return TechType.Init_1_Plus1;

                // 2 
                case TechType.Atk_1_Plus2: return TechType.Hp_1_Plus4;
                case TechType.CellChaseElement: return TechType.Hp_1_Plus4;
                case TechType.Spd_1_Plus10: return TechType.Hp_1_Plus4;

                // 3
                case TechType.Init_2_Plus1: return TechType.Atk_1_Plus2;
                case TechType.CellChaseEnemy: return TechType.Atk_1_Plus2;

                case TechType.Hp_2_Plus10: return TechType.CellChaseElement;

                case TechType.Slowdown: return TechType.Spd_1_Plus10;
                case TechType.Spd_2_Plus20: return TechType.Spd_1_Plus10;

                // 4
                case TechType.Atk_2_Plus5: return TechType.CellChaseEnemy;
                case TechType.Init_3_Plus2: return TechType.Spd_2_Plus20;
                case TechType.Endless: return TechType.Atk_2_Plus5;

                // 5
                case TechType.Def_1_Plus5: return TechType.Atk_2_Plus5;

                case TechType.Element_1_Plus5: return TechType.Spd_2_Plus20;
                case TechType.Init_4_Plus4: return TechType.Hp_2_Plus10;
                case TechType.SpringUp: return TechType.Init_4_Plus4;
            }

            return TechType.None;
        }

        /// <summary>
        /// UnlockNewTech
        /// </summary>
        public bool TryUnlockNewTech(TechType _techType)
        {
            TechType techTypeRequirement = GetTechRequirement(_techType);

            // 檢查是否滿足前置需求
            if(techTypeRequirement != TechType.None)
            {
                if(IsTechUnlocked(techTypeRequirement))
                {
                    return true;
                }

                else
                {
                    Debug.Log("Require" + techTypeRequirement);
                    return false;
                }
            }

            // 沒有前置需求
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 讀存檔時，解鎖全部已解鎖科技
        /// </summary>
        public void CheckUnlockSkill()
        {
            foreach(TechType techType in unlockTechList)
            {
                TryUnlockNewTech(techType);
                OnUnlockedNewTech?.Invoke(techType);
            }
        }

        public int currentTechPoint = 1;
        public int maxTechPoint = 1;

        /// <summary>
        /// 重置所有科技
        /// </summary>
        public void ResetTechPoint()
        {
            currentTechPoint = maxTechPoint;

            OnChangeCurrentTechPoint?.Invoke(currentTechPoint);
        }
        public void GetTechPoint(int _amount)
        {
            currentTechPoint += _amount;
            maxTechPoint += _amount;

            OnChangeCurrentTechPoint?.Invoke(currentTechPoint);
        }


        [ContextMenu("TestGetTechPoint5")]
        private void GetTechPoint()
        {
            GetTechPoint(5);
        }

        public void LoadData(GameData _gameData)
        {
            unlockTechList = _gameData.UnlockTechList;

            currentTechPoint = _gameData.CurrentTechPoint;

            maxTechPoint = _gameData.MaxTechPoint;
            currentLevel = _gameData.CurrentLevel;

            Invoke(nameof(CheckUnlockSkill), .1f);
        }

        public void SaveData(ref GameData _gameData)
        {
            _gameData.UnlockTechList = unlockTechList;

            _gameData.CurrentTechPoint = currentTechPoint;

            _gameData.MaxTechPoint = maxTechPoint; 

            _gameData.CurrentLevel = currentLevel;           
        }
    }
}
