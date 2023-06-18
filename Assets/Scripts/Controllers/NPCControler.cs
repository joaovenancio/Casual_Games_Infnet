using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControler : MonoBehaviour
{
    [SerializeField]
    private bool _waiting;
    [SerializeField]
    private int _queuePosition; // First in the line is the number 0

    private void Awake()
    {
        
    }
}
