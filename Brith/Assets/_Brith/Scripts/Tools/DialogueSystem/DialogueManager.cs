using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance{get; private set;}

    #region Ink Tag
    private const string Speaker_Tag = "speaker";
    private const string Portrait_Tag = "portrait";
    private const string Layout_Tag = "layout";
    private const string Audio_Tag = "audio";
    #endregion

    #region Ink External
    private const string PlayEmote = "PlayEmote";
    private const string LoadScene = "LoadScene";
    #endregion

    #region Event
    public event EventHandler OnDialogueStart;
    public event EventHandler OnDialogueClose;

    public event EventHandler OnCanContinueTrue;
    public event EventHandler OnCanContinueFalse;
    #endregion

    [Header("Dialogue")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private float typeDelay = .05f;
    
    public bool IsDialoguePlaying{get; private set;}

    private Story currentStory;
    private bool canContinue = false;
    private bool canSkip = false;

    private Coroutine canSkipCorutine;
    private Coroutine typeEffectCoroutine;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choiceList;
    private TextMeshProUGUI[] choicesTextList;

    [Header("Animator")]
    [SerializeField] private Animator portraitAnim;
    [SerializeField] private Animator emoteAnim;

    [Header("Audio")]
    [SerializeField] private bool makePredictable;
    [SerializeField] private DialogueAudioSO defaultAudioSO;
    [SerializeField] private DialogueAudioSO[] audioSOList;
    private DialogueAudioSO currentAudioSO;
    private AudioSource audioSource;

    private Dictionary<string, DialogueAudioSO> audioSODictionary;


    #region Unity Fuction
    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }    

        audioSource = gameObject.AddComponent<AudioSource>();
        currentAudioSO = defaultAudioSO;
        InitAudioSODictionary();
    } 

    private void InitAudioSODictionary() 
    {
        audioSODictionary = new Dictionary<string, DialogueAudioSO> {{ defaultAudioSO.id, defaultAudioSO }};
        foreach(DialogueAudioSO _audioSO in audioSOList)
        {
            audioSODictionary.Add(_audioSO.id, _audioSO);
        }
    }

    private void OnEnable()
    {
        OnDialogueClose?.Invoke(this, EventArgs.Empty); // DialoguePanel
        IsDialoguePlaying = false;

        // 生成相應數量的選項   
        choicesTextList = new TextMeshProUGUI[choiceList.Length];  // 生成和　choice 一樣多的　text
        int _index = 0;
        foreach(GameObject choice in choiceList)
        {
            choicesTextList[_index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            _index++;
        }     
    }

    private void Update()
    {
        if(!IsDialoguePlaying)  return;

        // 避免選選項時，會一直刷新
        if(currentStory.currentChoices.Count == 0 && canContinue && Input.GetKeyDown(KeyCode.E))
        {
            ContinueStory();
        }
    }
    #endregion

    #region  ExternalFuction
    private void SetUpExternalFuction(Animator _emoteAnim)
    {
        currentStory.BindExternalFunction(PlayEmote, (string _emoteName) =>
        {
            if (_emoteAnim != null)
            {
                _emoteAnim.Play(_emoteName);
            }
            else
            {
                Debug.LogError("Emote Anim Error" + _emoteName);
            }
        });     

        currentStory.BindExternalFunction(LoadScene, ()=>
        {
            //Loader.LoadNextScene();
        });
    }
    #endregion

    #region Dialogue
    public void StartDialogue(TextAsset _inkJson, Animator _emoteAnim)
    {
        currentStory = new Story(_inkJson.text);
        
        IsDialoguePlaying = true;
        OnDialogueStart?.Invoke(this, EventArgs.Empty); // DialoguePanel

        //SetUpExternalFuction(_emoteAnim);   // External Fuc

        // reset dialogue
        nameText.text = "";
        //portraitAnim.Play("Default");
        //layoutAnim.Play("Right");


        ContinueStory();
    }

    // 一句一句
    private void ContinueStory()
    {
        if(currentStory.canContinue)
        {   
            if(canSkipCorutine != null)
            {
                StopCoroutine(canSkipCorutine);
            }
            canSkipCorutine = StartCoroutine(Coroutine_CanSkipDialogue());


            if(typeEffectCoroutine != null)
            {
                StopCoroutine(typeEffectCoroutine);
            }
            string _nextLine = currentStory.Continue(); // 先讀下一行才能讀新 Audio

            // if the last line is External Fuction
            if(_nextLine.Equals("") && !currentStory.canContinue)
            {
                CloseDialogue();
            }
            else
            {
                HandleInkTags(currentStory.currentTags);
                typeEffectCoroutine = StartCoroutine(Coroutine_TypeEffect(_nextLine)); // 有 Audio 才播放
            }
        }
        else
        {
            CloseDialogue();
        }
    }

    private IEnumerator Coroutine_TypeEffect(string _line)
    {
        WaitForSeconds _typeDelay = new WaitForSeconds(typeDelay);

        HideChoice();
        canContinue = false;
        dialogueText.text = _line;
        dialogueText.maxVisibleCharacters = 0;

        OnCanContinueFalse?.Invoke(this, EventArgs.Empty);
        
        bool _isAddingRichText = false;

        foreach(char _letter in _line.ToCharArray())
        {
            // Skip Dialogue(避免同一禎同時開始對話及跳過)
            if(Input.GetKeyDown(KeyCode.E) && canSkip)
            {
                Debug.Log("Skip");
                dialogueText.maxVisibleCharacters = _line.Length;
                break;
            }
            
            // 是否有 RichText(若有，則先不 TypeEffect)
            if(_letter == '<' || _isAddingRichText)
            {
                _isAddingRichText = true;

                if(_letter == '>')
                {
                    _isAddingRichText = false;
                }
            }
            else
            {
                PlayDialogueSound(dialogueText.maxVisibleCharacters, dialogueText.text[dialogueText.maxVisibleCharacters]); // 最大可見 index = 目前文字 index

                dialogueText.maxVisibleCharacters++;    // 隨 type 增加 visiable

                yield return _typeDelay;
            }

        }

        DisplayChoices();
        canContinue = true;
        OnCanContinueTrue?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator Coroutine_CanSkipDialogue()
    {
        canSkip = false;
        yield return new WaitForEndOfFrame();
        canSkip = true;
    }

    public void CloseDialogue()
    {
        OnDialogueClose?.Invoke(this, EventArgs.Empty);

        //currentStory.UnbindExternalFunction(PlayEmote);   ExternalFuc

        IsDialoguePlaying = false;
        dialogueText.text = ""; // 清空
        SetCurrentAudioSO(defaultAudioSO.id);
    }
    #endregion

    #region InkTag (處理不同 portrait, layout)
    private void HandleInkTags(List<string> _currentTags)
    {
        foreach(string _tag in _currentTags)
        {
            string[] _splitTag = _tag.Split(':');    // 用 : 分隔成 Key - Value pair

            if(_splitTag.Length != 2)   Debug.LogError("Tag Error" + _tag); // debug

            string _tagKey = _splitTag[0].Trim();   // Trim() 忽視空格
            string _tagValue = _splitTag[1].Trim();

            TagState(_tagKey, _tagValue);
        }
    }
    private void TagState(string _tagKey, string _tagValue)
    {
        switch(_tagKey)
        {
            case Speaker_Tag:
                nameText.text = _tagValue;
                break;
            case Portrait_Tag:
                portraitAnim.Play(_tagValue);
                break;
            case Layout_Tag:
                Debug.Log("Layout = " + _tagValue);
                break;
            case Audio_Tag:
                SetCurrentAudioSO(_tagValue);
                break;
            default:
                Debug.LogError("tag name error" + _tagValue);
                break;
        }
    }
    #endregion

    #region Choice
    private void DisplayChoices()
    {
        List<Choice> currentChoiceList = currentStory.currentChoices;

        // defensive check
        if(currentChoiceList.Count > choiceList.Length)
        {
            Debug.LogError("超出 UI 容納最大數量" + currentChoiceList.Count);
        }

        int _index = 0;
        // enable the choice and fill in text
        foreach(Choice choice in currentChoiceList)
        {
            choiceList[_index].gameObject.SetActive(true);  
            choicesTextList[_index].text = choice.text;

            _index++;
        }
        for(int i = _index; i < choiceList.Length; i++)
        {
            choiceList[i].gameObject.SetActive(false); // 將剩餘沒用到 ui 隱藏
        }

        StartCoroutine(DefaultSelect());
    }

    private void HideChoice()
    {
        foreach(GameObject _choice in choiceList)
        {
            _choice.SetActive(false);
        }
    }
    
    public void MakeChoice(int _choiceIndex)
    {
        if(canContinue)
        {
            currentStory.ChooseChoiceIndex(_choiceIndex);
            ContinueStory();
        }
    }

    private IEnumerator DefaultSelect()
    {
        EventSystem.current.SetSelectedGameObject(null);

        yield return new WaitForEndOfFrame();

        EventSystem.current.SetSelectedGameObject(choiceList[0].gameObject);
    }
    #endregion

    #region Sound
    private void SetCurrentAudioSO(string _id)
    {
        DialogueAudioSO _audioSO = null;
        audioSODictionary.TryGetValue(_id, out _audioSO);

        if(_audioSO != null)
        {
            currentAudioSO = _audioSO;
        }
        else
        {
            Debug.LogError("Failed To Find AudioSO id " + _id);
        }
    }
    private void PlayDialogueSound(int _currentCharacterCount, char _currentCharacter)
    {
        AudioClip[] _dialogueAudioList = currentAudioSO.dialogueAudioList;
        int _frequencyLevel = currentAudioSO.frequencyLevel;
        float _maxPitch = currentAudioSO.maxPitch;
        float _minPitch = currentAudioSO.minPitch;
        bool _isStopSoundOrNot = currentAudioSO.isStopSoundOrNot; 

        // 隔幾個字發出聲音
        if(_currentCharacterCount % _frequencyLevel == 0)
        {
            if(_isStopSoundOrNot)    audioSource.Stop(); // 先停止再撥放

            // 讓每個字頻率音軌相同
            AudioClip _talkingClip = null;
            if(makePredictable)
            {
                int _hashCode = _currentCharacter.GetHashCode();

                // Sound
                int _predictableIndex = Math.Abs(_hashCode % _dialogueAudioList.Length);
                _talkingClip = _dialogueAudioList[_predictableIndex];

                // Pitch
                int _minPitchInt = (int) (_minPitch * 100);
                int _maxPitchInt = (int) (_maxPitch * 100);
                int _pitchRangeInt = _maxPitchInt - _minPitchInt; 

                if(_pitchRangeInt != 0)
                {
                    int _predictablePointInt = (_hashCode % _pitchRangeInt) + _minPitchInt;
                    float _predictablePitch = _predictablePointInt / 100f;

                    audioSource.pitch = _predictablePitch;
                }

                // Can not divide by 0
                else
                {
                    audioSource.pitch = _minPitch;
                }
            }
            else
            {
                // Sound
                int _randomIndex = UnityEngine.Random.Range(0, _dialogueAudioList.Length);
                _talkingClip = _dialogueAudioList[_randomIndex];

                // pitch
                audioSource.pitch = UnityEngine.Random.Range(_minPitch, _maxPitch);
            }

            // play
            audioSource.PlayOneShot(_talkingClip);
        }
    }
    #endregion
}
