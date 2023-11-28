using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace V
{
    public enum SkillType
    {
        /// <summary>
        /// 元素迸发
        /// </summary>
        ElementBurst,
        /// <summary>
        /// 钟慢效应
        /// </summary>
        Slowdown,
        /// <summary>
        /// 生生不息
        /// </summary>
        Endless,
        /// <summary>
        /// 生命涌现
        /// </summary>
        SpringUp
    }

    public class SkillUI : MonoBehaviour
    {
        public SkillButtonUI Skill_1;
        public SkillButtonUI Skill_2;
        public SkillButtonUI Skill_3;
        public SkillButtonUI Skill_4;

        public float enemySlowdownSpeed = 1f;   // 钟慢效应 敌方细胞的移速
        public float slowdownTimerMax = 3f; // 钟慢效应 时间


        private void Start() 
        {
            InitSkill(Skill_1, SkillType.ElementBurst);
            InitSkill(Skill_2, SkillType.Slowdown);
            InitSkill(Skill_3, SkillType.Endless);
            InitSkill(Skill_4, SkillType.SpringUp);
        }

        private void Update() 
        {
            AbilityCooldown(ref Skill_1.CurrentCooldownTimer, Skill_1.SkillCooldownTimerMax, 
                ref Skill_1.IsCooldown, Skill_1.IconMaskImage, Skill_1.CooldownTimeText);    

            AbilityCooldown(ref Skill_2.CurrentCooldownTimer, Skill_2.SkillCooldownTimerMax, 
                ref Skill_2.IsCooldown, Skill_2.IconMaskImage, Skill_2.CooldownTimeText);   

            AbilityCooldown(ref Skill_3.CurrentCooldownTimer, Skill_3.SkillCooldownTimerMax, 
                ref Skill_3.IsCooldown, Skill_3.IconMaskImage, Skill_3.CooldownTimeText);

            AbilityCooldown(ref Skill_4.CurrentCooldownTimer, Skill_4.SkillCooldownTimerMax, 
                ref Skill_4.IsCooldown, Skill_4.IconMaskImage, Skill_4.CooldownTimeText);   
        }

        /// <summary>
        /// 按鈕按下邏輯
        /// </summary>
        /// <param name="_skillButtonUI"></param>
        private void SetSkillButton(SkillButtonUI _skillButtonUI, SkillType _skillType)
        {
            _skillButtonUI.SkillButton.onClick.AddListener(() =>
            {
                // 處於非冷卻時間
                if(!_skillButtonUI.IsCooldown && _skillButtonUI.UseCount > 0)
                {
                    _skillButtonUI.IsCooldown = true;  // 進入冷卻
                    _skillButtonUI.CurrentCooldownTimer = _skillButtonUI.SkillCooldownTimerMax;// 冷卻時間
                    _skillButtonUI.UseCount--;
                    _skillButtonUI.UseCountText.text = "x " + _skillButtonUI.UseCount;

                    Debug.Log("use skill");
                    switch(_skillType)
                    {
                        case SkillType.ElementBurst:
                            GameEventManager.Instance.SkillEvent.ElementBurstSkillEvent();
                            break;
                        case SkillType.Slowdown:
                            GameEventManager.Instance.SkillEvent.SlowdownSkillEvent();
                            break;
                        case SkillType.Endless:
                            GameEventManager.Instance.SkillEvent.EndlessSkillEvent();
                            break;
                        case SkillType.SpringUp:
                            GameEventManager.Instance.SkillEvent.SpringUpEvent();
                            break;
                    }
                }

                if(_skillButtonUI.UseCount <= 0)
                {
                    _skillButtonUI.SkillButton.enabled = false;
                }
                else
                {
                    _skillButtonUI.SkillButton.enabled = true;
                }
            }); 
        }

        /// <summary>
        /// 初始化按鈕 
        /// </summary>
        /// <param name="_skillButtonUI"></param>
        private void InitSkill(SkillButtonUI _skillButtonUI, SkillType _skillType)
        {
            SetFillAmount(_skillButtonUI.IconMaskImage, 0);

            _skillButtonUI.CooldownTimeText.text = "";

            SetSkillButton(_skillButtonUI, _skillType);            
        }

        /// <summary>
        /// 技能冷卻
        /// </summary>
        private void AbilityCooldown(ref float _curentCooldownTimer, float _cooldownTimerMax, 
            ref bool _isCoolDown, Image _skillMask, TextMeshProUGUI _cooldownTimeText)
        {
            if(!_isCoolDown)
            {
                return;
            }

            _curentCooldownTimer -= Time.deltaTime;

            if (_curentCooldownTimer <= 0)
            {
                _isCoolDown = false;
                _curentCooldownTimer = 0f;

                if(_cooldownTimeText != null)
                {
                    _cooldownTimeText.text = "";
                }
                if(_skillMask != null)
                {
                    SetFillAmount(_skillMask, 0);
                }
            }

            else
            {
                if(_cooldownTimeText != null)
                {
                    _cooldownTimeText.text = Mathf.Ceil(_curentCooldownTimer).ToString();
                }
                if(_skillMask != null)
                {
                    SetFillAmount(_skillMask, _curentCooldownTimer / _cooldownTimerMax);
                }
            }
        }

        private void SetFillAmount(Image _image, float _amount)
        {
            _image.fillAmount = _amount;

            if(_amount == 0)
            {
                _image.gameObject.SetActive(false);
            }
            else
            {
                _image.gameObject.SetActive(true);
            }
        }
    }
}
