using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace V
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] AudioMixer audioMixer;
        public static AudioManager instance;
        public AudioType[] AudioTypes;
        public Dictionary<string, AudioType> Audios = new Dictionary<string, AudioType>();
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
            foreach (AudioType type in AudioTypes)
            {
                type.Source = gameObject.AddComponent<AudioSource>();
                type.Source.clip = type.Clip;
                type.Source.name = type.Name;
                type.Source.volume = type.Volume;
                type.Source.pitch = type.Pitch;
                type.Source.loop = type.Loop;
                type.Source.playOnAwake = type.playOnAwake;
                if (type.MixerGroup != null)
                {
                    type.Source.outputAudioMixerGroup = type.MixerGroup;
                }
                Audios.Add(type.Name, type);
            }
        }
        #region load Audio
        void Start()
        {
           
        }
        #endregion
        #region base void
        public void StartP(string name)
        {
            if (Audios.TryGetValue(name, out AudioType audioType))
            {
                audioType.Source.Play();
                Debug.Log(audioType.Source.clip.name);
            }
        }
        public void Pasuse(string name)
        {
            if (Audios.TryGetValue(name, out AudioType audioType))
            {
                audioType.Source.Pause();
            }
        }
        public void Stop(string name)
        {
            if (Audios.TryGetValue(name, out AudioType audioType))
            {
                audioType.Source.Stop();
            }
        }
        #endregion
        #region UI option
        public void MusicSlider(Slider slider)
        {
            audioMixer.SetFloat("Music", slider.value);
        }
        public void SoundEffectSlider(Slider slider)
        {
            audioMixer.SetFloat("SoundEffect", slider.value);
        }
        #endregion
        public void HoverButton()
        {
            StartP("hoverButton");
        }
        public void ClickButton()
        {
            StartP("clickButton");
        }
    }
}
