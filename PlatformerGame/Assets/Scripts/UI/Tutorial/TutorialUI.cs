using System.Collections;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public GameObject[] tutorials;
    public float displayTime = 3f;
    private bool hasAppeared = false;

    private void Update()
    {
        if (PlayerMovement.instance.isGrounded && !hasAppeared)
        {
            hasAppeared = true;
            StartCoroutine(ShowTutorialsSequentially());
        }
    }

    private IEnumerator ShowTutorialsSequentially()
    {
        foreach (GameObject tuto in tutorials)
        {
            tuto.SetActive(true);
            yield return new WaitForSeconds(displayTime);
            tuto.SetActive(false);
        }
    }
}
