using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputAction moveAction;
    InputAction jump;
    public float speed = 5;
    private SpriteRenderer sprite;
    private Animator animator;
    private bool isGrounded;
    private Rigidbody2D rb;
    public float jumpForce = 5f;

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
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveValue.x, 0, 0) * speed * Time.deltaTime;
        bool isMoving = moveValue.x != 0 || moveValue.y != 0;
        animator.SetBool("isRunning", isMoving);
        transform.position += movement;

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
}
