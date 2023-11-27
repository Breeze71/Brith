using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace V
{
    public class SkillUI : MonoBehaviour
    {
        public SkillButtonUI Skill_1;

        private void Start() 
        {
            SetFillAmount(Skill_1.IconMaskImage, 0);

            Skill_1.CooldownTimeText.text = "";

            SetSkillButton(Skill_1);
        }
        private void Update() 
        {
            AbilityCooldown(ref Skill_1.CurrentCooldownTimer, Skill_1.SkillCooldownTimerMax, 
                ref Skill_1.IsCooldown, Skill_1.IconMaskImage, Skill_1.CooldownTimeText);    
        }


        private void SetSkillButton(SkillButtonUI _skillButtonUI)
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
                }
            }); 
        }

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
