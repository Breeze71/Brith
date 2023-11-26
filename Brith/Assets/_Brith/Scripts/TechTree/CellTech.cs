using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public enum TechType
    {
        Follow,
        MoveSpeed_1,
        MoveSpeed_2,
        HealthMax_1,
        HealthMax_2,
    }

    /// <summary>
    ///  The Technology Tree of Cell
    /// </summary>
    public class CellTech
    {
        public event Action OnUnlockedNewTech;
        private List<TechType> unlockTechList;


        public CellTech()
        {
            unlockTechList = new List<TechType>();
        }

        /// <summary>
        /// Unlock New Tech
        /// </summary>
        public void UnlockTech(TechType _techType)
        {
            if(IsTechUnlocked(_techType))
            {   
                Debug.Log("tech has already unlock");
                return;
            }

            unlockTechList.Add(_techType);
            Debug.Log("Unlock " + _techType);
            
            OnUnlockedNewTech?.Invoke(); // To - Do ④是否被解锁示意圈
        }

        /// <summary>
        /// Check if is Unlock
        /// </summary>
        public bool IsTechUnlocked(TechType _techType)
        {
            return unlockTechList.Contains(_techType);
        }
    }
}
