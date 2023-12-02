using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class StartTutorial : MonoBehaviour
    {
        [SerializeField] private TextAsset inkJson; 
        [SerializeField] private GameObject DialoguePannel;   

        private void Start() 
        {
            Invoke(nameof(SetActivePannel), 0.1f);
        }

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
