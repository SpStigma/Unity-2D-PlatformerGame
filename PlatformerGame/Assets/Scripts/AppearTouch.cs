using System.Collections;
using UnityEngine;

public class AppearTouch : MonoBehaviour
{
    public GameObject letter;
    private float range = 3f;
    private Vector3 originalScale;
    private float beatSpeed = 3f;
    private float beatAmount = 0.1f;
    
    private void Start()
    {
        originalScale = letter.transform.localScale;
        StartCoroutine(CheckPlayerProximity());
    }

    private IEnumerator CheckPlayerProximity()
    {
        while (true)
        {
            PlayerClose();
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void PlayerClose()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
        bool playerNearby = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                playerNearby = true;
                if (!letter.activeSelf)
                {
                    letter.SetActive(true);
                }
                AnimateLetter();
                return;
            }
        }

        if (!playerNearby && letter.activeSelf)
        {
            letter.SetActive(false);
            letter.transform.localScale = originalScale;
        }
    }

    private void AnimateLetter()
    {
        float time = Time.time * beatSpeed;
        float scaleFactor = 1 + Mathf.Abs(Mathf.Sin(time)) * beatAmount;

        letter.transform.localScale = originalScale * scaleFactor;
    }
}
