using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed = 5f;
    public float fallingSpeed = 5f;
    private Rigidbody2D rgbody;
    private Vector2 moveDirection = Vector2.zero;
    [SerializeField]
    private InputAction movementControls;

    void OnEnable()
    {
        movementControls.Enable();
    }    

    void OnDisable()
    {
        movementControls.Disable();
    }    

    void Awake()
    {
        rgbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveDirection = movementControls.ReadValue<Vector2>();        
        rgbody.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        if(moveDirection.x == 0 && moveDirection.y == 0)
            rgbody.velocity -= new Vector2(0, fallingSpeed);
    }
}
