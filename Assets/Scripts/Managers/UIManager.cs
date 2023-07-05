using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private Canvas _dialogueCanvas;


    [Header("Setup")]
    [SerializeField] private TextDictionaryLine[] _textReferences;

    public static UIManager Instance { get; private set; }

    [Serializable]
    private struct TextDictionaryLine
    {
        public string ElementName;
        public TMPro.TMP_Text TextElementReference;
    }

    private void Awake()
    {
        SingletonCheck();
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(string uiElementName, string content)
    {
        TMP_Text textElement = FindTextElementByName(uiElementName);

        if (textElement != null)
        {
            textElement.text = content;
        }
    }

    private TMP_Text FindTextElementByName(string elementName)
    {
        foreach (TextDictionaryLine dictionaryLine in _textReferences)
        {
            if (dictionaryLine.ElementName.Equals(elementName))
            {
                return dictionaryLine.TextElementReference;
            }
        }

        Debug.Log("UI Manager: Didn't find a TMPText element with the given name -> " + elementName);

        return null;
    }
}
