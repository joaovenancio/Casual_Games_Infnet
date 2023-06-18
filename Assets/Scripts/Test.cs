using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Position(CallbackContext cntx)
    {
        Debug.Log("Position: " + Camera.main.ScreenToWorldPoint(cntx.ReadValue<Vector2>()) );
    }
}
