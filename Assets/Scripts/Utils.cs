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

    private void Start()
    {

    }

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


    public static Vector3 TransformToPosition (Transform transform)
    {
        if (transform == null)
            return Vector3.zero;

        return transform.position;
    }

    public static Vector3[] TransformToPosition(Transform[] transforms)
    {
        if (transforms == null)
            return null;

        Vector3[] vector2Array = new Vector3[transforms.Length];

        for (int i = 0; i < transforms.Length; i++)
        {
            vector2Array[i] = new Vector2(transforms[i].transform.position.x, transforms[i].transform.position.y);
        }

        return vector2Array;
    }

    public static U Convert<T, U>(T objectToConvert)
    {
        Type inputType = typeof(T);
        Type targetType = typeof(U);

        UnityEngine.Debug.Log((typeof(Vector3[])));
        UnityEngine.Debug.Log(targetType.Equals(typeof(Vector3[])));

        if ((inputType.Equals(typeof(Transform)) || objectToConvert is Transform[]) &&
            (targetType.Equals(typeof(Vector2)) || targetType.Equals(typeof(Vector3))) )
        {

            Vector3 output = Vector3.zero;

            if (inputType.Equals(typeof(Transform)) )
            {
                Transform t = objectToConvert as Transform;
                output = t.position;
            }
            else if (objectToConvert is Transform[])
            {
                Transform[] t = objectToConvert as Transform[];
                output = t[0].position;
            }

            if (targetType.Equals(typeof(Vector2)))
            {
                Vector2 result = output;

                return (U) System.Convert.ChangeType(result, targetType);
            } 
            else if (targetType.Equals(typeof(Vector3)))
            {
                Vector3 result = output;

                return (U)System.Convert.ChangeType(result, targetType);
            }
        }
        else if (objectToConvert is Transform[] &&
            ( targetType.Equals(typeof(Vector3[])) || targetType.Equals(typeof(Vector2[]))) )
        {
            Transform[] transforms = objectToConvert as Transform[];

            int inputLenght = transforms.Length;

            Vector2[] vector2Array = new Vector2[inputLenght];
            Vector3[] vector3Array = new Vector3[inputLenght];

            for (int i = 0; i < transforms.Length; i++)
            {
                if (transforms[i] != null)
                {
                    if (targetType.Equals(typeof(Vector3[])))
                    {
                        vector3Array[i] = transforms[i].position;
                    }
                    else
                    {
                        vector2Array[i] = transforms[i].position;
                    }
                } else
                {
                    continue;
                }
            }

            if (targetType.Equals(typeof(Vector3[])))
            {
                return (U)System.Convert.ChangeType(vector3Array, targetType);
            } else
            {
                return (U)System.Convert.ChangeType(vector2Array, targetType);
            }

        }

        UnityEngine.Debug.Log("Utilities: Conversion not implemented.");
        return default(U);
    }
}
