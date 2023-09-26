using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Move _moveScript;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private FoodHolder[] FoodHolders;
    

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    PointerEventData pointerEventData;
    List<RaycastResult> raycastResultsList;

    public void MovePlayer(CallbackContext context) {
        //Debug.Log("Position: " + Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>()));

        //Vector2 worldPoint = _mainCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());

        pointerEventData = new PointerEventData(EventSystem.current);


        pointerEventData.position = Touchscreen.current.position.ReadValue();
        raycastResultsList = new List<RaycastResult>();

        // essa parada aqui que faz a magica
        EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);


        foreach (RaycastResult hit in raycastResultsList)
        {
            Debug.Log(hit.gameObject.tag);

            if (raycastResultsList.Count <= 0)
                continue;

            if (hit.gameObject.CompareTag("UI"))
            {
                return;
            }
        }

        _moveScript.MoveTo(_mainCamera.ScreenToWorldPoint(context.ReadValue<Vector2>()));


    }

    public bool AddFood(GameObject food)
    {
        FoodHolder foodHolder = FindFoodFreeSpace();

        if (foodHolder != null)
        {
            foodHolder.FoodPrefrab = food;

            ChangeSprite changeSprite = foodHolder.GetComponent<ChangeSprite>();
            changeSprite.SpriteToChange = food.GetComponent<SpriteRenderer>().sprite;
            changeSprite.Change();

            return true;
        } else
        {
            return false;
        }

    }

    public FoodHolder FindFoodFreeSpace ()
    {
        foreach (FoodHolder foodHolder in FoodHolders)
        {
            if (foodHolder.FoodPrefrab == null)
            {
                Debug.Log("Achei no "+foodHolder.name);
                return foodHolder;
            }
        }

        return null;
    }

    private FoodHolder RetrieveOccupiedFoodHolder(GameObject food)
    {
        foreach (FoodHolder foodHolder in FoodHolders)
        {
            if (foodHolder.FoodPrefrab != null)
            {
                if (foodHolder.FoodPrefrab.GetComponent<FoodController>().Name.Equals(food.GetComponent<FoodController>().Name))
                {
                    return foodHolder;
                }
            }
        }

        return null;
    }

    public bool DeliverFood(GameObject food, NPCControler npc)
    {
        FoodHolder foodHolder = RetrieveOccupiedFoodHolder(food);

        if (foodHolder != null)
        {
            if (npc.HavePatiance)
            {
                Debug.Log("Thank you!!!");

                npc.HaveFood = true;
                npc.state = NPCState.EATING;

                SoundManager.Instance.Play("right", false, npc.gameObject);

                GameManager.Instance.RecieveMoney(((int)foodHolder.FoodPrefrab.GetComponent<FoodController>().Price));
                foodHolder.FoodPrefrab = null;

                foodHolder.GetComponent<ChangeSprite>().DefaultSprite();

                return true;
            }

            return false;
            
        } else
        {
            Debug.Log("I dont have the food");

            return false;
        }
    }
}
