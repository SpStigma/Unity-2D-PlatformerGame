using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class LigthTreeGestion : MonoBehaviour
{
    private Camera mainCamera;
    private Light2D[] allLights;

    void Start()
    {
        mainCamera = Camera.main;
        allLights = GetComponentsInChildren<Light2D>();
        
    }

    public void LateUpdate()
    {
        foreach(Light2D light in allLights)
        {
            if(light != null)
            {
                bool isVisible = IsLightVisible(light);
                light.enabled = isVisible;
            }
        }
    }

    public bool IsLightVisible(Light2D light)
    {
        if(mainCamera != null)
        {
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(light.transform.position);
            if (viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1 && viewportPoint.z > 0)
            {
                return true;
            }
        }
        return false;
    }
}
