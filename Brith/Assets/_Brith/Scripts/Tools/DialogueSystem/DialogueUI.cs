using UnityEngine;
using TMPro;
using System;


public class DialogueUI : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject dialogueText;
    [SerializeField] private GameObject nameUI;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private GameObject portraitUI;
    [SerializeField] private GameObject choiceUI;


    private void Start() 
    {
        DialogueManager.Instance.OnDialogueStart += DialogueManager_OnDialogueStart;
        DialogueManager.Instance.OnDialogueClose += DialogueManager_OnDialogueClose;

        DialogueManager.Instance.OnCanContinueTrue += DialogueManager_OnCanContinueTrue;
        DialogueManager.Instance.OnCanContinueFalse += DialogueManager_OnCanContinueFalse;
    }

    private void DialogueManager_OnCanContinueTrue(object sender, EventArgs e)
    {
        continueIcon.SetActive(true);
    }
    private void DialogueManager_OnCanContinueFalse(object sender, EventArgs e)
    {
        continueIcon.SetActive(false);
    }


    private void DialogueManager_OnDialogueStart(object sender, EventArgs e)
    {
        dialoguePanel.SetActive(true);
        dialogueText.SetActive(true);
        nameUI.SetActive(true);
        continueIcon.SetActive(true);
        portraitUI.SetActive(true);
        choiceUI.SetActive(true);
    }
    private void DialogueManager_OnDialogueClose(object sender, EventArgs e)
    {
        dialoguePanel.SetActive(false);
        dialogueText.SetActive(false);
        nameUI.SetActive(false);
        continueIcon.SetActive(false);     
        portraitUI.SetActive(false);   
        choiceUI.SetActive(false);
    }

}
