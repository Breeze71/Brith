using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class FoldExpandUI : MonoBehaviour
    {
        [SerializeField] private GameObject TaskUI;
        [SerializeField] private GameObject Expand;
        [SerializeField] private GameObject Fold;
        CanvasGroup canvasGroup;
        private void Awake()
        {
            canvasGroup = TaskUI.GetComponent<CanvasGroup>();
        }
        public void expand()
        {
            Fold.SetActive(true);
            display();
            Expand.SetActive(false);
        }
        public void fold()
        {

            hide();
            Expand.SetActive(true);
            Fold.SetActive(false);
        }
        void hide()
        {
            //Òþ²Ø
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        void display()
        {
            //ÏÔÊ¾
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}
