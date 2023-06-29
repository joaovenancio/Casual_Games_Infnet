using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food List Data ", menuName = "Scriptable Objects/Food/FoodList")]
public class FoodListScriptableObject : ScriptableObject
{
    [Serializable]
    public class FoodListData
    {
        public GameObject foodPrefab;
        public bool unlocked;
    }

    [SerializeField]
    public FoodListData[] Foods;
}
