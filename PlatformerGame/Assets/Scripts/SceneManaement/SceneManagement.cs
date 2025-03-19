using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public Animator animator;

    public void GoToSceneIndex(int index)
    {
        CursorEffect.instance.SetMenuState(false);
        StartCoroutine(CrossFade(index));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Retry()
    {
        CursorEffect.instance.SetMenuState(false);
        StartCoroutine(CrossFade(SceneManager.GetActiveScene().buildIndex));
    }

    private IEnumerator CrossFade(int sceneIndex)
    {
        if (animator != null)
        {
            animator.SetTrigger("Start");
            yield return new WaitForSeconds(1f);
        }

        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
    }
}
