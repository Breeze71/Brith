using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    /// <summary>
    /// 處理細胞全死光跳轉輸
    /// </summary>
    public class PlayerEvent
    {
        private int currentPlayerCell = 0;  // 儲存當前細胞數

        public event Action OnSpawnCell;
        public void SpawnCellEvent()
        {
            Debug.LogError("Spawn Cell");
            OnSpawnCell?.Invoke();
            currentPlayerCell++;
        }
        
        public event Action OnPlayerLoss;
        public event Action OnCellDead;
        public void CellDeadEvent()
        {
            OnCellDead?.Invoke();
            currentPlayerCell--;
            Debug.LogError("Cell Dead");

            if(currentPlayerCell == 0)
            {
                // Player Loss
                OnPlayerLoss?.Invoke();
                Debug.LogError("PlayerLoss");
                Loader.LoadScene(Loader.Scene.LossDialogue.ToString());
            }
        }
    }
}
