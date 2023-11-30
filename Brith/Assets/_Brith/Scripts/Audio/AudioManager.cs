using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        public AudioType[] AudioTypes;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }
        void Start()
        {
            foreach (AudioType type in AudioTypes)
            {
                type.Source=gameObject.AddComponent<AudioSource>();
                type.Source.clip = type.Clip;
                type.Source.name = type.Name;
                type.Source.volume = type.Volume;
                type.Source.pitch = type.Pitch;
                type.Source.loop = type.Loop;

                if(type.MixerGroup != null)
                {
                    type.Source.outputAudioMixerGroup = type.MixerGroup;
                }
            }
        }
    }
}
