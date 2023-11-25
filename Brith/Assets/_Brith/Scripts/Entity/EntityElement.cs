using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    /// <summary>
    /// 儲存各元素數量
    /// </summary>
    public class EntityElement
    {
        public int GroundElement;

        public int FireElement;
        public int WindElement;
        public int WaterElement;

        public int GetTotalElementAmount()
        {
            return GroundElement + FireElement + WindElement + WaterElement;
        }

        /// <summary>
        /// 生成新 Entity 時，減少 50 
        /// </summary>
        public void DecreaseElement()
        {
            GroundElement = 0;
            FireElement = 0;
            WindElement = 0;
            WaterElement = 0;
        }
    }
}
