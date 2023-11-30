using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueAudioSO", menuName = "DialogueSystem/DialogueAudioSO", order = 1)]
public class DialogueAudioSO : ScriptableObject
{
    public string id;
    public AudioClip[] dialogueAudioList;

    public bool isStopSoundOrNot;
    [Range(1, 5)] public int frequencyLevel = 2;
    [Range(-3, 3)] public float minPitch = .5f;
    [Range(-3, 3)] public float maxPitch = 2f;
}
