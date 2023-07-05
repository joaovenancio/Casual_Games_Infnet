using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Character : MonoBehaviour
{
    public string Name;

    public StringToSpriteDictionaryLine[] Thumbnails = new StringToSpriteDictionaryLine[0];

    public StringToSpriteDictionaryLine[] Images = new StringToSpriteDictionaryLine[0];

    [TextArea]
    public string Description;

    public Sprite SearchImage(string imageName)
    {
        foreach (StringToSpriteDictionaryLine dictionaryLine in Images)
        {
            if (dictionaryLine.Name.Equals(imageName))
                return dictionaryLine.Sprite;
        }

        return null;
    }

    public Sprite SearchThumbnail(string thumbnailName)
    {
        foreach (StringToSpriteDictionaryLine dictionaryLine in Thumbnails)
        {
            if (dictionaryLine.Name.Equals(thumbnailName))
                return dictionaryLine.Sprite;
        }

        return null;
    }

    [Serializable]
    public struct StringToSpriteDictionaryLine
    {
        public string Name;
        public Sprite Sprite;
    }
}
