using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static NPCListScriptableObject;

public class NPCManager : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] public List<GameObject> Customers;
    [SerializeField] public LinkedList<GameObject> CustomersInQueue;
    private List<GameObject> _unlockedNPCs;

    [Header("Setup")]
    [SerializeField] private GameObject _customersContainer;
    [SerializeField] private Transform[] _pathToStartOfTheQueue;
    [SerializeField] private Transform[] _queueSpots;

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
        Debug.Log("CustomersWaitingInLine: " + CustomersInQueue.Count);
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
        CustomersInQueue = new LinkedList<GameObject>();
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

        //Debug.Log(CustomersInQueue == null);

        if (totalOFAvailableNPCs >= 1)
        {

            int randomNumber = UnityEngine.Random.Range(0, totalOFAvailableNPCs-1);

            GameObject customer = GameObject.Instantiate(_unlockedNPCs[randomNumber], location, Quaternion.identity);

            if (_customersContainer != null)
                customer.transform.SetParent(_customersContainer.transform, true);

            //NPCControler customerController = customer.GetComponent<NPCControler>();

            Customers.Add(customer);

            return customer;
        } else
        {
            return null;
        }
    }

    public bool AddToQueue (GameObject customer)
    {
        NPCControler npcController = customer.GetComponent<NPCControler>();

        if (npcController == null)
            return false;

        int placeInQueue = CustomersInQueue.Count;

        npcController.QueuePosition = placeInQueue;
        CustomersInQueue.AddLast(customer);

        Move moveScript = customer.GetComponent<Move>();

        Vector2[] pathToQueue = Utils.Convert<Transform[], Vector2[]>(_pathToStartOfTheQueue);
        
        if (pathToQueue == null || pathToQueue[0] == null)
        {
            Debug.Log("NPCManager -> AddToQueue: No pathToQueue.");
            return false;
        } else
        {
            moveScript.MoveTo(pathToQueue);
        }

        //Utils.DebugVariables(new object[] { placeInQueue, npcController.QueuePosition, customer.name }, new string[] {"Count: ", "Place in queue: ", "Object Name: "});

        Debug.Log(npcController.QueuePosition);

        UpdateQueue();

        return true;
    }

    public GameObject RemoveFromQueue ()
    {
        LinkedListNode<GameObject> linkedNode = CustomersInQueue.First;
        GameObject firstInLine = linkedNode.Value;

        if (linkedNode == null ||
            firstInLine == null)
            return null;

        linkedNode = linkedNode.Next;
        int newPosition = 0;

        while (linkedNode != null)
        {
            linkedNode.Value.GetComponent<NPCControler>().QueuePosition = newPosition;
            linkedNode = linkedNode.Next;

            Debug.Log("PASSEI");

            newPosition++;
        }

        CustomersInQueue.RemoveFirst();

        UpdateQueue();

        return firstInLine;
    }

    private void UpdatePositionsInLine()
    {
        
    }

    public void UpdateQueue ()
    {
        Debug.Log(CustomersInQueue.First == null);
        if (CustomersInQueue.First == null)
            return;

        if (_queueSpots == null || _queueSpots[0] == null)
        {
            Debug.Log("NPCManager -> AddToQueue: No _queueSpots.");
            return;
        }
        else
        {

            LinkedListNode<GameObject> listNode = CustomersInQueue.First;

            while (listNode != null)
            {
                GameObject customer = listNode.Value;

                int customerPositionInQueue = customer.GetComponent<NPCControler>().QueuePosition;
                Move moveScript = customer.GetComponent<Move>();

                if (customerPositionInQueue+1 >= _queueSpots.Length)
                {
                    Debug.Log("LAST");

                    Vector2 spot = Utils.Convert<Transform, Vector2>(_queueSpots[_queueSpots.Length - 1]);
                    moveScript.MoveTo(spot);
                } else
                {
                    Debug.Log("WIIIII "+ customerPositionInQueue);
                    Vector2 spot = Utils.Convert<Transform, Vector2>(_queueSpots[customerPositionInQueue]);
                    moveScript.MoveTo(spot);
                }

                listNode = listNode.Next;
            }

            
        }
    }

    public bool SeatCustomer ()
    {
        
        
        return true;
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

    internal void RecieveAFoodToOrder()
    {
        GameObject food = GameManager.Instance.RequestAFood();  
    }
}
