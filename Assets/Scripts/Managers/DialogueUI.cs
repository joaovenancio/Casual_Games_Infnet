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
    [Header("References Setup")]
    [SerializeField] private GameObject _interfaceTextElement;
    [SerializeField] private GameObject _interfaceSenderTextElement;
    [Space]
    [Header("Options")]
    [SerializeField] private bool _disableLogs = false;

    private TMPro.TextMeshProUGUI _textMeshPro = null;
    private UnityEngine.UI.Text _textUnity = null;
    private TMPro.TextMeshProUGUI _senderTextMeshPro = null;
    private UnityEngine.UI.Text _senderTextUnity = null;

    private bool _doUseTMPInText = false;
    private bool _doUseTMPInSender = false;

    private void Awake()
    {
        RetrieveTextObjectReferece();
        RetrieveDialogueSenderObjectReference();
    }

    public void WriteText(string text)
    {
        if (CheckTextReferences())
            return;

        if (_doUseTMPInText)
        {
            _textMeshPro.text = text;
        } else
        {
            _textUnity.text = text;
        }
    }
    
    public void AddToText(string textToAdd)
    {
        if (CheckTextReferences())
            return;

        if (_doUseTMPInText)
        {
            _textMeshPro.text += textToAdd;
        } else
        {
            _textUnity.text += textToAdd;
        }
    }

    public void WriteDialogueSender(string whoIsTalking)
    {
        if (CheckSenderReferences())
            return;

        if (_doUseTMPInSender)
        {
            _senderTextMeshPro.text = whoIsTalking;
        } else
        {
            _senderTextUnity.text = whoIsTalking;
        }
    }

    public void AddToDialogueSender(string textToAdd)
    {
        if (CheckSenderReferences())
            return;

        if (_doUseTMPInSender)
        {
            _senderTextMeshPro.text += textToAdd;
        } else
        {
            _senderTextUnity.text += textToAdd;
        }
    }

    private void RetrieveTextObjectReferece()
    {
        _textMeshPro = _interfaceTextElement.GetComponent<TMPro.TextMeshProUGUI>();

        if (_textMeshPro != null)
        {
            _doUseTMPInText = true;
            return;
        }
        else
        {
            _textUnity = _interfaceTextElement.GetComponent<Text>();

            if (_textUnity == null)
            {
                if (!_disableLogs)
                    Debug.LogWarning("DialogueUI on " + gameObject.name + ": Interface Text Element is not set.");

                return;
            }
        }
    }

    private void RetrieveDialogueSenderObjectReference()
    {
        _senderTextMeshPro = _interfaceSenderTextElement.GetComponent<TMPro.TextMeshProUGUI>();

        if (_senderTextMeshPro != null)
        {
            _doUseTMPInSender = true;
            return;
        }
        else
        {
            _senderTextUnity = _interfaceSenderTextElement.GetComponent<Text>();

            if (_senderTextUnity == null)
            {
                if (!_disableLogs)
                    Debug.Log("DialogueUI on " + gameObject.name + ": Sender Text Element is not set.");

                return;
            }
        }
    }

    private bool CheckTextReferences()
    {
        if (_textMeshPro == null && _textUnity == null)
        {
            Debug.LogWarning("DialogueUI on " + gameObject.name + ": No Text Element defined.");

            return true;
        } else
        {
            return false;
        }
    }

    private bool CheckSenderReferences()
    {
        if (_senderTextMeshPro == null && _senderTextUnity == null)
        {
            Debug.LogWarning("DialogueUI on " + gameObject.name + ": No Sender Element defined.");

            return true;
        }
        else
        {
            return false;
        }
    }
}
