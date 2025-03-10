using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    InputAction moveAction;
    InputAction jump;
    public float speed = 3;
    public float maxSpeed = 8;
    private SpriteRenderer sprite;
    private Animator animator;
    private bool isGrounded;
    private Rigidbody2D rb;
    public float jumpForce = 5f;
    private Coroutine accelerationCoroutine;
    private float idleTimer = 0f;
    public bool isMoving;
    public Vector2 moveValue;

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
    }

    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveValue.x, 0, 0) * speed * Time.deltaTime;
        isMoving = moveValue.x != 0 || moveValue.y != 0;
        animator.SetBool("isRunning", isMoving);
        transform.position += movement;

        if (isMoving)
        {
            if(idleTimer > 0)
            {
                idleTimer = 0;
            }
            if(accelerationCoroutine == null)
            {
                accelerationCoroutine = StartCoroutine(Acceleration(2, .5f));
            }
        }
        else
        {
            idleTimer += Time.deltaTime;

            if(idleTimer >= 0.1f)
            {
                if(accelerationCoroutine != null)
                {
                    StopCoroutine(accelerationCoroutine);
                    accelerationCoroutine = null;
                }
                speed = 3;
                idleTimer = 0f;
            }
        }


        FlipSprit(moveValue.x);

        if(jump.IsPressed() && isGrounded)
        {
            Jump();
        }
    }

    public void FlipSprit(float moveX)
    {
        if(moveX != 0 && sprite != null)
        {
            sprite.flipX = moveX < 0;
        }
    }

    public void Jump()
    {
        if(rb != null)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }    
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
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
