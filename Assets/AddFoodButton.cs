using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddFoodButton : MonoBehaviour
{
    public GameObject FoodPrefrab;
    public Image ButtonImage;
    public TMPro.TMP_Text ButtonText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddFoodToPLayer()
    {
        GameManager.Instance.AddFoodToPlayer(FoodPrefrab);
    }
}
