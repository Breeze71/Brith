using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace V
{
    public class AudioType 
    {
        [HideInInspector]
        public AudioSource Source;
        public AudioClip Clip;
        public AudioMixerGroup MixerGroup;

        public string Name;
        public float Volume;
        public float Pitch;
        public bool Loop;
    }
}
