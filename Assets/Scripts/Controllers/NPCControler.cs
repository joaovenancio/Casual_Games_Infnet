using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControler : MonoBehaviour
{
    [SerializeField] public NPCState state;
    [SerializeField] public int QueuePosition; // First in the line is the number 0
    [SerializeField] private Move moveScript;
    [SerializeField] private bool _isNearSeat;

    private void Awake()
    {
        
    }

    private void Update()
    {
        Act();
        //Debug.Log(moveScript.moving);
    }

    private void Act()
    {

        if (state == NPCState.WAITING &&
            _isNearSeat)
        {
            state = NPCState.CHOSING_FOOD;

        } else if (moveScript.moving)
        {
            state = NPCState.MOVING;

        } else if (!moveScript.moving &&
            !_isNearSeat)
        {
            state = NPCState.WAITING;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.transform.parent.gameObject.CompareTag("Seat"))
            {
                _isNearSeat = true;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent.gameObject.CompareTag("Seat"))
        {
            _isNearSeat = false;
        }
    }
}
