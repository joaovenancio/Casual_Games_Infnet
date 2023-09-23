using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

//TODO: Interface
public class DialogueManager : Singleton<DialogueManager>
{
    [Header("List of Dialogues")]
    [Space]
    public Dialogue[] Dialogues;
    public bool _runDialoguesOnStart = true;
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
    public UnityEvent RunWhenNoDialoguesLeft;
    [Space]
    [Space]
    [Header("Options")]
    [Space]
    [SerializeField] private bool _disableLogs = false;
    [Space]
    [Space]
    [Header("Debug Variables")]
    [Space]
#if UNITY_EDITOR
    [ReadOnly]
#endif
    public Dialogue CurrentDialogue = null;
#if UNITY_EDITOR
    [ReadOnly]
#endif
    public Chat CurrentChat;
    

    private Dialogue _nextDialogue = null;
    private Dialogue _previousDialogue = null;
    private int _currentDialogueIndex = 0;
    private Chat _nextChat;
    private Chat _previousChat;
    private Chat[] _chats;
    private int _currentChatIndex = 0;
    private bool _resetVariables = false;
    

    private void Awake()
    {
        InitializeSingleton();
        CheckForDialogues();

        //Debug.Log("Final Awake");
    }

    // Start is called before the first frame update
    void Start()
    {
        RunOnStart.Invoke();
        EnableObjects();

        if (_runDialoguesOnStart)
            StartDialogue();
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
        _resetVariables = true;
        RunWhenThereIsNoDialogueLeft();
        _resetVariables = false;

        CurrentDialogue = dialogue;

        UpdateDialogues();

        StartDialogue();
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

    #pragma warning disable CS0168
    public void UpdateChats()
    {
        try
        {
            if (_chats[_currentChatIndex + 1] != null)
            {
                _nextChat = _chats[_currentChatIndex + 1];
            }
        }
        catch (IndexOutOfRangeException e)
        {
            _nextChat = null;
        }

        try
        {
            if (_chats[_currentChatIndex - 1] != null)
            {
                _previousChat = _chats[_currentChatIndex - 1];
            }
        }
        catch (IndexOutOfRangeException e)
        {
            _previousChat = null;
        }
    }
    #pragma warning restore CS0168

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
        CurrentChat = _chats[_currentChatIndex];

        UpdateChats();
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
        string text = CurrentChat.WhatToSay;
        if (text != null)
            DialogueUI.Instance.WriteText(text);
    }

    private void UpdateSender()
    {
        if (CurrentChat.DontUseCharactersNames)
        {
            DialogueUI.Instance.WriteDialogueSender(CurrentChat.CustomSender);
        }
        else
        {
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

            String result = default;
            bool firstCharacter = true;
            Character character = null;

            for (int characterIndex = 0; characterIndex < CurrentChat.Charaters.Length; characterIndex++)
            {
                character = CurrentChat.Charaters[characterIndex];

                if (character != null)
                {
                    if (!firstCharacter)
                    {
                        #pragma warning disable CS0168
                        try
                        {
                            if (CurrentChat.Charaters[characterIndex + 1] != null)
                                result += CurrentChat.MultipleCharactersNameSeparator;

                        } catch (IndexOutOfRangeException e)
                        {
                            result += CurrentChat.LastCharacterNameSeparator;
                        }
                        #pragma warning restore CS0168
                    }
                    else
                    {
                        firstCharacter = false;
                    }

                    result += character.Name;
                } else
                {
                    if (!_disableLogs)
                        Debug.Log("Dialogue Manager in " + gameObject.name + ": Character index *" + characterIndex + "* in Chat *" + _currentChatIndex + "* of Dialogue *" + _currentDialogueIndex +
                            "* (located on GameObject " + Dialogues[_currentDialogueIndex].gameObject.name + ") doesn't have any character set.");
                }
                
            }

            if (CurrentChat.WhatToWriteOnEndOfSenderText != null)
            {
                if (!CurrentChat.WhatToWriteOnEndOfSenderText.Equals(""))
                {
                    result += CurrentChat.WhatToWriteOnEndOfSenderText;
                }
            }

            DialogueUI.Instance.WriteDialogueSender(result);
        }
    }

    private void DebugLogNoCharacters ()
    {
        if (!_disableLogs)
            if (!CurrentChat.DontUseCharactersNames)
                Debug.Log("Dialogue Manager in " + gameObject.name + ": Dialogue *" + Dialogues[_currentDialogueIndex].gameObject.name + "* doesn't have any Character set.");
    }

    

    private void RunWhenThereIsNoDialogueLeft()
    {
        Dialogues = new Dialogue[0];
        RunOnStart = new UnityEvent();
        RunWhenNoDialoguesLeft = new UnityEvent();
        CurrentDialogue = null;
        CurrentChat = null;
        _nextDialogue = null;
        _previousDialogue = null;
        _currentDialogueIndex = 0;
        _nextChat = null;
        _previousChat = null;
        _chats = new Chat[0];
        _currentChatIndex = 0;

        if (!_resetVariables)
            RunWhenNoDialoguesLeft.Invoke();
    }

    public void NextDialogue()
    {
        if (_nextDialogue == null)
        {
            Debug.Log("Dialogue Manager in " + gameObject.name + ": There is no Dialogues left.");
        }
        else
        {
            CurrentDialogue = _nextDialogue;
            _currentDialogueIndex++;
            UpdateDialogues();
            StartDialogue();
        }

    }

    public void PreviousDialogue()
    {
        if (_previousDialogue == null)
            return;

        CurrentDialogue = _previousDialogue;
        _currentDialogueIndex--;
        UpdateDialogues();
        StartDialogue();
    }

    public void NextChat()
    {
        if (_nextChat == null)
        {
            NextDialogue();
        }
        else
        {
            CurrentChat = _nextChat;
            _currentChatIndex++;
            UpdateChats();
            UpdateUIWithChat();
        }

    }

    public void PreviousChat()
    {
        if (_previousChat == null)
            return;

        CurrentChat = _previousChat;
        _currentChatIndex--;
        UpdateChats();
        UpdateUIWithChat();
    }

# if UNITY_EDITOR

    //Menu Itens
    [MenuItem("GameObject/Dialogue System/Dialogue Manager", false, 1)]
    public static void CreateDialogueManager()
    {
        UnityEngine.Object dialogueManager = new GameObject("Dialogue Manager");
        dialogueManager.AddComponent<DialogueManager>();

        if (Selection.activeObject != null)
        {
            dialogueManager.GameObject().transform.parent = Selection.activeObject.GameObject().transform; ;
        }

        ProjectWindowUtil.ShowCreatedAsset(dialogueManager);
    }

    [MenuItem("GameObject/Dialogue System/Dialogue Manager with Dialogue UI", false, 1)]
    public static void CreateDialogueManagerWithUI()
    {
        UnityEngine.Object dialogueManager = new GameObject("Dialogue Manager");
        dialogueManager.AddComponent<DialogueUI>();
        dialogueManager.AddComponent<DialogueManager>();

        if (Selection.activeObject != null)
        {
            dialogueManager.GameObject().transform.parent = Selection.activeObject.GameObject().transform; ;
        }

        ProjectWindowUtil.ShowCreatedAsset(dialogueManager);

    }


    //TODO: Make it a new class
    //https://discussions.unity.com/t/how-to-make-a-readonly-property-in-inspector/75448/7
    public class ReadOnlyAttribute : PropertyAttribute
    {

    }

    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
                                                GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position,
                                   SerializedProperty property,
                                   GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }

#endif

}
