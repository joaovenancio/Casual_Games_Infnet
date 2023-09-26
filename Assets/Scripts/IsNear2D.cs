using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public Collision2DEvent FunctionsToCallOnTriggerStay;
    [Header("Optional")]
    public bool DisableWarnings = false;

    private Collider2D _collider;
    private bool _hasGameObjects;
    private bool _hasTags;

    private void Start()
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

            foreach (GameObject objectsToAnalize in GameObjectsToObeserveIfItsNear)
            {
                if (objectsToAnalize == null)
                {
                    _hasGameObjects = false;
                }
            }
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

    //TODO: Implement a list of objects that the script should be observing, and, for every object, tell if there is a collision or not. Use a string key for the values. Could be something arbitrary like a string or the object per se. Returns a boolean
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //GameObject.Find("TEXTOTESTE").GetComponent<TMP_Text>().text = TagsToObeserveIfItsNear[0];

       
        

        foreach (string tagToAnalize in TagsToObeserveIfItsNear)
        {
            if (collision.gameObject.CompareTag(tagToAnalize))
            {
                //GameObject.Find("TEXTOTESTE").GetComponent<TMP_Text>().text = "OI";
                IsNear = true;
                if (FunctionsToCallOnTriggerEnter != null)
                    FunctionsToCallOnTriggerEnter.Invoke(collision);
            }
        }

        if (_hasGameObjects)
        {
            foreach (GameObject objectsToAnalize in GameObjectsToObeserveIfItsNear)
            {
                if (collision.gameObject.CompareTag(objectsToAnalize.tag))
                {
                    //GameObject.Find("TEXTOTESTE").GetComponent<TMP_Text>().text = "OI";
                    IsNear = true;
                    if (FunctionsToCallOnTriggerEnter != null)
                        FunctionsToCallOnTriggerEnter.Invoke(collision);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (string tagToAnalize in TagsToObeserveIfItsNear)
        {
            if (collision.gameObject.CompareTag(tagToAnalize))
            {
                IsNear = false;
                if (FunctionsToCallOnTriggerExit != null)
                    FunctionsToCallOnTriggerExit.Invoke(collision);
            }
        }

        if (_hasGameObjects)
        {
            foreach (GameObject objectsToAnalize in GameObjectsToObeserveIfItsNear)
            {
                if (collision.gameObject.CompareTag(objectsToAnalize.tag))
                {
                    IsNear = false;
                    if (FunctionsToCallOnTriggerExit != null)
                        FunctionsToCallOnTriggerExit.Invoke(collision);
                }
            }
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    foreach (GameObject objectsToAnalize in GameObjectsToObeserveIfItsNear)
    //    {
    //        if (objectsToAnalize.Equals(collision.gameObject))
    //        {
    //            IsNear = true;
    //            if (FunctionsToCallOnTriggerStay != null)
    //                FunctionsToCallOnTriggerStay.Invoke(collision);
    //        }
    //    }

    //    foreach (string tagToAnalize in TagsToObeserveIfItsNear)
    //    {
    //        if (tagToAnalize.Equals(collision.gameObject.tag))
    //        {
    //            IsNear = true;
    //            if (FunctionsToCallOnTriggerStay != null)
    //                FunctionsToCallOnTriggerStay.Invoke(collision);
    //        }
    //    }
    //}

    //Custom classes:
    [System.Serializable]
    public class Collision2DEvent : UnityEvent<Collider2D> { }
}
