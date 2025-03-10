using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightGestion : MonoBehaviour
{
    public Light2D globalLight;
    public float newIntensity = 0.3f;
    private float originalIntensity;
    public float transitionSpeed = 1f;

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
            isDimming = true;
        }
    }

    private void Update()
    {
        if (isDimming)
        {
            DiminishedIntensityOvertime();
        }
    }

    private void DiminishedIntensityOvertime()
    {
        if (globalLight != null)
        {
            globalLight.intensity = Mathf.Lerp(globalLight.intensity, newIntensity, transitionSpeed * Time.deltaTime);

            if (Mathf.Abs(globalLight.intensity - newIntensity) < 0.01f)
            {
                globalLight.intensity = newIntensity;
                isDimming = false;
            }
        }
    }
}
