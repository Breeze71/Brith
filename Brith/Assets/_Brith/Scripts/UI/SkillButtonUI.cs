using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace V
{
    public class SkillButtonUI : MonoBehaviour
    {
        public Button SkillButton;
        public Image IconMaskImage;
        public TextMeshProUGUI CooldownTimeText;
        public TextMeshProUGUI UseCountText;

        public float SkillCooldownTimerMax = 10; // 冷卻時間
        public float CurrentCooldownTimer;
        public bool IsCooldown = false;    // 是否在冷卻
        public int UseCount = 3; // 使用次數 
    }
}
