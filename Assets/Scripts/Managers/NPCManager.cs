using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NPCListScriptableObject;

public class NPCManager : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] public Queue<GameObject> CustomersInsideRestaurant;
    [SerializeField] public Queue<GameObject> CustomersWaitingInLine;

    [Header("Data setup")]
    [SerializeField] private NPCListScriptableObject _NPCListData;

    [Header("Test")]
    public GameObject NPC;

    public static NPCManager Instance { get; private set; }

    //Internal Variables
    private List<GameObject> _unlockedNPCs;
    private int _totalNumberOfNPCs;

    private void Awake()
    {
        SingletonCheck();

        InitializeUnlockedNPCsList();

        InitializeVariables();

    }

    private void DebugVariables()
    {
        Debug.Log("NPCManager variables: ");
        Debug.Log("CustomersInsideRestaurant: " + CustomersInsideRestaurant.Count);
        Debug.Log("CustomersWaitingInLine: " + CustomersWaitingInLine.Count);
        Debug.Log("_totalNumberOfNPCs: " + _totalNumberOfNPCs);
    }

    // Start is called before the first frame update
    void Start()
    {
        //CreateCustomer(Vector3.zero);
        //CreateCustomer(Vector3.zero);

        //try
        //{
        //    SeatManager.Instance.Seat(CustomersWaitingInLine.Dequeue());
        //    SeatManager.Instance.Seat(CustomersWaitingInLine.Dequeue());
        //    SeatManager.Instance.Seat(CustomersWaitingInLine.Dequeue());
        //} catch (Exception ex) 
        //{
        //    Debug.LogException(ex);
        //}

        //DebugVariables();

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
        CustomersInsideRestaurant = new Queue<GameObject>(GameManager.Instance.MaxNumberOfNPC);
        CustomersWaitingInLine = new Queue<GameObject>(SeatManager.Instance.NumberSeats);
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
    public GameObject CreateCustomer(Vector3 location)
    {
        int totalOFAvailableNPCs = _unlockedNPCs.Count;

        if (totalOFAvailableNPCs >= 1)
        {
            int randomNumber = UnityEngine.Random.Range(0, totalOFAvailableNPCs-1);

            GameObject customer = GameObject.Instantiate(_unlockedNPCs[randomNumber], location, Quaternion.identity);
            NPCControler customerController = customer.GetComponent<NPCControler>();

            customerController.waiting = true;
            customerController.queuePosition = CustomersWaitingInLine.Count+1;

            _totalNumberOfNPCs++;

            CustomersWaitingInLine.Enqueue(customer);

            return customer;
        } else
        {
            return null;
        }
    }

    public void MoveNPC(GameObject npc, Vector3[] path)
    {
        if (path == null || npc == null) return;

        NPCControler npcController = npc.GetComponent<NPCControler>();
        Move moveScript = npc.GetComponent<Move>();

        for (int pointsTravaled = 0; pointsTravaled < path.Length; pointsTravaled++)
        {
            if (!moveScript.moving)
            {
                moveScript.MoveTo(path[pointsTravaled]);
            }
        }
    }
}
