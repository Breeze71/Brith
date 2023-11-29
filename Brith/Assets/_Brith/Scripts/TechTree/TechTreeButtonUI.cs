using UnityEngine;
using UnityEngine.UI;

namespace V.UI
{
    public class TechTreeButtonUI : MonoBehaviour
    {
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

        [Header("PlayerSkill")]
        [SerializeField] private TechButtonUI elementBurst;
        [SerializeField] private TechButtonUI slowdown;
        [SerializeField] private TechButtonUI endless;
        [SerializeField] private TechButtonUI springUp;

        [Header("Cell State")]
        [SerializeField] private TechButtonUI cellChaseEnemy;
        [SerializeField] private TechButtonUI cellChaseElement;
        #endregion

        private CellTech cellTech;

        private void Awake() 
        {
            // SetButtonUnlockTech(followButton, TechType.Follow);
            // SetButtonUnlockTech(hp_1Button, TechType.HealthMax_1);
            // SetButtonUnlockTech(hp_2Button, TechType.HealthMax_2);
            // SetButtonUnlockTech(sp_1Button, TechType.MoveSpeed_1);
            // SetButtonUnlockTech(sp_2Button, TechType.MoveSpeed_2);

            cellTech = GameObject.FindGameObjectWithTag("CellTag").GetComponent<CellTech>(); 

            nextSceneButton.onClick.AddListener(() =>
            {   
                Loader.LoadScene(Loader.Scene.RandomMapTest);

                PlayerPrefs.SetInt("Current Level", PlayerPrefs.GetInt("Current Level", 0) + 1);
                PlayerPrefs.Save();

                if(PlayerPrefs.GetInt("Current Level") > 6)
                {
                    // To - Do Ending
                    Debug.LogWarning("Ending Scene");

                    PlayerPrefs.SetInt("Current Level", 0);
                }

                Debug.LogError("Current Level" + PlayerPrefs.GetInt("Current Level"));
            });
        }

        private void SetButtonUnlockTech(TechButtonUI _techButtonUI, TechType _unlockTechType)
        {
            // To - Do PointerEnter 顯示細節 

            _techButtonUI.ButtonMask.onClick.AddListener(() =>
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
