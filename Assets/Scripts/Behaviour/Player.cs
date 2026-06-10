using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Jump & Aim Settings")]
    [SerializeField] private string jumpButton = "Jump";
    [SerializeField] private float minJumpPower = 15f; 
    [SerializeField] private float maxJumpPower = 35f; 
    [SerializeField] private float maxChargeTime = 1.0f; 
    [SerializeField] private float launchBufferDuration = 0.3f; 

    [Header("Buff Settings")]
    [SerializeField] private float doubleJumpPower = 20f;
    public bool hasPermanentDoubleJump = false; 
    private bool canDoubleJumpNow = false;

    [Header("Knockback & iFrames")]
    public float horizontalKnockbackForce = 12f;
    public float upwardKnockbackForce = 8f; 
    public float iFrameDuration = 1.0f;

    [Header("Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 groundCheckBoxSize = new Vector2(0.5f, 0.1f); 
    [SerializeField] private float groundCheckDistance = 0.5f; 

    [Header("Animation Parameters")]
    [SerializeField] private string chargeParam = "isCharging";
    [SerializeField] private string groundedParam = "isGrounded";
    [SerializeField] private string yVelocityParam = "yVelocity";

    private Rigidbody2D rb;
    private Animator animator; // Link to your Animator Controller
    private SpriteRenderer spriteRenderer;
    private Camera mainCam;
    private PlayerAnimationAudio playerAudio; 
    
    private float chargeTimer;
    private bool isCharging = false;
    private Vector2 jumpDirection;
    private float launchBuffer; 
    private float originalGravityScale;
    private bool wasGrounded; 
    private float invincibilityTimer = 0f;
    private bool isCinematicActive = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Cache the component[cite: 5]
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCam = Camera.main;
        originalGravityScale = rb.gravityScale;
        playerAudio = GetComponent<PlayerAnimationAudio>();
    }

    void Update()
    {
        HandleInvincibility(); 
        if (!isCinematicActive) HandleInput();

        UpdateAnimations(); // Dedicated method to sync visuals[cite: 5]

        bool currentlyGrounded = IsGrounded();
        if (!wasGrounded && currentlyGrounded)
        {
            if (hasPermanentDoubleJump) canDoubleJumpNow = true; 
            if (playerAudio != null) playerAudio.PlayLandSound(); 
        }
        wasGrounded = currentlyGrounded;
    }

    private void UpdateAnimations()
    {
        if (animator == null) return;

        // Syncs the script state with the Animator Controller parameters[cite: 5]
        animator.SetBool(chargeParam, isCharging);
        animator.SetBool(groundedParam, IsGrounded());
        animator.SetFloat(yVelocityParam, rb.linearVelocity.y);
    }

    public void SetCinematicMode(bool active)
    {
        isCinematicActive = active;
        if (active)
        {
            isCharging = false;
            rb.linearVelocity = Vector2.zero;
        }
    }

    public bool IsInvincible() => invincibilityTimer > 0;

    private void HandleInvincibility()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
            spriteRenderer.color = new Color(1, 1, 1, Mathf.PingPong(Time.time * 15, 1));
        }
        else spriteRenderer.color = Color.white;
    }

    private void HandleInput()
    {
        bool grounded = IsGrounded();
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -mainCam.transform.position.z; 
        Vector3 worldMousePos = mainCam.ScreenToWorldPoint(mousePos);
        jumpDirection = (worldMousePos - transform.position).normalized;

        if (isCharging)
        {
            chargeTimer = Mathf.Min(chargeTimer + Time.deltaTime, maxChargeTime);
            spriteRenderer.flipX = jumpDirection.x < 0;
        }

        if (Input.GetButtonDown(jumpButton) && grounded)
        {
            isCharging = true;
            chargeTimer = 0f;
        }
        else if (Input.GetButtonDown(jumpButton) && !grounded && canDoubleJumpNow)
        {
            ExecuteInstantJump();
        }

        if (Input.GetButtonUp(jumpButton) && isCharging) PerformJump();
    }

    private void PerformJump()
    {
        float chargeFactor = chargeTimer / maxChargeTime;
        rb.gravityScale = originalGravityScale;
        rb.linearVelocity = jumpDirection * Mathf.Lerp(minJumpPower, maxJumpPower, chargeFactor);
        
        if (playerAudio != null) playerAudio.PlayJumpSound();

        launchBuffer = launchBufferDuration; 
        isCharging = false;
    }

    private void ExecuteInstantJump()
    {
        rb.gravityScale = originalGravityScale;
        rb.linearVelocity = jumpDirection * doubleJumpPower;
        if (playerAudio != null) playerAudio.PlayJumpSound();
        canDoubleJumpNow = false; 
    }

    private void FixedUpdate()
    {
        if (launchBuffer > 0)
        {
            launchBuffer -= Time.fixedDeltaTime;
            return; 
        }

        if (IsGrounded())
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0; 
        }
        else 
        { 
            rb.gravityScale = originalGravityScale; 
        }
    }

    public void ApplyKnockback(Vector2 damageSourcePosition)
    {
        if (invincibilityTimer > 0) return;
        launchBuffer = launchBufferDuration; 
        float directionX = (transform.position.x > damageSourcePosition.x) ? 1f : -1f;
        rb.linearVelocity = new Vector2(directionX * horizontalKnockbackForce, upwardKnockbackForce);
        invincibilityTimer = iFrameDuration;
    }

    public void EnablePermanentDoubleJump()
    {
        hasPermanentDoubleJump = true;
        canDoubleJumpNow = true;
    }

    public void ResetPermanentBuffs()
    {
        hasPermanentDoubleJump = false;
        canDoubleJumpNow = false;
    }

    private bool IsGrounded() 
    {
        if (groundCheck == null || launchBuffer > 0) return false;
        return Physics2D.BoxCast(groundCheck.position, groundCheckBoxSize, 0f, Vector2.down, groundCheckDistance, groundLayer);
    }
}