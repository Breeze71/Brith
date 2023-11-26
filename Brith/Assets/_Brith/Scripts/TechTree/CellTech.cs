using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public enum TechType
    {
        None,
        Follow,
        MoveSpeed_1,
        MoveSpeed_2,
        HealthMax_1,
        HealthMax_2,
    }

    /// <summary>
    ///  The Technology Tree of Cell
    /// </summary>
    public class CellTech : MonoBehaviour
    {
        public event Action<TechType> OnUnlockedNewTech;
        public List<TechType> unlockTechList;


        public CellTech()
        {
            unlockTechList = new List<TechType>();
        }

        private void UnlockNewTech(TechType _techType)
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
        /// Check if is Unlock
        /// </summary>
        public bool IsTechUnlocked(TechType _techType)
        {
            return unlockTechList.Contains(_techType);
        }

        /// <summary>
        /// 前置需求
        /// </summary>
        public TechType GetTechRequirement(TechType _techType)
        {
            switch(_techType)
            {
                case TechType.MoveSpeed_2: 
                    return TechType.MoveSpeed_1;

                case TechType.HealthMax_2: 
                    return TechType.HealthMax_1;
            }

            return TechType.None;
        }

        /// <summary>
        /// UnlockNewTech
        /// </summary>
        public bool TryUnlockNewTech(TechType _techType)
        {
            TechType techTypeRequirement = GetTechRequirement(_techType);

            if(techTypeRequirement != TechType.None)
            {
                // 檢查是否滿足前置需求
                if(IsTechUnlocked(techTypeRequirement))
                {
                    UnlockNewTech(_techType);

                    return true;
                }

                else
                {
                    return false;
                }
            }

            // 沒有前置需求
            else
            {
                UnlockNewTech(_techType);

                return true;
            }
        }

        public void CheckUnlockSkill()
        {
            foreach(TechType techType in unlockTechList)
            {
                TryUnlockNewTech(techType);
            }
        }
    }
}
