using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private PlayerMove controls;
    private Animator animator;


    void Awake()
    {
        controls = new PlayerMove();
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    void FixedUpdate()
    {
     
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
        if (moveInput.x > 0)
        {
            animator.SetFloat("Speed", 7);
        }
        else{ 
            animator.SetFloat("Speed", 0);

        }

        Debug.Log(moveInput.x);

    }
}