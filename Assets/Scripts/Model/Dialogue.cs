using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using static DialogueManager;

[Serializable]
public class Dialogue : MonoBehaviour
{
    [Space]
    public Chat[] Chats;
    [Space]
    [Space]
    [Space]
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

    [MenuItem("GameObject/Dialogue System/Dialogue", false, 1)]
    public static void CreateDialogue()
    {
        UnityEngine.Object dialogueManager = new GameObject("Dialogue");
        dialogueManager.AddComponent<Dialogue>();
    }
}
