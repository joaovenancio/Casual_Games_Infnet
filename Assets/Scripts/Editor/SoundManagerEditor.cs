using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundManager))]
public class SoundManagerEditor : Editor
{
    SerializedProperty _volume;

    void OnEnable()
    {
        _volume = serializedObject.FindProperty("_globalAudioVolume");
    }

    private void Awake()
    {
        
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));

        SoundManager soundManager = (SoundManager)target;

        EditorGUILayout.LabelField("Volume Configuration", EditorStyles.boldLabel);
        soundManager.GlobalAudioVolume = EditorGUILayout.Slider("Global Audio Volume", soundManager.GlobalAudioVolume, 0f, 1f);

        _volume.floatValue = soundManager.GlobalAudioVolume;
        
        serializedObject.ApplyModifiedProperties();

        DrawPropertiesExcluding(serializedObject, new string[] { "m_Script" });

        //base.OnInspectorGUI();
    }
}
