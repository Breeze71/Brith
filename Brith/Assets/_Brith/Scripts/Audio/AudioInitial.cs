using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioInitial : MonoBehaviour
{
    public AudioMixer audioMixer;
    private void Start()
    {
        audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("Music", 20));
        audioMixer.SetFloat("SoundEffect", PlayerPrefs.GetFloat("SoundEffect", 20));
    }
}

