using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class StartDialogue : MonoBehaviour
    {
    [SerializeField] private TextAsset inkJson;
    
    private void Start()
    {
        Invoke(nameof(PlayDialogue), 0.5f);
    }

    private void PlayDialogue()
    {
        if(DialogueManager.Instance.IsDialoguePlaying)  return;

        DialogueManager.Instance.StartDialogue(inkJson);
    }
    }
}
