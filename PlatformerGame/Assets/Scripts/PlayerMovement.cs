using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputAction moveAction;
    public float speed = 5;
    private SpriteRenderer sprite;
    private Animator animator;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveValue.x, 0, moveValue.y) * speed * Time.deltaTime;
        bool isMoving = moveValue.x != 0 || moveValue.y != 0;
        animator.SetBool("isRunning", isMoving);
        transform.position += movement;

        FlipSprit(moveValue.x);
    }

    public void FlipSprit(float moveX)
    {
        if(moveX != 0 && sprite != null)
        {
            sprite.flipX = moveX < 0;
        }
    }
}
