using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IInput
{
    public Action<Vector2> onMovementInput { get; set; }
    public Action<Vector3> onMovementDIrectionInput { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        GetMovementDIrection();
    }

    private void GetMovementDIrection()
    {
        var cameraForwardDIrection = Camera.main.transform.forward;
        Debug.DrawRay(Camera.main.transform.position, cameraForwardDIrection * 10, Color.red);
        var directionToMoveIn = Vector3.Scale(cameraForwardDIrection, (Vector3.right + Vector3.forward));
        Debug.DrawRay(Camera.main.transform.position, directionToMoveIn * 10, Color.blue);
        onMovementDIrectionInput?.Invoke(directionToMoveIn);
    }

    private void GetMovementInput()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        onMovementInput?.Invoke(input);
    }
}
