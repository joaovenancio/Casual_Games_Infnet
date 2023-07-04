using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    [Header("Setup")]
    public Sprite SpriteToChange;
    [SerializeField] private bool RunOnStart = false;
    //[SerializeField] private bool ReplaceSpriteRenderer = false;

    [Header("Optional")]
    [SerializeField] private GameObject _targetGameObject;
    public GameObject TargetGameObject {
        get { return _targetGameObject; }
        set
        {
            if (value != null)
            {
                _targetGameObject = value;
                GetTargetSpriteRenderer();
            }
        } 
    }

    private SpriteRenderer _targetSpriteRenderer;

    private void Awake()
    {
        if (TargetGameObject == null)
            TargetGameObject = gameObject;

        GetTargetSpriteRenderer();
    }

    void Start()
    {
        if (RunOnStart)
            if (HasSprite())
                Change();
    }


    public void Change()
    {
        if (HasSpriteRenderer())
        {
            if (HasSprite())
            {
                _targetSpriteRenderer.sprite = SpriteToChange;
            }
        }
    }

    private bool HasSpriteRenderer()
    {
        if (_targetSpriteRenderer == null)
        {
            Debug.LogWarning("ChangeSprite at " + gameObject.name + ": Desired GameObject does not have a SpriteRenderer component. Trying with the GameObject that this script is attached to.");

            TargetGameObject = gameObject;

            if (_targetSpriteRenderer == null)
                Debug.LogWarning("ChangeSprite at " + gameObject.name + ": Attempt failed -> Desired GameObject does not have a SpriteRenderer component.");

            return false;

        } else
        {
            return true;
        }

    }

    public void Change(Sprite newSprite)
    {
        if (HasSpriteRenderer()) {
            if (HasSprite())
            {
                _targetSpriteRenderer.sprite = newSprite;
            }
        }
    }

    public void Change(Sprite newSprite, GameObject targetGameObject)
    {
        if (newSprite != null && targetGameObject != null )
        {
            _targetSpriteRenderer = targetGameObject.GetComponent<SpriteRenderer>();

            if (_targetSpriteRenderer != null) 
            {
                _targetSpriteRenderer.sprite = newSprite;

            } else
            {
                Debug.LogWarning("ChangeSprite at " + gameObject.name + ": Desired GameObject does not have a SpriteRenderer component.");
            }
        } else
        {
            Debug.LogWarning("ChangeSprite at " + gameObject.name + ": Sprite or GameObject is null. Please check the method call.");
        }
    }

    public void Change(Sprite newSprite, SpriteRenderer targetSpriteRenderer)
    {
        if (newSprite != null && targetSpriteRenderer != null)
        {
            targetSpriteRenderer.sprite = newSprite;
        }
        else
        {
            Debug.LogWarning("ChangeSprite at " + gameObject.name + ": Sprite or SpriteRenderer is null. Please check the method call.");
        }
    }

    private bool HasSprite ()
    {
        if (SpriteToChange == null)
        {
            Debug.LogWarning("ChangeSprite at " + gameObject.name + ": Cannot run without a defined Sprite.");

            return false;
        } else
        {
            return true;
        }
    }

    private void GetTargetSpriteRenderer()
    {
        _targetSpriteRenderer = TargetGameObject.GetComponent<SpriteRenderer>();
    }

}
