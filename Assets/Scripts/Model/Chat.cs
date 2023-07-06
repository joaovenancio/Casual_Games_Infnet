using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

[Serializable]
public struct Chat
{
    public Character[] Charaters;
    public bool DontUseCharactersNames;
    public string CustomSender;
    [TextArea]
    public string WhatToSay;
    public String MultipleCharactersNameSeparator;
    public String LastCharacterNameSeparator;
    public UIImageToSpriteDictionary[] UiImagesToChangeUsingReferences;
    public UiImageToCharacterDictionary[] UiImagesToChangeUsingCharacter;
    public UnityEvent RunOnStart;
    public UnityEvent RunOnEnd;



    [Serializable]
    public struct UIImageToSpriteDictionary
    {
        public Image TargetUiImage;
        public Sprite SpriteToChang;
    }

    [Serializable]
    public struct UiImageToCharacterDictionary
    {
        public Image TargetUiImage;
        public Character Character;
        public bool IsThumbnail;
        public string SpriteNickname;
    }
}
