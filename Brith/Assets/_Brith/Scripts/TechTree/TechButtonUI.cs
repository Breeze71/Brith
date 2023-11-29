using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace V
{
    public class TechButtonUI : MonoBehaviour
    {
        /// <summary>
        /// 點擊可解鎖
        /// </summary>
        public Button ButtonMask;

        /// <summary>
        /// 科技圖片
        /// </summary>
        public GameObject TechImg;

        /// <summary>
        /// 花費
        /// </summary>
        public int TechCount;
        /// <summary>
        /// 特性名
        /// </summary>
        public string techName;
        /// <summary>
        /// 介紹文字
        /// </summary>
        public string techIntroText;
    }
}
