using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

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
        public SkillButtonUI ElementBurst;
        public SkillButtonUI SlowDown;
        public SkillButtonUI Endless;
        public SkillButtonUI SpringUp;

        public float enemySlowdownSpeed = 1f;   // 钟慢效应 敌方细胞的移速
        public float slowdownTimerMax = 3f; // 钟慢效应 时间

        private CellTech cellTech;
        

        #region Unity
        private void Awake() 
        {
            InitSkill(ElementBurst, SkillType.ElementBurst);
            InitSkill(SlowDown, SkillType.Slowdown);
            InitSkill(Endless, SkillType.Endless);
            InitSkill(SpringUp, SkillType.SpringUp);
            
        }
        private void Start() 
        {

            cellTech = GameObject.FindGameObjectWithTag("CellTag").GetComponent<CellTech>();  

            cellTech.OnUnlockedNewTech += CellTech_OnUnlockedNewTech; // add new tech
            
            Invoke(nameof(BindNextSceneButton), 0.1f);
        }

        private void BindNextSceneButton()
        {     


            foreach(TechType techType in cellTech.unlockTechList)
            {
                CellTech_OnUnlockedNewTech(techType);
            }       
        }

        private void Update() 
        {
            AbilityCooldown(ref ElementBurst.CurrentCooldownTimer, ElementBurst.SkillCooldownTimerMax, 
                ref ElementBurst.IsCooldown, ElementBurst.IconMaskImage, ElementBurst.CooldownTimeText);    

            AbilityCooldown(ref SlowDown.CurrentCooldownTimer, SlowDown.SkillCooldownTimerMax, 
                ref SlowDown.IsCooldown, SlowDown.IconMaskImage, SlowDown.CooldownTimeText);   

            AbilityCooldown(ref Endless.CurrentCooldownTimer, Endless.SkillCooldownTimerMax, 
                ref Endless.IsCooldown, Endless.IconMaskImage, Endless.CooldownTimeText);

            AbilityCooldown(ref SpringUp.CurrentCooldownTimer, SpringUp.SkillCooldownTimerMax, 
                ref SpringUp.IsCooldown, SpringUp.IconMaskImage, SpringUp.CooldownTimeText);   
        }
        
        private void OnDestroy() 
        {
            cellTech.OnUnlockedNewTech -= CellTech_OnUnlockedNewTech; // add new tech    
        }
        #endregion

        /// <summary>
        /// 技能解鎖
        /// </summary>
        private void CellTech_OnUnlockedNewTech(TechType type)
        {
            switch(type)
            {
                case TechType.ElementBurst:
                    Debug.Log("Unlock Burst");
                    ElementBurst.SkillButton.gameObject.SetActive(true);
                    break;
                case TechType.Slowdown:
                    Debug.Log("Slow Down");
                    SlowDown.SkillButton.gameObject.SetActive(true);
                    break;
                case TechType.Endless:
                    Debug.Log("Endless");
                    Endless.SkillButton.gameObject.SetActive(true);
                    break;
                case TechType.SpringUp:
                    Debug.Log("SpringUp");
                    SpringUp.SkillButton.gameObject.SetActive(true);
                    break;
            }
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

            _skillButtonUI.SkillButton.gameObject.SetActive(false); // 設置完按鈕後關閉          
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
