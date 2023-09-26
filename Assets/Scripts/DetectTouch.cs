using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

        GameObject.FindFirstObjectByType<PlayerInput>().currentActionMap.FindAction("TouchedTheScreen").performed += Detect;
        //GetComponent<PlayerInput>().currentActionMap.FindAction("TouchedTheScreen").canceled += Detect;
        //GetComponent<PlayerInput>().onActionTriggered += Detect;
    }

    public void Detect(CallbackContext context)
    {
        //GameObject.Find("TEXTOTESTE").GetComponent<TMP_Text>().text = "OI";

        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());

        ContactFilter2D filter2D = new ContactFilter2D().NoFilter();
        RaycastHit2D[] hits = new RaycastHit2D[30];
        Physics2D.Raycast(Camera.main.transform.position, worldPoint, filter2D, hits, Mathf.Infinity);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null)
                continue;

            if (hit.collider.Equals(_collider))
            {

                //runWhenTouched();
                RunWhenTouched.Invoke();
                //GameObject.Find("TEXTOTESTE").GetComponent<TMP_Text>().text = RunWhenTouched.GetPersistentMethodName(0);
                //Debug.Log("COLISÃO COLISÃO COLISÃO COLISÃO COLISÃO COLISÃO");
                
            }
        }

        
    }
}
