using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCrosshair : MonoBehaviour
{
    private Camera _cam;     

    void Start()
    {
        _cam = Camera.main;
        Cursor.visible = false;
    }

    void Update()
    {
        transform.position = (Vector2) _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());    
    }
}
