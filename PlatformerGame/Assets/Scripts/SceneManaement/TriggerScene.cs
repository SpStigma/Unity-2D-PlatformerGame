using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScene : MonoBehaviour
{
    public GameObject transition;
    public Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(transition != null)
            {
                transition.SetActive(true);
            }
            StartCoroutine(CrossFade(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    private IEnumerator CrossFade(int sceneIndex)
    {
        if (animator != null)
        {
            animator.SetTrigger("Start");
            yield return new WaitForSeconds(1f);
        }

        SceneManager.LoadScene(sceneIndex);
    }
}
