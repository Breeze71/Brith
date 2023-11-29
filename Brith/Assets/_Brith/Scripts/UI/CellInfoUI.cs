using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace V
{
    public class CellInfoUI : MonoBehaviour
    {
        [SerializeField] private LayerMask canCheckInfoLayer;
        [SerializeField] private GameObject cellInfoPannel;

        [SerializeField] private Image cellImg;
        [SerializeField] private List<Image> gearImgList;

        [SerializeField] private List<TextMeshProUGUI> propertyInfoTextList;
        [SerializeField] private List<TextMeshProUGUI> elementsAmountTextList;

        [SerializeField] private Button closeButton;
        private EntityBase entityBase;

        private void Awake() 
        {
            Hide();

            closeButton.onClick.AddListener(() =>
            {
                Hide();
            });
        }

        private void Update() 
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, canCheckInfoLayer);
            if (hit.collider != null)
            {
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Show();

                    entityBase = hit.collider.GetComponent<EntityBase>();
                }
            }

            // 時時更新
            if(entityBase != null)
            {
                GetCurrentCellGearInfo(entityBase);
            }
        }

        private void GetCurrentCellGearInfo(EntityBase _entityBase)
        {
            // cell img
            cellImg = _entityBase.CellImg;
            // Gear
            foreach(ElementType _element in _entityBase.GetGears())
            {
                switch(_element)
                {
                    case ElementType.Fire:
                        gearImgList[0].enabled = true;
                        break;
                    case ElementType.Wind:
                        gearImgList[1].enabled = true;
                        break;
                    case ElementType.Ground:
                        gearImgList[2].enabled = true;
                        break;
                    case ElementType.Water:
                        gearImgList[3].enabled = true;
                        break;                                            
                }
            }
        
            // property
            propertyInfoTextList[0].text = _entityBase.Attack.ToString();
            propertyInfoTextList[1].text = _entityBase.Defense.ToString();
            propertyInfoTextList[2].text = _entityBase.HealthSystem.GetHealthAmount().ToString();
            propertyInfoTextList[3].text = _entityBase.Speed.ToString();

            //element amount
            elementsAmountTextList[0].text = _entityBase.EntityElement.FireElement.ToString();
            elementsAmountTextList[1].text = _entityBase.EntityElement.WindElement.ToString();
            elementsAmountTextList[2].text = _entityBase.EntityElement.GroundElement.ToString();
            elementsAmountTextList[3].text = _entityBase.EntityElement.WaterElement.ToString();            
        }

        private void Show()
        {
            cellInfoPannel.SetActive(true);
        }

        private void Hide()
        {
            foreach(Image _img in gearImgList)
            {
                _img.enabled = false;
            }

            foreach(TextMeshProUGUI _propertyInfoText in propertyInfoTextList)
            {
                _propertyInfoText.text = "";
            }

            foreach(TextMeshProUGUI _elementsAmountText in elementsAmountTextList)
            {
                _elementsAmountText.text = "";
            }

            cellInfoPannel.SetActive(false);  

            Debug.Log("false");      
        }
    }
}
