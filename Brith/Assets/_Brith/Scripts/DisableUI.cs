using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class DisableUI : MonoBehaviour
    {
        [SerializeField] private GameObject start;
        CanvasGroup canvasGroup;
        private void Awake()
        {
            canvasGroup = start.GetComponent<CanvasGroup>();
        }
        public void hide()
        {
            //вўВи
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
