using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Customer", menuName = "Scriptable Objects/NPCs/Customer")]
public class CustomersScriptableObject : ScriptableObject
{
    public bool unlocked;
    [SerializeField] public SpriteRenderer sprite;
}
