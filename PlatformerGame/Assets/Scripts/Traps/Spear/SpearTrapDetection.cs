using System.Collections;
using UnityEngine;

public class SpearTrapDetection : MonoBehaviour
{
    private Animator animator;

    public void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            animator.SetBool("isAttack", true);
            StartCoroutine(ResetTriggerAnimation());
        }
    }

    private IEnumerator ResetTriggerAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("isAttack", false);
    }
}
