using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Variables")]
    [HideInInspector]
    [SerializeField]private float _globalAudioVolume = 0.125f;
    public float GlobalAudioVolume {
        get { return this._globalAudioVolume; }
        set 
        {
            _globalAudioVolume = value;
            _globalAudioSource.volume = this._globalAudioVolume;
        }
    }

    [Header("Setup")]
    [SerializeField] private AudioEntry[] _audios;
    [SerializeField] private AudioSource _globalAudioSource;

    private Dictionary<string, AudioClip> _audioClipDictionary;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        SingletonCheck();

        VariablesSetup();

        LoadStructToDictionary();
    }

    private void VariablesSetup()
    {
        _audioClipDictionary = new Dictionary<string, AudioClip>();
    }

    private void SingletonCheck()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public bool Play (string clipName, bool isSoundtrack, GameObject target)
    {
        try
        {
            AudioClip audioClip = _audioClipDictionary[clipName]; //Exception thrower

            if (isSoundtrack)
            {
                GlobalAudioVolume = 0.2f;

                _globalAudioSource.volume = GlobalAudioVolume;
                _globalAudioSource.clip = audioClip;
                _globalAudioSource.Play();
            } else
            {
                if (target != null)
                {
                    AudioSource targetAudioSource = null;

                    try
                    {
                        targetAudioSource = target.GetComponent<AudioSource>(); //Exception thrower

                    } catch (Exception e)
                    {
                        Debug.Log("Sound Manager: No Audio Source Detected. Initializing default AudioSource. " + e.Message);

                        targetAudioSource = target.AddComponent<AudioSource>();
                    }

                    targetAudioSource.clip = audioClip;
                    targetAudioSource.Play();

                } else
                {
                    Debug.Log("Sound Manager: No target specified.");
                }
            }

            return true;

        } catch (Exception e)
        {
            Debug.Log("SoundManager: No clip found with that name. Please check the clips list. " + e.Message);
            
            return false;
        }
    }

    //Editor:



    //Structs:
    [Serializable]
    private struct AudioEntry
    {
        public string AudioName;
        public AudioClip AudioClip;
        [TextArea(1,5)]
        public string AdditionalInfo;
    }
    
    private void LoadStructToDictionary()
    {
        if (_audios.Length > 0 &&
            _audioClipDictionary != null)
        {
            foreach (AudioEntry audio in _audios)
            {
                _audioClipDictionary[audio.AudioName] = audio.AudioClip;
            }
        }
    }

}
