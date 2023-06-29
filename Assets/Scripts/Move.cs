using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Variables")]
    public bool moving;
    public float _translationSpeed;
    [SerializeField] bool _instantMovement;

    private Transform _transform;
    private Vector2 _lastTargetLocation;
    private bool _move = false;

    private Queue<Vector2> _locationsToMove; 

    private void Awake()
    {
        SetupVariables();
    }

    private void FixedUpdate()
    {
        ProcessMovement();
    }

    private void SetupVariables()
    {
        moving = false;
        _transform = gameObject.GetComponent<Transform>();
        _lastTargetLocation = transform.position;

        if (_locationsToMove == null)
        {
            _locationsToMove = new Queue<Vector2>();
        }
    }

    private void ProcessMovement()
    {
        if (_move)
        {
            moving = true;

            if (!(_lastTargetLocation.Equals(_transform.position)))
            {
                moving = true;
                _transform.position = Vector3.MoveTowards(transform.position, _lastTargetLocation, _translationSpeed);

            }
            else
            {
                if (_locationsToMove.Count > 0)
                {
                    _lastTargetLocation = _locationsToMove.Dequeue();
                }
                else
                {
                    ResetVariables();
                }
            }
        }
    }

    private void ResetVariables()
    {
        moving = false;
        _move = false;
    }

    public void MoveTo(Vector2 targetLocation)
    {
        if (_instantMovement)
        {
            _lastTargetLocation = targetLocation;

        } else
        {
            _locationsToMove.Enqueue(targetLocation);

        }

        _move = true;
        //Debug.Log(targetLocation);
    }

    public void MoveTo(Vector2[] path)
    {
        if (path == null)
            return;

        foreach (Vector2 location in path)
        {
            if (location != null)
                _locationsToMove.Enqueue(location);

            //Debug.Log(location);
        }

        _move = true;

    }
}
