using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SliderC : MonoBehaviour
{
    public Slider sliderMusic;
    public Slider sliderEffect;
    public AudioMixer audioMixer;
    private void OnEnable()
    {
        sliderMusic.value = PlayerPrefs.GetFloat("Music",20);
        audioMixer.SetFloat("Music", sliderMusic.value);
        sliderEffect.value = PlayerPrefs.GetFloat("SoundEffect",20);
        audioMixer.SetFloat("SoundEffect", sliderEffect.value);
    }
}
