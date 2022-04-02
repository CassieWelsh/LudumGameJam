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
    private PlayerInput playerInput;
    private InputAction move;
    private InputAction fire;

    void OnEnable()
    {
        move = playerInput.Player.Move;
        move.Enable();

        fire = playerInput.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }    

    void OnDisable()
    {
        move.Disable();
        fire.Disable();
    }    

    void Awake()
    {
        rgbody = GetComponent<Rigidbody2D>();
        playerInput = new PlayerInput();
    }

    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();        
        rgbody.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        if(moveDirection.x == 0 && moveDirection.y == 0)
            rgbody.velocity -= new Vector2(0, fallingSpeed);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        print("Pew");
    }
}
