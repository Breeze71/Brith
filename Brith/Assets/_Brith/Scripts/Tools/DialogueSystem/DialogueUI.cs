using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;


public class DialogueUI : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueText;

    [SerializeField] private Button skipButton;
    [SerializeField] private Button autoButton;


    private void Start() 
    {
        DialogueManager.Instance.OnDialogueStart += DialogueManager_OnDialogueStart;
        DialogueManager.Instance.OnDialogueClose += DialogueManager_OnDialogueClose;

        skipButton.onClick.AddListener(() =>
        {
            Loader.LoadNextScene();
        });

        autoButton.onClick.AddListener(() =>
        {
            if(DialogueManager.Instance.isAuto)
            {
                DialogueManager.Instance.isAuto = false;
            }
            else
            {
                DialogueManager.Instance.isAuto = true;
            }
        });
    }



    private void DialogueManager_OnDialogueStart(object sender, EventArgs e)
    {
        dialogueText.SetActive(true);
    }
    private void DialogueManager_OnDialogueClose(object sender, EventArgs e)
    {
        dialogueText.SetActive(false); 
    }

}
