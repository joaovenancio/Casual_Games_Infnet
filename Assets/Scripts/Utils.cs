using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public int testeDeInt = 4356723;
    private int bagas = 32;

    private void Start()
    {
        //Move move = new Move();

        //DebugScript<Utils>(this, true);

        int vari = 123213;
        int kakakaka = 01202190321;


        object[] objs = { vari, kakakaka };

        //DebugVariables(new object[] { }, this);

        DebugVariables("testeDeInt", this);
    }


    public void DebugVariables<T>(object[] objectList, T script)
    {


        foreach (object obj in objectList)
        {
            obj.ToString();

            //UnityEngine.Debug.Log(obj.GetType().DisplayName());

            UnityEngine.Debug.Log(obj.ToString());
        }
    }

    public void DebugVariables<T>(string name, T objectToDebug)
    {

        PropertyInfo[] properties = objectToDebug.GetType().GetProperties();

        //UnityEngine.Debug.Log(objectToDebug.GetType().GetField("enabled") == null);

        UnityEngine.Debug.Log(objectToDebug.GetType().GetField(name.ToString()).GetValue(objectToDebug));


        //foreach (PropertyInfo prop in properties) { 
        //    prop.
        //}

        //foreach (object obj in objectList)
        //{
        //    obj.ToString();

        //    //UnityEngine.Debug.Log(obj.GetType().DisplayName());

        //    UnityEngine.Debug.Log(obj.ToString());
        //}
    }

    public void DebugScript<T>(T scriptToDebug, bool showMonobeheaviorProperties)
    {
        Type scriptType = scriptToDebug.GetType();
        PropertyInfo[] properties = scriptType.GetProperties();

        UnityEngine.Debug.Log(scriptType.Name + " - DEBUG:");

        //MonoBehaviour monoObject;
        //if (!showMonobeheaviorProperties)
        //{
        //    monoObject = new MonoBehaviour();
        //}

        foreach (PropertyInfo property in properties)
        {

            if (!showMonobeheaviorProperties)
            {
                bool hasProperty = false;

                foreach (PropertyInfo monoBeheavorProperty in 
                    base.GetType().GetProperties())
                {
                    if (property.Equals(monoBeheavorProperty))
                    {
                        hasProperty = true;
                        UnityEngine.Debug.Log("AAAAAAAAAAAAAAAAAAAA");
                        break;
                    }
                }

                if (hasProperty)
                {
                    continue;
                }
            }

            UnityEngine.Debug.Log(property.Name + "-> ");

        }

        UnityEngine.Debug.Log("-");
        //typeof(T).GetProperties();
    }
}
