using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IDialogueUI
{
    public void WriteText(string text) { }

    public void AddToText(string textToAdd) { }

    public void WriteDialogueSender(string whoIsTalking) { }

    public void AddToDialogueSender(string textToAdd) { }

}
