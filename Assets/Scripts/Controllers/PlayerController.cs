using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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

    public void MovePlayer(CallbackContext context) {
        //Debug.Log("Position: " + Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>()));
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

    public bool DeliverFood(GameObject food)
    {
        FoodHolder foodHolder = RetrieveOccupiedFoodHolder(food);

        if (foodHolder != null)
        {
            Debug.Log("Thank you!!!");
            GameManager.Instance.RecieveMoney(((int)foodHolder.FoodPrefrab.GetComponent<FoodController>().Price));
            foodHolder.FoodPrefrab = null;

            foodHolder.GetComponent<ChangeSprite>().DefaultSprite();

            return true;
        } else
        {
            Debug.Log("I dont have the food");

            return false;
        }
    }
}
