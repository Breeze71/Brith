using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace V.UI
{
    public class TechTreeButtonUI : MonoBehaviour
    {
        [SerializeField] private GameObject techPointNotEnoughPannel;

        #region Reset Tech
        [SerializeField] private GameObject resetTechPannel;
        [SerializeField] private Button resetTechPointButton;
        [SerializeField] private Button confirmResetButton;
        [SerializeField] private Button cancelResetButton;
        [SerializeField] private TextMeshProUGUI currentTechPointText;
        #endregion

        #region Tech Info
        [SerializeField] private GameObject techInfoPannel; 

        [SerializeField] private TextMeshProUGUI techCostAmountText;
        [SerializeField] private TextMeshProUGUI techNameText;
        [SerializeField] private TextMeshProUGUI techIntroText;
        [SerializeField] private Button comfirmTechButton;
        #endregion

        [SerializeField] private Button nextSceneButton;
        
        #region TechButtonUI
        [Header("TechButtonUI")]
        [Space(5)]
        [Header("Init Amount")]
        [SerializeField] private TechButtonUI init_1_Plus1;
        [SerializeField] private TechButtonUI init_2_Plus1;
        [SerializeField] private TechButtonUI init_3_Plus2;
        [SerializeField] private TechButtonUI init_4_Plus4;

        [Header("Hp")]
        [SerializeField] private TechButtonUI hp_1_Plus4;
        [SerializeField] private TechButtonUI hp_2_Plus10;
        
        [Header("Atk")]
        [SerializeField] private TechButtonUI atk_1_Plus2;
        [SerializeField] private TechButtonUI atk_2_Plus5;

        [Header("Speed")]
        [SerializeField] private TechButtonUI spd_1_Plus10;
        [SerializeField] private TechButtonUI spd_2_Plus20;

        [Header("Defense")]
        [SerializeField] private TechButtonUI def_1_Plus5;

        [Header("Element Collect Amount")]
        [SerializeField] private TechButtonUI element_1_Plus5;

        [Header("PlayerSkill")]
        [SerializeField] private TechButtonUI elementBurst;
        [SerializeField] private TechButtonUI slowdown;
        [SerializeField] private TechButtonUI endless;
        [SerializeField] private TechButtonUI springUp;

        [Header("Cell State")]
        [SerializeField] private TechButtonUI cellChaseEnemy;
        [SerializeField] private TechButtonUI cellChaseElement;
        #endregion

        [SerializeField] private GameObject TechCanvas;

        /// <summary>
        /// To - Do DataPersis
        /// </summary>
        [SerializeField]private List<TechButtonUI> unlocktechButtonUIList;
        private CellTech cellTech;

        private void Awake() 
        {
            #region SetButtonUnlockTech
            SetButtonUnlockTech(init_1_Plus1, TechType.Init_1_Plus1);
            SetButtonUnlockTech(init_2_Plus1, TechType.Init_2_Plus1);
            SetButtonUnlockTech(init_3_Plus2, TechType.Init_3_Plus2);
            SetButtonUnlockTech(init_4_Plus4, TechType.Init_4_Plus4);
            
            SetButtonUnlockTech(hp_1_Plus4, TechType.Hp_1_Plus4);
            SetButtonUnlockTech(hp_2_Plus10, TechType.Hp_2_Plus10);
            
            SetButtonUnlockTech(atk_1_Plus2, TechType.Atk_1_Plus2);
            SetButtonUnlockTech(atk_2_Plus5, TechType.Atk_2_Plus5);
            
            SetButtonUnlockTech(spd_1_Plus10, TechType.Spd_1_Plus10);
            SetButtonUnlockTech(spd_2_Plus20, TechType.Spd_2_Plus20);
            
            SetButtonUnlockTech(def_1_Plus5, TechType.Def_1_Plus5);
            SetButtonUnlockTech(element_1_Plus5, TechType.Element_1_Plus5);
            SetButtonUnlockTech(elementBurst, TechType.ElementBurst);
            SetButtonUnlockTech(slowdown, TechType.Slowdown);
            SetButtonUnlockTech(endless, TechType.Endless);
            SetButtonUnlockTech(springUp, TechType.SpringUp);
            
            SetButtonUnlockTech(cellChaseEnemy, TechType.CellChaseEnemy);
            SetButtonUnlockTech(cellChaseElement, TechType.CellChaseElement);
            #endregion

            cellTech = GameObject.FindGameObjectWithTag("CellTag").GetComponent<CellTech>(); 

            nextSceneButton.onClick.AddListener(() =>
            {   
                TechCanvas.SetActive(false);
            });
            
            resetTechPointButton.onClick.AddListener(() =>
            {
                resetTechPannel.SetActive(true);
                techInfoPannel.SetActive(false);
            });

            cancelResetButton.onClick.AddListener(() =>
            {
                resetTechPannel.SetActive(false);
            });

            confirmResetButton.onClick.AddListener(() =>
            {
                cellTech.ResetTechPoint();

                foreach(TechButtonUI _techButtonUI in unlocktechButtonUIList)
                {
                    _techButtonUI.ButtonMask.gameObject.SetActive(true);
                }

                resetTechPannel.SetActive(false);
            });

        }

        private void Start() 
        {
            resetTechPannel.SetActive(false);
            techPointNotEnoughPannel.SetActive(false);
            techInfoPannel.SetActive(false);
            
            currentTechPointText.text = cellTech.currentTechPoint.ToString();

            cellTech.OnChangeCurrentTechPoint += CellTech_OnChangeCurrentTechPoint;

            // TechCanvas.SetActive(false);
        }

        /// <summary>
        /// 更新 當前點數
        /// </summary>
        /// <param name="obj"></param>
        private void CellTech_OnChangeCurrentTechPoint(int obj)
        {
            currentTechPointText.text = obj.ToString();
        }

        private void SetButtonUnlockTech(TechButtonUI _techButtonUI, TechType _unlockTechType)
        {
            // To - Do PointerEnter 顯示細節 

            _techButtonUI.ButtonMask.onClick.AddListener(() =>
            {
                techInfoPannel.SetActive(true);
                techIntroText.text = _techButtonUI.techIntroText;
                techNameText.text = _techButtonUI.techName;
                techCostAmountText.text = _techButtonUI.TechCount.ToString();


                // Comfirm Unlock Tech
                comfirmTechButton.onClick.AddListener(() =>
                {
                    if(cellTech.TryUnlockNewTech(_unlockTechType))
                    {
                        if(cellTech.currentTechPoint < _techButtonUI.TechCount)
                        {
                            StartCoroutine(TechPointNotEnoughPannel());
                            Debug.Log("TechPoint Not Enough");
                            return;
                        }

                        _techButtonUI.ButtonMask.gameObject.SetActive(false);
                        cellTech.currentTechPoint -= _techButtonUI.TechCount;
                        currentTechPointText.text = cellTech.currentTechPoint.ToString();
                        cellTech.UnlockNewTech(_unlockTechType);
                        unlocktechButtonUIList.Add(_techButtonUI);

                        techInfoPannel.SetActive(false);
                    }
                    else
                    {
                        // To - do
                        // ui  顯示需先滿足前置需求

                    }
                });
            }); 
        }

        private IEnumerator TechPointNotEnoughPannel()
        {
            techPointNotEnoughPannel.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            techPointNotEnoughPannel.SetActive(false);
        }
    }
}
