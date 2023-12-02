using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class StartTutorial : MonoBehaviour
    {
        [SerializeField] private TextAsset inkJson; 
        [SerializeField] private GameObject DialoguePannel;  
        private CellTech cellTech; 

        private void Awake() 
        {
            cellTech = GameObject.FindGameObjectWithTag("CellTag").GetComponent<CellTech>(); 
        }
        // private void Start() 
        // {
        //     Invoke(nameof(SetActivePannel), 0.1f);

        //     DialogueManager.Instance.OnDialogueClose += DialogueManager_OnDialogueClose ;
        // }

        // private void DialogueManager_OnDialogueClose(object sender, EventArgs e)
        // {
        //     if(cellTech.currentLevel == 1)
        //     {
        //         NewRoommanagerOnGame.Instance.CreateNewRoom();
        //     }
        // }

        private void SetActivePannel()
        {
            DialoguePannel.SetActive(false);
        }

        public void PlayDialogue()
        {
            if(DialogueManager.Instance.IsDialoguePlaying)  return;

            DialogueManager.Instance.StartDialogue(inkJson);
        }
    }
}
