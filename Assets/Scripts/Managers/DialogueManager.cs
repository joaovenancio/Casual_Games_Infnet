using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;

//TODO: Interface
public class DialogueManager : Singleton<DialogueManager>
{
    [Header("List of Dialogues")]
    [Space]
    public Dialogue[] Dialogues;
    [Space]
    [Space]
    [Header("Enable Game Objects when dialogue starts")]
    [Space]
    public bool EnableGameObjects;
    public GameObject[] WhatToEnable;
    [Space]
    [Space]
    [Header("Events to run")]
    [Space]
    public UnityEvent RunOnStart;
    [Space]
    public UnityEvent RunOnEnd;
    [Space]
    public UnityEvent RunWhenNoDialoguesLeft;
    [Space]
    [Space]
    [Header("Options")]
    [Space]
    [SerializeField] private bool _disableLogs = false;
    [Space]
    [Space]
    [Header("Variables")]
    [Space]
    public Dialogue CurrentDialogue = null;
    public Chat CurrentChat;
    

    private Dialogue _nextDialogue = null;
    private Dialogue _previousDialogue = null;
    private int _currentDialogueIndex = 0;
    private Chat _nextChat;
    private Chat[] _chats;
    private int _currentChatIndex = 0;
    

    private void Awake()
    {
        InitializeSingleton();
        CheckForDialogues();
        CheckEvents();

        Debug.Log("Final Awake");
    }

    private void CheckEvents()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        RunOnStart.Invoke();
        EnableObjects();
    }

    private void CheckForDialogues()
    {
        if (Dialogues.Length == 0)
        {
            if (!_disableLogs)
                Debug.LogWarning("DialogueManager in "+gameObject.name+": there any Dialogue set.");

            return;
        }

        _currentDialogueIndex = 0;
        CurrentDialogue = Dialogues[_currentDialogueIndex];

        if (CurrentDialogue == null)
        {
            if (!_disableLogs)
                Debug.LogWarning("DialogueManager in " + gameObject.name + ": There is a Null element in the Dialogues list. Please, check the references.");

            return;
        }

        UpdateDialogues();
    }

    private void EnableObjects()
    {
        if (!EnableGameObjects)
            return;

        foreach(GameObject obj in WhatToEnable)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartDialogue(Dialogue dialogue)
    {

    }

    public void StartDialogue()
    {
        CurrentDialogue.RunOnStart.Invoke();

        LoadChats();
        UpdateUIWithChat();

        CurrentDialogue.RunOnEnd.Invoke();
    }

    #pragma warning disable CS0168
    public void UpdateDialogues()
    {

        try {
            if (Dialogues[_currentDialogueIndex + 1] != null)
            {
                _nextDialogue = Dialogues[_currentDialogueIndex + 1];
            }
        } catch (IndexOutOfRangeException e) {
            _nextDialogue = null;
        }

        try
        {
            if (Dialogues[_currentDialogueIndex - 1] != null)
            {
                _previousDialogue = Dialogues[_currentDialogueIndex - 1];
            }
        }
        catch (IndexOutOfRangeException e)
        {
            _previousDialogue = null;
        }
    }
    #pragma warning restore CS0168

    //TODO:
    private void LoadChats()
    {
        _chats = Dialogues[_currentDialogueIndex].Chats;

        if (_chats == null)
        {
            Debug.Log("NULO");
            DebugLogNoChats();
            return;
        }
        if (_chats.Length == 0)
        {
            Debug.Log("NULO MANINHO");
            DebugLogNoChats();
            return;
        }
        if (_chats[0].Equals(default(Chat)))
        {
            Debug.Log("TOTALMENTE NULO MANINHO");
            DebugLogNoChats();
            return;
        }

        _currentChatIndex = 0;
    }

    private void DebugLogNoChats()
    {
        if (!_disableLogs)
            Debug.Log("Dialogue Manager in " + gameObject.name + ": Dialogue *" + _currentDialogueIndex + " - on GameObject " + Dialogues[_currentDialogueIndex].gameObject.name + "* doesn't have any chat set.");
    }

    //TODO
    private void UpdateUIWithChat()
    {
        CurrentChat.RunOnStart.Invoke();
        UpdateSender();
        UpdateText();
        CurrentChat.RunOnEnd.Invoke();
    }

    private void UpdateText()
    {
        
    }

    private void UpdateSender()
    {
        if (CurrentChat.DontUseCharactersNames)
        {
            DialogueUI.Instance.WriteDialogueSender(CurrentChat.CustomSender);
        }
        else
        {
            String result = default;

            if (CurrentChat.Charaters == null)
            {
                DebugLogNoCharacters();
                return;
            }
            if (CurrentChat.Charaters.Length == 0)
            {
                DebugLogNoCharacters();
                return;
            }
            if (String.IsNullOrEmpty(CurrentChat.MultipleCharactersNameSeparator))
                CurrentChat.MultipleCharactersNameSeparator = "";
            if (String.IsNullOrEmpty(CurrentChat.LastCharacterNameSeparator))
                CurrentChat.LastCharacterNameSeparator = "";

            bool firstCharacter = true;

            foreach (Character character in CurrentChat.Charaters)
            {
                if (!firstCharacter)
                {
                    result += CurrentChat.MultipleCharactersNameSeparator;
                }
                else
                {
                    firstCharacter = false;
                }

                result += character.name;
            }

            result += CurrentChat.LastCharacterNameSeparator;

            DialogueUI.Instance.WriteDialogueSender(result);
        }
    }

    private void DebugLogNoCharacters ()
    {
        if (!_disableLogs)
            if (!CurrentChat.DontUseCharactersNames)
                Debug.Log("Dialogue Manager in " + gameObject.name + ": Dialogue *" + Dialogues[_currentDialogueIndex].gameObject.name + "* doesn't have any Character set.");
    }

    public void NextDialogue()
    {
        if (_nextDialogue == null)
        {
            RunWhenThereIsNoDialogueLeft();
        } else {
            CurrentDialogue = _nextDialogue;
            _currentDialogueIndex++;
            UpdateDialogues();
            StartDialogue();
        }
        
    }

    private void RunWhenThereIsNoDialogueLeft()
    {
        RunWhenNoDialoguesLeft.Invoke();

        Dialogues = new Dialogue[0];
        RunOnStart = new UnityEvent();
        RunOnEnd = new UnityEvent();
        RunWhenNoDialoguesLeft = new UnityEvent();
        CurrentDialogue = null;
        CurrentChat = default;
        _nextDialogue = null;
        _currentDialogueIndex = 0;
        _nextChat = default;
        _chats = new Chat[0];
        _currentChatIndex = 0;
    }

    public void PreviousDialogue()
    {
        if (_previousDialogue == null)
            return;

        CurrentDialogue = _previousDialogue;
        UpdateDialogues();
        StartDialogue();
    }

    

    //Menu Itens
    [MenuItem("GameObject/Dialogue System/Dialogue Manager", false, 1)]
    public static void CreateDialogueManager()
    {
        UnityEngine.Object dialogueManager = new GameObject("Dialogue Manager");
        dialogueManager.AddComponent<DialogueManager>();
    }

    [MenuItem("GameObject/Dialogue System/Dialogue Manager with Dialogue UI", false, 1)]
    public static void CreateDialogueManagerWithUI()
    {
        UnityEngine.Object dialogueManager = new GameObject("Dialogue Manager");
        dialogueManager.AddComponent<DialogueUI>();
        dialogueManager.AddComponent<DialogueManager>();

    }

}
