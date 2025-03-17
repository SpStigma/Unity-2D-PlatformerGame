using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    private Animator animator;
    private bool playerInTrap = false;
    private Collider2D playerCollider;

    private void Start()
    {
        animator = GetComponent<Animator>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !playerInTrap)
        {
            playerInTrap = true;
            playerCollider = collision;
            StartCoroutine(ActivateTrap());
        }      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTrap = false;
        }
    }

    private IEnumerator ActivateTrap()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Attack");

        if (playerInTrap && playerCollider != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
