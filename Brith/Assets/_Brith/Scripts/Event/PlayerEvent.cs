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
        private int currentPlayerCell;  // 儲存當前細胞數

        public event Action OnSpawnCell;
        public void SpawnCellEvent()
        {
            OnSpawnCell?.Invoke();
            currentPlayerCell++;
        }
        
        public event Action OnPlayerLoss;
        public event Action OnCellDead;
        public void CellDeadEvent()
        {
            OnCellDead?.Invoke();
            currentPlayerCell--;

            if(currentPlayerCell == 0)
            {
                // Player Loss
                OnPlayerLoss?.Invoke();
                Loader.LoadScene(Loader.Scene.LossDialogue.ToString());
            }
        }
    }
}
