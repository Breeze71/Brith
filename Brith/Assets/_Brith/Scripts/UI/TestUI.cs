using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace V
{
    public class TestUI : MonoBehaviour
    {
        [SerializeField] private Button lossButton;
        [SerializeField] private Button winButton;
        private CellTech cellTech;
        private void Awake() 
        {
            cellTech = GameObject.FindGameObjectWithTag("CellTag").GetComponent<CellTech>();
            
            lossButton.onClick.AddListener(() =>
            {
                Loader.LoadScene(Loader.Scene.LossDialogue.ToString());
            })  ;  

            winButton.onClick.AddListener(() =>
            {
                cellTech.GetTechPoint(TaskSystemManager.Instance.GetAllstarNum());

                cellTech.currentLevel += 1;

                if(cellTech.currentLevel <= 5)
                {
                    Loader.LoadScene(Loader.Scene.WinDialogue.ToString());
                }
                else if(cellTech.currentLevel > 5)
                {
                    Loader.LoadScene(Loader.Scene.ThankForPlayingScene.ToString());
                }
            });
        }
    }
}
