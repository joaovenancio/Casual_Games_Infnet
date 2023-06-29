using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class DetectTouch : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] public UnityEvent RunWhenTouched;

    [Header("Variables Setup (optional)")]
    [SerializeField] private Collider2D _collider = null;

    private void Awake()
    {
        if (_collider == null)
            _collider = GetComponent<Collider2D>();
    }

    public void Detect(CallbackContext context)
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position, worldPoint);

        if (hit.collider == null)
            return;

        if (hit.collider.Equals(_collider))
        {

            //runWhenTouched();
            RunWhenTouched.Invoke();
            Debug.Log("COLISÃO COLISÃO COLISÃO COLISÃO COLISÃO COLISÃO");
        }
    }
}
