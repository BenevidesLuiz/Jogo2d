
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;


public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private PlayerMove controls;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private SpriteRenderer sprite;

    private Vector2 collisorSize;

    public float speed = 5f;
    private Vector2 moveInput;
    private Coroutine stopRunningCoroutine;


    public float dashDuration = 0.5f;
    public float dashCooldown = 2f;
    public float dashForce = 20f;

    private bool dashInput;
    private float dashTimer = 0f;
    private float lastDashTime = 0;
    private bool isDashing = false;

    private bool attackInput;
    private bool canAttack = true;





    void Awake()
    {
        controls = new PlayerMove();
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        controls.Player.Dash.performed += ctx => dashInput = true;
        controls.Player.Dash.canceled += ctx => dashInput = false;
        controls.Player.Attack.performed += ctx => attackInput = true;
        controls.Player.Attack.canceled += ctx => attackInput = false;

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
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        collisorSize = boxCollider.size;
    }

    void FixedUpdate()
    {
       
            
        if (!isDashing) {
            rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
            if (moveInput.x > 0) {
                transform.localScale = new Vector3(2, 2, 2);
            } else if (moveInput.x < 0) {
                transform.localScale = new Vector3(-2, 2, 2);
            }
            bool isRunning = Mathf.Abs(moveInput.x) > 0;
            if (isRunning) {
                if (stopRunningCoroutine != null) {
                    StopCoroutine(stopRunningCoroutine);
                    stopRunningCoroutine = null;
                }
                animator.SetBool("running", true);
            } else {
                stopRunningCoroutine ??= StartCoroutine(StopRunningAfterDelay());
            }

            if (dashInput && Time.time >= lastDashTime + dashCooldown) {
                animator.SetBool("dashing", true);
                StartCoroutine(PerformDash());

            }
            if (attackInput) {
                StartCoroutine(AttackRoutine());
            }
        }


    }
    IEnumerator StopRunningAfterDelay()
    {
        yield return new WaitForSeconds(0.05f);
        if (Mathf.Abs(moveInput.x) == 0)
        {
            //animator.SetBool("running", false);
        }

        stopRunningCoroutine = null;
    }
    IEnumerator PerformDash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        lastDashTime = Time.time;

        float dashDirection = Mathf.Sign(moveInput.x);
        if (dashDirection == 0) dashDirection = transform.localScale.x;

        while (dashTimer > 0)
        {
            rb.linearVelocity = new Vector2(dashDirection * dashForce, rb.linearVelocity.y);
            dashTimer -= Time.deltaTime;
            yield return null;
        }



        isDashing = false;
        animator.SetBool("dashing", false);
    }
    private IEnumerator AttackRoutine()
    {
        canAttack = false;
        animator.SetBool("attacking", true);


        yield return new WaitForSeconds(0.5f); 





    




        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(6f);
        canAttack = true;
    }
}