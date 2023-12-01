using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace V
{
    public class ThankForPlayingUI : MonoBehaviour
    {
        [SerializeField] private GameObject thanForPlayingUI;
        [SerializeField] private Button returnButton;
        [SerializeField] private Button mainMenuButton;

        private void Awake() 
        {
            returnButton.onClick.AddListener(() =>
            {
                thanForPlayingUI.SetActive(false);
            });

            mainMenuButton.onClick.AddListener(() =>
            {
                Loader.LoadScene(Loader.Scene.UI_EnterGame.ToString());
            });
        }
    }
}
