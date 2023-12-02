using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;


public class DialogueUI : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueText;
    [SerializeField] private GameObject dialoguePannel;

    [SerializeField] private Button skipButton;
    [SerializeField] private Button autoButton;


    private void Start() 
    {
        DialogueManager.Instance.OnDialogueStart += DialogueManager_OnDialogueStart;
        DialogueManager.Instance.OnDialogueClose += DialogueManager_OnDialogueClose;

        skipButton.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scene.RandomMapTest.ToString());
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
        dialoguePannel.SetActive(true);
    }
    private void DialogueManager_OnDialogueClose(object sender, EventArgs e)
    {
        dialogueText.SetActive(false); 
        dialoguePannel.SetActive(false);
    }

}
