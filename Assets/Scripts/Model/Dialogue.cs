using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using static DialogueManager;

[Serializable]
public class Dialogue : MonoBehaviour, IDialogue
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

    public Chat NextChat()
    {
        return default;
    }

    public Chat PreviousChat()
    {
        return default;
    }

    #if UNITY_EDITOR
    [MenuItem("GameObject/Dialogue System/Dialogue", false, 1)]
    public static void CreateDialogue()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);

        UnityEngine.Object dialogue = new GameObject("New Dialogue");
        dialogue.AddComponent<Dialogue>();

        if (Selection.activeObject != null) {
            dialogue.GameObject().transform.parent = Selection.activeObject.GameObject().transform; ;
        }
            
        ProjectWindowUtil.ShowCreatedAsset(dialogue);

    }

#endif
}
