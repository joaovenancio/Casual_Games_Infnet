using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] public string Name;
    [SerializeField] public float Price;

    [Header("References setup")]
    [SerializeField] private Sprite _sprite;

    private void Awake()
    {
        GetSprite();
    }

    private void GetSprite()
    {
        if (_sprite == null)
            return;

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _sprite = spriteRenderer.sprite;

    }

}
