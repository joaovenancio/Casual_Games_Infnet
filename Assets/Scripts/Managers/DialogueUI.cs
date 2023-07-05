using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DialogueUI : Singleton<DialogueUI>
{
    [Serialize]
    public ILayoutElement text;
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        //rectTransform.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
