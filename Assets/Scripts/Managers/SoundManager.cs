using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private AudioEntry[] _audios;
    [SerializeField] private AudioSource _GlobalAudioSource;

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

    // Start is called before the first frame update
    void Start()
    {
        Play("soundtrack1-cooking", true, null);
    }

    // Update is called once per frame
    void Update()
    {
        
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
                _GlobalAudioSource.clip = audioClip;
                _GlobalAudioSource.Play();
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
                        Debug.Log("Sound Manager: No Audio Source Detected. Initializing default AudioSource.");

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
            Debug.Log("SoundManager: No clip found with that name. Please check the clips list. " + e.Source);
            
            return false;
        }
    }

    //Structs
    [Serializable]
    private struct AudioEntry
    {
        public string AudioName;
        public AudioClip AudioClip;
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
