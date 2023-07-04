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
    [SerializeField] private Sprite[] _emojis;

    private ChangeSprite _changeSprite;
    [SerializeField] private bool _isPlayerNear;
    private bool _showedEmoji = false;
    private bool _chosedFood = false;

    private void Awake()
    {
        SetupVariables();
    }

    private void SetupVariables()
    {
        _dialogueBox.SetActive(false);
        _changeSprite = GetComponent<ChangeSprite>();
        _changeSprite.TargetGameObject= _dialogueBox.GetComponent<Transform>().Find("Image").gameObject;
        if (_emojis.Length == 0)
            Debug.LogWarning("No emojis");
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
            if (!_showedEmoji)
            {
                ShowEmoji(0);
                _showedEmoji = true;
            }
            if (!_chosedFood)
            {
                StartCoroutine(ChooseFood(5));
                _chosedFood = true;
            }
        }  
        else if (state == NPCState.ORDERING)
        {
            //Something
        }
        else if (state == NPCState.WAITING_FOOD)
        {
            //Something -> maybe a timer
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
        if (state == NPCState.ORDERING && _isPlayerNear)
        {
            Debug.Log("I took the order!");
            state = NPCState.WAITING_FOOD;
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
        //Animation
        _changeSprite.SpriteToChange = _foodToOrder.GetComponent<SpriteRenderer>().sprite;
        _changeSprite.Change();
    }

    public void ShowEmoji(int index)
    {
        _dialogueBox.SetActive(true);
        //Animation
        _changeSprite.SpriteToChange = _emojis[index];
        _changeSprite.Change();
    }

    public void ChangePlayerIsNear(bool isPlayerNear)
    {
        _isPlayerNear = isPlayerNear;
    }

    IEnumerator ChooseFood(int time)
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        _foodToOrder = NPCManager.Instance.RecieveAFoodToOrder();
        //ShowDialogueFoodToOrder();

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(time);

        ShowDialogueFoodToOrder();
        state = NPCState.ORDERING;

        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

}
