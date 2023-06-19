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

    //Internal Variables
    private List<GameObject> _unlockedNPCs;
    private int _totalNumberOfNPCs;

    private void Awake()
    {
        InitializeUnlockedNPCsList();

        InitializeVariables();

    }

    private void InitializeVariables()
    {
        CustomersInsideRestaurant = new Queue<GameObject>(GameManager.Instance.MaxNumberOfNPC);
        CustomersWaitingInLine = new Queue<GameObject>(SeatManager.Instance.NumberSeats);
    }


    // Start is called before the first frame update
    void Start()
    {
        SeatManager.Instance.Seat(Instantiate(NPC));
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void CreateCustomer(Vector3 location)
    {
        int totalOFAvailableNPCs = _unlockedNPCs.Count;

        if (totalOFAvailableNPCs >= 1)
        {
            int randomNumber = UnityEngine.Random.Range(0, totalOFAvailableNPCs-1);

            GameObject customer = GameObject.Instantiate(_unlockedNPCs[randomNumber], location, Quaternion.identity);
            NPCControler customerController = customer.GetComponent<NPCControler>();

            customerController.waiting = true;
            customerController.queuePosition = CustomersWaitingInLine.Count-1;

            CustomersWaitingInLine.Enqueue(customer);
            //customerController
            //customersList.Add(customerController);

        }
    }


}
