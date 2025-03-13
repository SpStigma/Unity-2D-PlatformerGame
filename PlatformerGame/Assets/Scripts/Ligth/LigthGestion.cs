using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class LightGestion : MonoBehaviour
{
    public Light2D globalLight;
    public float newIntensity = 0.3f;
    private float originalIntensity;
    public float transitionSpeed = 1f;
    private Coroutine lightCoroutine;
    private bool isDimming = false;

    private void Start()
    {
        if (globalLight != null)
        {
            originalIntensity = globalLight.intensity;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            float targetIntensity;

            if (isDimming)
            {
                targetIntensity = originalIntensity;
                isDimming = false;
            }
            else
            {
                targetIntensity = newIntensity;
                isDimming = true;
            }

            if (lightCoroutine != null)
            {
                StopCoroutine(lightCoroutine);
            }
            
            lightCoroutine = StartCoroutine(DiminishedIntensityOverTime(targetIntensity));
        }
    }

    private IEnumerator DiminishedIntensityOverTime(float targetIntensity)
    {
        float startIntensity = globalLight.intensity;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            globalLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime);
            elapsedTime += Time.deltaTime * transitionSpeed;
            yield return null;
        }

        globalLight.intensity = targetIntensity;
    }
}
