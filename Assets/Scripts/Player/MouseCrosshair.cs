using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCrosshair : MonoBehaviour
{
    public float coordZ = -9f;
    private Camera _cam;     

    void Start()
    {
        _cam = Camera.main;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 nextPosition = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        nextPosition.z = coordZ;
        transform.position = nextPosition;    
    }
}
