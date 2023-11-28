using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace V.UI
{
    public class TechTreeUI : MonoBehaviour
    {
        [SerializeField] private Material techUnlockMaterial;
        [SerializeField] private Material techUnlockableMaterial;
        [SerializeField] private Button nextSceneButton;
        
        #region Button
        [Header("Button")]
        [SerializeField] private Button followButton;
        [SerializeField] private Button hp_1Button;
        [SerializeField] private Button hp_2Button;
        [SerializeField] private Button sp_1Button;
        [SerializeField] private Button sp_2Button;
        #endregion

        private CellTech cellTech;

        private void Awake() 
        {
            SetButtonUnlockTech(followButton, TechType.Follow);
            SetButtonUnlockTech(hp_1Button, TechType.HealthMax_1);
            SetButtonUnlockTech(hp_2Button, TechType.HealthMax_2);
            SetButtonUnlockTech(sp_1Button, TechType.MoveSpeed_1);
            SetButtonUnlockTech(sp_2Button, TechType.MoveSpeed_2);
            Debug.Log("setbutton");

            nextSceneButton.onClick.AddListener(() =>
            {   
                Loader.LoadScene(Loader.Scene.RandomMapTest);
            });

            cellTech = GameObject.FindGameObjectWithTag("CellTag").GetComponent<CellTech>(); 
        }

        private void SetButtonUnlockTech(Button _button, TechType _unlockTechType)
        {
            // To - Do PointerEnter 顯示細節 

            _button.onClick.AddListener(() =>
            {
                if(!cellTech.TryUnlockNewTech(_unlockTechType))
                {
                    // To - do
                    // ui  顯示需先滿足前置需求
                }
            }); 
        }

    }
}
