using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class Menu : MonoBehaviour
    {

        [SerializeField] private GameObject UI;
        CanvasGroup canvasGroup;
        bool awake = false;
        private void Awake()
        {
            canvasGroup = UI.GetComponent<CanvasGroup>();
            fold();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(!awake)
                {
                    expand();
                    awake = true;
                }
                else
                {
                    fold();
                    awake = false;
                }
            }
        }
        public void expand()
        {
            display();
        }
        public void fold()
        {

            hide();
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
