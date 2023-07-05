using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;

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

    // Start is called before the first frame update
    void Start()
    {

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


    [MenuItem("GameObject/Dialogue System/Dialogue Manager", false, 1)]
    public static void CreateDialogueManager()
    {
        UnityEngine.Object dialogueManager = new GameObject("Dialogue Manager");
        dialogueManager.AddComponent<DialogueManager>();
    }

}
