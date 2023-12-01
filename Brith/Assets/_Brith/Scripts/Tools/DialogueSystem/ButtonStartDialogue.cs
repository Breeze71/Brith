using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace V
{
    public class ButtonStartDialogue : MonoBehaviour
    {
        [SerializeField] private TextAsset inkJson;
        [SerializeField] private GameObject pannelUI;
        [SerializeField] private GameObject dialogueUI;
        [SerializeField] private Button button;
        [SerializeField] private Button ReturnToMainMenubutton;
        
        private void Start()
        {
            button.onClick.AddListener(() =>
            {
                pannelUI.SetActive(false);
                dialogueUI.SetActive(true);

                PlayDialogue();
            });

            ReturnToMainMenubutton.onClick.AddListener(() =>
            {
                Loader.LoadScene(Loader.Scene.UI_EnterGame.ToString());
            });

            dialogueUI.SetActive(false);
        }

    
        private void PlayDialogue()
        {
            if(DialogueManager.Instance.IsDialoguePlaying)  return;

            DialogueManager.Instance.StartDialogue(inkJson);
        }
    }
}
