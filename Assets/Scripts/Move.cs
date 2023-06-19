using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Variables")]
    public bool moving;
    public float _translationSpeed;

    private Transform _transform;
    private Vector2 _lastTargetLocation;
    private bool _move = false;

    private bool _hasMultipleLocations = false;
    private Vector2[] _locationsToMove; 
    private int _currentLocation = 0;

    private void Awake()
    {
        moving = false;
        _transform = gameObject.GetComponent<Transform>();
        _lastTargetLocation = transform.position;
        if (_locationsToMove == null)
        {
            _locationsToMove = new Vector2[0];
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ProcessMovement();
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
                if (_hasMultipleLocations &&
                    _currentLocation < _locationsToMove.Length)
                {
                    _lastTargetLocation = _locationsToMove[_currentLocation];
                    _currentLocation++;
                }
                else
                {
                    moving = false;
                    _move = false;

                }
            }
        }
    }

    public void MoveTo(Vector2 targetLocation)
    {
        _lastTargetLocation = targetLocation;
        _move = true;
    }

    public void MoveTo(Vector2[] path)
    {
        _currentLocation = 0;
        _hasMultipleLocations = true;
        _locationsToMove = path;

        _lastTargetLocation = path[_currentLocation];
        _currentLocation++;
        _move = true;

    }
}
