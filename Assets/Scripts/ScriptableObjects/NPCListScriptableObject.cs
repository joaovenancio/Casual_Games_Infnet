    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC List Data ", menuName = "Scriptable Objects/NPCs/NPCList")]
public class NPCListScriptableObject : ScriptableObject
{
    [Serializable]
    public class CustomerListData
    {
        public GameObject customerPrefrab;
        public bool unlocked;
    }

    [SerializeField]
    public CustomerListData[] customers;
}
