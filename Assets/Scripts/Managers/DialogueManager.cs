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
    private int _currentDialogueIndex = 0;
    private Chat _nextChat;
    private Chat[] _chats;
    private int _currentChatIndex = 0;
    

    private void Awake()
    {
        CheckForDialogues();
    }

    // Start is called before the first frame update
    void Start()
    {
        EnableObjects();
    }

    private void CheckForDialogues()
    {
        if (Dialogues.Length == 0)
        {
            if (!_disableLogs)
                Debug.LogWarning("DialogueManager in "+gameObject.name+": there is no Dialogues set.");

            return;
        }

        _currentDialogueIndex = 0;
    }

    private void EnableObjects()
    {
        if (!EnableGameObjects)
            return;

        foreach(GameObject obj in WhatToEnable)
        {
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
        _currentDialogueIndex = 0;
        CurrentDialogue = Dialogues[0];

        LoadChats();
        UpdateUIWithChat();
    }

    private void LoadChats()
    {
        _chats = Dialogues[_currentDialogueIndex].Chats;

        if (_chats == null)
        {
            DebugLogNoChats();
            return;
        }
        if (_chats.Length == 0)
        {
            DebugLogNoChats();
            return;
        }
    }

    private void DebugLogNoChats()
    {
        if (!_disableLogs)
            Debug.Log("Dialogue Manager in " + gameObject.name + ": Dialogue *" + Dialogues[_currentDialogueIndex].gameObject.name + "* doesn't have any chat set.");
    }

    //TODO
    private void UpdateUIWithChat()
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
            if(CurrentChat.Charaters.Length == 0)
            {
                DebugLogNoCharacters();
                return;
            }
            if (String.IsNullOrEmpty(CurrentChat.MultipleCharactersNameSeparator))
                CurrentChat.MultipleCharactersNameSeparator = "";
            if (String.IsNullOrEmpty(CurrentChat.LastCharacterNameSeparator))
                CurrentChat.MultipleCharactersNameSeparator = "";

            foreach (Character character in CurrentChat.Charaters)
            {

            }

            DialogueUI.Instance.WriteDialogueSender(result);
        }
        
    }

    private void DebugLogNoCharacters ()
    {
        if (!_disableLogs)
            Debug.Log("Dialogue Manager in " + gameObject.name + ": Dialogue *" + Dialogues[_currentDialogueIndex].gameObject.name + "* doesn't have any Character set.");
    }

    public void NextDialogue()
    {

    }

    public void PreviousDialogue()
    {

    }

    public void NextChat()
    {

    }

    public void PreviousChat()
    {

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
