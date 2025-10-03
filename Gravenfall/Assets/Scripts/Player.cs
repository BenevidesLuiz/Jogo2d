
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;


public class Player : MonoBehaviour{
    private SpriteRenderer playerSprite;
    private Rigidbody2D rb;
    private PlayerMove controls;
    private Animator animator;
    public GameObject slash;
    public GameObject collisorAreaGameObject;
    private TrailRenderer trail;
    public UIManager uiManager;


    private int cantMoveDirection = 0;
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


    public float dashAttackDuration = 0.5f;
    public float dashAttackCooldown = 2f;
    public float dashAttackForce = 20f;
    public float dashDirection;
    private float lastDashAttackTime = 0;
    private float dashAttackTimer = 0f;
    private bool attackInput;
    private bool canAttack = true;
    Health playerHealth;
    [SerializeField] private Animator _animator;
    private List<Health> _objectsWithHealth = new();

    //add isso aqui... @Luiz
    private void OnFire(){
        _animator.SetTrigger("Attack");
        for(var i = _objectsWithHealth.Count - 1; i >= 0; i --){
            _objectsWithHealth[i].Damage(3);
        }
    }
    // aqui tmb

    void OnTriggerEnter2D(Collider2D col)
    {
       
            if (col.gameObject.CompareTag("BossTag"))
            {
                playerHealth.Damage(amount: 1);
            }
        
    }

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("BossTag"))
        {
            canAttack = false;
        }

    }
    void OnTriggerExit2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("BossTag"))
        {
            canAttack = true;
        }

    }

    void Awake()
    {
        controls = new PlayerMove();
        controls.Player.GoToMenu.performed += ctx => uiManager.ToggleMenuPanel();
        controls.Player.EquipSword.performed += ctx => {
            if (!animator.GetBool("dashing") && !animator.GetBool("running") && !animator.GetBool("dashing") && !animator.GetBool("attacking"))
            {

                if (animator.GetInteger("weapon") == 1)
                {
                    animator.SetInteger("weapon", 0);

                }
                else
                {

                    animator.SetInteger("weapon", 1);

                }
            }
            


        };

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        controls.Player.Dash.performed += ctx => dashInput = true;
        controls.Player.Dash.canceled += ctx => dashInput = false;
        controls.Player.Attack.performed += ctx => {
            if (canAttack == true) { 
            attackInput = true;

            }
            else
            {
                attackInput = false;
            }

        };
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
        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;
        playerHealth = GetComponent<Health>();
        playerSprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();


    }

    void FixedUpdate()
    {


        if (!isDashing) {
            trail.enabled = false;

            if (moveInput.x > 0 && cantMoveDirection != 1 && !isDashing)
            {
                transform.localScale = new Vector3(2, 2, 2);
                rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);

            }
            else if (moveInput.x < 0 && cantMoveDirection != -1 && !isDashing)
            {
                transform.localScale = new Vector3(-2, 2, 2);
                rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);

            }
            else { 
                rb.linearVelocity = new Vector2( 0, rb.linearVelocity.y);

            }
            
            bool isRunning = Mathf.Abs(moveInput.x) > 0;
            if (isRunning)
            {
                if (attackInput && Time.time >= lastDashAttackTime + dashAttackCooldown && animator.GetInteger("weapon") == 1)
                {
                    dashDirection = Mathf.Sign(moveInput.x);

                    collisorAreaGameObject.SetActive(true);
                    animator.SetBool("dashing", true);
                    animator.SetBool("attacking", true);
                    StartCoroutine(PerformDashAttack());
                }

                if (dashInput && Time.time >= lastDashTime + dashCooldown)
                {
                    dashDirection = Mathf.Sign(moveInput.x);

                    animator.SetBool("dashing", true);

                    StartCoroutine(PerformDash());

                }
                if (stopRunningCoroutine != null)
                {
                    StopCoroutine(stopRunningCoroutine);
                    stopRunningCoroutine = null;
                }
                animator.SetBool("running", true);
            }
            else
            {
                stopRunningCoroutine ??= StartCoroutine(StopRunningAfterDelay());
            }
            
           
            
        
        }else
        {
            trail.enabled = true;

        }


    }
    public void stopMovement(int direction) {
        cantMoveDirection = direction;
    }
    public void ReceiveDamage() {
        StartCoroutine(HitAnimation());
    }
    IEnumerator StopRunningAfterDelay()
    {
        yield return new WaitForSeconds(0.05f);
        if (Mathf.Abs(moveInput.x) == 0)
        {
            animator.SetBool("running", false);
        }

        stopRunningCoroutine = null;
    }
    public void CancelDash() {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        isDashing = false;
        animator.SetBool("dashing", false);
    }
    
    IEnumerator PerformDash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        lastDashTime = Time.time;

        if (cantMoveDirection != dashDirection){
            if (dashDirection == 0) dashDirection = transform.localScale.x;

            while (dashTimer > 0)
            {
                rb.linearVelocity = new Vector2(dashDirection * dashForce, rb.linearVelocity.y);
                dashTimer -= Time.deltaTime;
                yield return null;
            }

        }



        isDashing = false;
        animator.SetBool("dashing", false);
    }
    

    IEnumerator PerformDashAttack(){
        isDashing = true;
        dashAttackTimer = dashAttackDuration;
        lastDashAttackTime = Time.time;
        yield return new WaitForSeconds(0.25f);

        if (dashDirection == 0) dashDirection = transform.localScale.x;
        while (dashAttackTimer > 0)
        {
            rb.linearVelocity = new Vector2(dashDirection * dashAttackForce, rb.linearVelocity.y);

           
            dashAttackTimer -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.15f);
        

        isDashing = false;
        
       

        animator.SetBool("attacking", false);
        animator.SetBool("dashing", false);
        collisorAreaGameObject.SetActive(false);

    }
    IEnumerator HitAnimation()
    {
        float duration = 2f;
        float elapsed = 0f;
        float blinkSpeed = 10f; 

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float alpha = Mathf.Lerp(0.3f, 1f, Mathf.PingPong(Time.time * blinkSpeed, 1f));

            Color c = playerSprite.color;
            c.a = alpha;
            playerSprite.color = c;

            yield return null;
        }

        Color finalColor = playerSprite.color;
        finalColor.a = 1f;
        playerSprite.color = finalColor;
    }

}