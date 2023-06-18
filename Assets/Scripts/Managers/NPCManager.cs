using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField]
    public List<NPCControler> customersList;
    [SerializeField]
    private NPCListScriptableObject _listOfAvailableNPC;

    private void Awake()
    {
        customersList = new List<NPCControler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
