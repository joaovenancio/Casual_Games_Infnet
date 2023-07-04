using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControler : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] public NPCState state;
    [SerializeField] public int QueuePosition; // First in the line is the number 0
    [SerializeField] private Move moveScript;
    [SerializeField] private bool _isNearSeat;
    [SerializeField] private FoodController _foodToOrder;

    [Header("Refereces Setup")]
    [SerializeField] private GameObject _dialogueBox;

    private ChangeSprite _changeSprite;
    [SerializeField] private bool _isPlayerNear;

    private void Awake()
    {
        SetupVariables();
    }

    private void SetupVariables()
    {
        _dialogueBox.SetActive(false);
        _changeSprite = GetComponent<ChangeSprite>();
        _changeSprite.TargetGameObject= _dialogueBox.GetComponent<Transform>().Find("Image").gameObject;
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

        } 
        else if (state == NPCState.CHOSING_FOOD)
        {
            _foodToOrder = NPCManager.Instance.RecieveAFoodToOrder();
            ShowDialogueFoodToOrder();
        }  
        else if (moveScript.moving)
        {
            state = NPCState.MOVING;

        }
        else if (!moveScript.moving && !(state == NPCState.ORDERING))
        {
            state = NPCState.WAITING;
        }

    }

    public void TakeOrder()
    {
        if (state == NPCState.CHOSING_FOOD && _isPlayerNear)
        {
            Debug.Log("I took the order!");
            state = NPCState.ORDERING;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.transform.parent != null)
            {
                if (collision.transform.parent.gameObject.CompareTag("Seat"))
                {
                    _isNearSeat = true;
                }
            }
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent != null)
        {
            if (collision.transform.parent.gameObject.CompareTag("Seat"))
            {
                _isNearSeat = false;
            }
        }
    }

    public void ShowDialogueFoodToOrder()
    {
        _dialogueBox.SetActive(true);
        _changeSprite.SpriteToChange = _foodToOrder.GetComponent<SpriteRenderer>().sprite;
        _changeSprite.Change();
    }

    public void ChangePlayerIsNear(bool isPlayerNear)
    {
        _isPlayerNear = isPlayerNear;
    }

}
