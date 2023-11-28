using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace V
{
    public class CellInfoUI : MonoBehaviour
    {
        [SerializeField] private GameObject cellInfoPannel;

        [SerializeField] private Image cellImg;
        [SerializeField] private List<Image> gearImgList;

        [SerializeField] private List<TextMeshProUGUI> propertyInfoTextList;
        [SerializeField] private List<TextMeshProUGUI> elementsAmountTextList;

        private void Awake() 
        {

            // Hide();
        }

        private void Show()
        {
            cellInfoPannel.SetActive(true);
        }

        private void Hide()
        {
            cellInfoPannel.SetActive(false);  
            Debug.Log("false");      
        }
    }
}
