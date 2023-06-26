using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Utils : MonoBehaviour
{
    private static string[] PREFIXES_TO_IGNORE = { "" , " ", "   ", null};

    #pragma warning disable CS0168
    public static void DebugVariables(object obj)
    {
        if (obj == null)
            return;

        UnityEngine.Debug.Log(obj.ToString());
    }

    public static void DebugVariables(object[] objectList)
    {
        if (objectList == null)
            return;

        foreach (object obj in objectList)
        {
            if (obj == null)
                continue;

            UnityEngine.Debug.Log(obj);
        }
    }

    public static void DebugVariables(object[] objectList, string[] prefixText)
    {
        if (objectList == null ||
            prefixText == null)
            return;

        int prefixIterator = 0;

        foreach (object obj in objectList)
        {
            if (obj == null)
            {
                prefixIterator++;
                continue;
            }

            string textToLog = "";
            string prefix;

            try
            {
                prefix = prefixText[prefixIterator];
            } catch (Exception e)
            {
                prefix = null;
            }

            if (!prefix.Equals(PREFIXES_TO_IGNORE) &&
                prefix != null)
            {
                textToLog += prefix;
            }

            textToLog += obj.ToString();
            prefixIterator++;

            UnityEngine.Debug.Log(textToLog);
        }
    }

    public static void DebugVariables(object[] objectList, string[] prefixText, string firstText)
    {
        if (objectList == null ||
            prefixText == null ||
            firstText == null)
            return;

        UnityEngine.Debug.Log(firstText);

        int prefixIterator = 0;

        foreach (object obj in objectList)
        {
            if (obj == null)
            {
                prefixIterator++;
                continue;
            }

            string textToLog = "";
            string prefix;

            try
            {
                prefix = prefixText[prefixIterator];
            }
            catch (Exception e)
            {
                prefix = null;
            }

            if (!prefix.Equals(PREFIXES_TO_IGNORE) &&
                prefix != null)
            {
                textToLog += prefix;
            }

            textToLog += obj.ToString();
            prefixIterator++;

            UnityEngine.Debug.Log(textToLog);
        }
    }

    public static void DebugVariables(string name, object objectToDebug)
    {
        if (name == null ||
            objectToDebug == null)
            return;

        Type objectType = objectToDebug.GetType();

        UnityEngine.Debug.Log(objectType.Name + ": ");

        try
        {
            FieldInfo fieldInfo = objectType.GetField(name.ToString(), BindingFlags.NonPublic |
                         BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

            UnityEngine.Debug.Log(name + "-> " +
            fieldInfo.GetValue(objectToDebug));

        } catch (Exception e)
        {
            return;
        }
    }

    public static void DebugVariables(string[] names, object objectToDebug)
    {
        if (names == null ||
            objectToDebug == null)
            return;

        Type objectType = objectToDebug.GetType();

        UnityEngine.Debug.Log(objectType.Name + ": ");

        foreach (string name in names)
        {
            try
            {
                FieldInfo fieldInfo = objectType.GetField(name.ToString(), BindingFlags.NonPublic |
                         BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

                UnityEngine.Debug.Log(name + "-> " +
                    fieldInfo.GetValue(objectToDebug));
            }
            catch (Exception e)
            {
                continue;
            }
        }
    }
    #pragma warning restore CS0168

}
