using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Move _moveScript;
    [SerializeField] private Camera _mainCamera;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePlayer(CallbackContext context) {
        Debug.Log("Position: " + Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>()));
        _moveScript.MoveTo(_mainCamera.ScreenToWorldPoint(context.ReadValue<Vector2>()));
    }
}
