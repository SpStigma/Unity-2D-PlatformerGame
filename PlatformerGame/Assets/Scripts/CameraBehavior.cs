using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Camera cam;
    public GameObject target;

    public void LateUpdate()
    {
        LookAtPlayer(target);
    }

    public void LookAtPlayer(GameObject target)
    {
        if(target != null)
        {
            cam.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 1, -10);
        }
    }
}
