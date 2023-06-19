using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControler : MonoBehaviour
{
    [SerializeField]
    public bool waiting;
    [SerializeField]
    public int queuePosition; // First in the line is the number 0

    private void Awake()
    {
        
    }
}
