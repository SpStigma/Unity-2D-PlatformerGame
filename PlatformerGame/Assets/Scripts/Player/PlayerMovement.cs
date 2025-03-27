using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Singleton")]
    public static PlayerMovement instance;

    [Header("Mouvement")]
    public float speed = 3;
    public float maxSpeed = 8;
    public bool isMoving;
    public Vector2 moveValue;
    private Coroutine accelerationCoroutine;
    private float idleTimer = 0f;

    [Header("Jump")]
    public bool isGrounded;
    public float jumpForce = 5f;
    public float jumpTime;

    [Header("Coyote Time")]
    public float coyoteTime = 0.15f;
    private float coyoteTimeCounter;

    [Header("Gravité")]
    public float fallMultiplier = 2.5f;
    public float jumpMultiplier = 2.5f;
    private Vector2 vecGravity;

    [Header("Références")]
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    private InputAction moveAction;
    private InputAction jump;

    bool isJumping;
    float jumpCounter;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }

    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveValue.x, 0, 0) * speed * Time.deltaTime;
        isMoving = moveValue.x != 0;
        animator.SetBool("isRunning", isMoving);
        transform.position += movement;

        if (isMoving)
        {
            if (idleTimer > 0) idleTimer = 0;
            if (accelerationCoroutine == null)
            {
                accelerationCoroutine = StartCoroutine(Acceleration(2, .5f));
            }
        }
        else
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= 0.1f)
            {
                if (accelerationCoroutine != null)
                {
                    StopCoroutine(accelerationCoroutine);
                    accelerationCoroutine = null;
                }
                speed = 3;
                idleTimer = 0f;
            }
        }

        FlipSprite(moveValue.x);

        if (!isGrounded)
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (jump.IsPressed() && (isGrounded || coyoteTimeCounter > 0))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = true;
            jumpCounter = 0;
            coyoteTimeCounter = 0;
        }

        if(rb.linearVelocity .y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if(jumpCounter > jumpTime)
            {
                isJumping = false;
            }
            rb.linearVelocity += vecGravity * jumpMultiplier * Time.deltaTime;
        }

        if(!jump.IsPressed())
        {
            isJumping = false;
        }

        if(rb.linearVelocity.y < 0)
        {
            rb.linearVelocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }
    }

    public void FlipSprite(float moveX)
    {
        if (moveX != 0 && sprite != null)
        {
            sprite.flipX = moveX < 0;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            coyoteTimeCounter = 0;
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            coyoteTimeCounter = coyoteTime;
            isGrounded = false;
        }
    }

    public IEnumerator Acceleration(float factorSpeed, float duration)
    {
        float startSpeed = speed;
        float targetSpeed = Mathf.Min(startSpeed * factorSpeed, maxSpeed);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            speed = Mathf.Lerp(startSpeed, targetSpeed, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        speed = targetSpeed;
        accelerationCoroutine = null;
    }
}