using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatManager : MonoBehaviour
{
    [Header("References setup")]
    [SerializeField] private List<Seat> _seats;

    public int NumberSeats { get; private set; }
    public int NumberFreeSeats { get; private set; }
    public int NumberOccupiedSeats { get; private set; }

    public static SeatManager Instance { get; private set; }

    private void Awake()
    {
        SingletonCheck();

        SetupVariables();

        DebugVariables();
    }

    private void SetupVariables()
    {
        NumberSeats = _seats.Count;
        NumberFreeSeats = NumberSeats;
        NumberOccupiedSeats = 0;

    }

    public bool Seat (GameObject customer)
    {
        if (NumberFreeSeats > 0)
        {
            customer.GetComponent<NPCControler>().QueuePosition = 0;

            Seat seat = GetFreeSeat();

            seat.Occupied = true;
            seat.Customer = customer;
            NumberFreeSeats--;

            Vector3 whereToSit = seat._sittingPoint.position;

            customer.GetComponent<Move>().MoveTo(new Vector2(whereToSit.x, whereToSit.y));

            return true;
        } else
        {
            return false;
        }
    }

    private Seat GetFreeSeat()
    {
        if (NumberFreeSeats > 0 &&
            _seats.Count > 0)
        {
            foreach (Seat seat in _seats)
            {
                if (!seat.Occupied) {
                    return seat;
                }
            }
        }
        
        return null;
    }

    public void DebugVariables()
    {
        Debug.Log("SeatManager: S - " + NumberSeats + "| F - " + NumberFreeSeats + "| O - " + NumberOccupiedSeats);
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
