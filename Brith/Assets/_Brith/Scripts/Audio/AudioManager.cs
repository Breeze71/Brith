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
        public AudioSource SoundEffectaudio;
        public AudioSource Musicaudio;
        //public AudioType[] AudioTypes;
        public AudioClip[] audioClips;
        public Dictionary<string, AudioClip> Audios = new Dictionary<string, AudioClip>();
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
            foreach (AudioClip clip in audioClips)
            {
                Audios.Add(clip.name, clip);
            }
            #region 
            //foreach (AudioType type in AudioTypes)
            //{
            //    type.Source = gameObject.AddComponent<AudioSource>();
            //    type.Source.clip = type.Clip;
            //    type.Source.name = type.Name;
            //    type.Source.volume = type.Volume;
            //    type.Source.pitch = type.Pitch;
            //    type.Source.loop = type.Loop;
            //    type.Source.playOnAwake = type.playOnAwake;
            //    if (type.MixerGroup != null)
            //    {
            //        type.Source.outputAudioMixerGroup = type.MixerGroup;
            //    }
            //    Audios.Add(type.Name, type);
            //}
            #endregion
        }
        #region load Audio
        #endregion
        #region base void
        public void PlayOne(string name)
        {
            if (Audios.TryGetValue(name, out AudioClip audioType))
            {
                Debug.Log(audioType.name);
                SoundEffectaudio.PlayOneShot(audioType);

            }
        }
        //public void Pasuse(string name)
        //{
        //    if (Audios.TryGetValue(name, out AudioClip audioType))
        //    {
        //        audioType.Source.Pause();
        //    }
        //}
        //public void Stop(string name)
        //{
        //    if (Audios.TryGetValue(name, out AudioType audioType))
        //    {
        //        audioType.Source.Stop();
        //    }
        //}
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
        //public void HoverButton()
        //{
        //    StartP("hoverButton");
        //}
        //public void ClickButton()
        //{
        //    StartP("clickButton");
        //}
        public void PlayUIClickButton()
        {
            PlayOne("UIClickButton");
        }
        public void PlayUIHoverButton()
        {
            PlayOne("UIHoverButton");
        }
    }
}
