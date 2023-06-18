using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Transform _transform;
    private Vector2 _lastTargetLocation;
    [SerializeField] private float _translationSpeed;
    

    private void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();
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
        if (!(_lastTargetLocation.Equals(_transform.position)) )
        {
            _transform.position = Vector3.MoveTowards(transform.position, _lastTargetLocation, _translationSpeed);
        }
    }

    public void MoveTo(Vector2 targetLocation)
    {
        _lastTargetLocation = targetLocation;
    }
}
