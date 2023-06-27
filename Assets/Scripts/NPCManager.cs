using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NPCListScriptableObject;

public class NPCManager : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] public List<GameObject> Customers;
    [SerializeField] public Queue<GameObject> CustomersWaitingInLine;
    private List<GameObject> _unlockedNPCs;

    [Header("Setup")]
    [SerializeField] private GameObject _customersContainer;

    [Header("Data setup")]
    [SerializeField] private NPCListScriptableObject _NPCListData;

    [Header("Test")]
    public GameObject NPC;

    public static NPCManager Instance { get; private set; }

    private void Awake()
    {
        SingletonCheck();

        InitializeUnlockedNPCsList();

        InitializeVariables();
    }

    private void DebugVariables()
    {
        Debug.Log("NPCManager variables: ");
        Debug.Log("Customers: " + Customers.Count);
        Debug.Log("CustomersWaitingInLine: " + CustomersWaitingInLine.Count);
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SingletonCheck()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void InitializeVariables()
    {
        Customers = new List<GameObject>();
        CustomersWaitingInLine = new Queue<GameObject>();
    }

    private void InitializeUnlockedNPCsList()
    {
        _unlockedNPCs = new List<GameObject>();

        foreach (CustomerListData customer in _NPCListData.customers)
        {
            if (customer.unlocked)
            {
                _unlockedNPCs.Add(customer.customerPrefrab);
            }
        }


    }

    private void UnlockNPC (GameObject npcToUnlock)
    {
        foreach (CustomerListData customer in _NPCListData.customers)
        {
            if (customer.Equals(npcToUnlock))
            {
                if (!customer.unlocked)
                {
                    customer.unlocked = true;
                    _unlockedNPCs.Add(npcToUnlock);
                    break;
                }
                else
                {
                    Debug.Log("NPCManager: Costumer already unlocked.");
                    return;
                }
                
            }
        }
    }

    //Returns a customer
    public GameObject SpawnCustomer(Vector3 location)
    {
        int totalOFAvailableNPCs = _unlockedNPCs.Count;

        Debug.Log(CustomersWaitingInLine == null);

        if (totalOFAvailableNPCs >= 1)
        {

            int randomNumber = UnityEngine.Random.Range(0, totalOFAvailableNPCs-1);

            GameObject customer = GameObject.Instantiate(_unlockedNPCs[randomNumber], location, Quaternion.identity);
            if (_customersContainer != null)
                customer.transform.SetParent(_customersContainer.transform, true);
            NPCControler customerController = customer.GetComponent<NPCControler>();

            Customers.Add(customer);

            return customer;
        } else
        {
            return null;
        }
    }

    public void MoveNPC(GameObject npc, Vector2[] path)
    {
        if (path == null || npc == null) return;

        Move moveScript = npc.GetComponent<Move>();

        moveScript.MoveTo(path);
    }

    public void MoveNPC(GameObject npc, Vector2 path)
    {
        if (path == null || npc == null) return;

        Move moveScript = npc.GetComponent<Move>();

        moveScript.MoveTo(path);
    }

}
