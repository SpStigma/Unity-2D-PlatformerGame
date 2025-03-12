using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class LightTreeManager : MonoBehaviour
{
    private Camera mainCamera;
    private Light2D[] allLights;
    private Dictionary<Light2D, float> lightOffsets = new Dictionary<Light2D, float>();

    private float blinkSpeed = 1f;

    void Start()
    {
        mainCamera = Camera.main;
        allLights = GetComponentsInChildren<Light2D>();

        foreach (Light2D light in allLights)
        {
            lightOffsets[light] = Random.Range(0f, 1f);
        }
    }

    void LateUpdate()
    {
        foreach (Light2D light in allLights)
        {
            if (light != null)
            {
                bool isVisible = IsLightVisible(light);
                light.enabled = isVisible;

                if (isVisible)
                {
                    LightBlink(light);
                }
            }
        }
    }

    private bool IsLightVisible(Light2D light)
    {
        if (mainCamera != null)
        {
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(light.transform.position);
            return viewportPoint.x > 0 && viewportPoint.x < 1 &&
                   viewportPoint.y > 0 && viewportPoint.y < 1 &&
                   viewportPoint.z > 0;
        }
        return false;
    }

    private void LightBlink(Light2D light)
    {
        float offset = lightOffsets[light];
        light.falloffIntensity = Mathf.Abs(Mathf.Sin(Time.time * blinkSpeed + offset));
    }
}
