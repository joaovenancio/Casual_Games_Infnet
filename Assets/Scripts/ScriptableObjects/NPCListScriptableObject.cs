    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC List Data ", menuName = "Scriptable Objects/NPCs/NPCList")]
public class NPCListScriptableObject : ScriptableObject
{
    [Serializable]
    public struct CustomersList
    {
        public GameObject customerPrefrab;
        public bool unlocked;
    }

    [SerializeField]
    public CustomersList[] customers;
}
