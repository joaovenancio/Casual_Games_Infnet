using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class IsNear2D : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private bool _isNear = false;
    public bool IsNear {
        get { return _isNear; }
        private set {
            _isNear = value;
        }
    }
    [Space]
    public GameObject[] GameObjectsToObeserveIfItsNear;
    public String[] TagsToObeserveIfItsNear;
    [Header("Functions to call")]
    [Space]
    public Collision2DEvent FunctionsToCallOnTriggerEnter;
    public Collision2DEvent FunctionsToCallOnTriggerExit;
    [Header("Optional")]
    public bool DisableWarnings = false;

    private Collider2D _collider;
    private bool _hasGameObjects;
    private bool _hasTags;

    private void Awake()
    {
        SetupCollider();
        CheckArrays();
    }

    private void CheckArrays()
    {
        if (GameObjectsToObeserveIfItsNear == null ||
            GameObjectsToObeserveIfItsNear.Length == 0)
        {
            _hasGameObjects = false;
            //if (!DisableWarnings)
            //{
            //    Debug.Log("IsNearPlayer2D script in " + name + ": The list of GameObjects to check is empty.");
            //}
        } else
        {
            _hasGameObjects = true;
        }

        if (TagsToObeserveIfItsNear == null ||
            TagsToObeserveIfItsNear.Length == 0)
        {
            _hasTags = false;
        }
        else
        {
            _hasTags = true;
        }

        if (!_hasGameObjects && !_hasTags)
        {
            if (!DisableWarnings)
            {
                Debug.Log("IsNearPlayer2D script in " + name + ": The list of GameObjects and Tags to check are empty.");
            }
        }
    }

    private void SetupCollider()
    {
        _collider = gameObject.GetComponent<Collider2D>();

        if (!DisableWarnings)
        {
            if (_collider == null)
            {
                Debug.LogWarning("IsNearPlayer2D script in " + name + ": The Gameobject of the attached script does not have a Collider2D.");
                enabled = false;
            }
        }

        _collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject objectsToAnalize in GameObjectsToObeserveIfItsNear)
        {
            if (objectsToAnalize.Equals(collision.gameObject))
            {
                IsNear = true;
                if (FunctionsToCallOnTriggerEnter != null)
                    FunctionsToCallOnTriggerEnter.Invoke(collision);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (GameObject objectsToAnalize in GameObjectsToObeserveIfItsNear)
        {
            if (objectsToAnalize.Equals(collision.gameObject))
            {
                IsNear = false;
                if (FunctionsToCallOnTriggerExit != null)
                    FunctionsToCallOnTriggerExit.Invoke(collision);
            }
        }
    }

    //Custom classes:
    [System.Serializable]
    public class Collision2DEvent : UnityEvent<Collider2D> { }
}