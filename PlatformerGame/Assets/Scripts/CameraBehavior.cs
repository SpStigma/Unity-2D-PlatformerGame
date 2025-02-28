using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Camera cam;
    public GameObject target;
    public GameObject anchorRigth;
    public GameObject anchorLeft;
    public float lerpSpeed = 10;

    public void LateUpdate()
    {
        if(PlayerMovement.instance.moveValue.x > 0)
        {
            WhenPlayerMovingToRight(anchorRigth);
        }
        else if(PlayerMovement.instance.moveValue.x < 0)
        {
            WhenPlayerMovingToLeft(anchorLeft);
        }
        else
        {
            LookAtPlayer(target);
        }
    }

    public void LookAtPlayer(GameObject target)
    {
        if(target != null)
        {
            Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y + 1, -10);
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, lerpSpeed * Time.deltaTime);
        }
    }

    public void WhenPlayerMovingToRight(GameObject target1)
    {
        if(target1 != null)
        {
            Vector3 newPlacement = new Vector3(target1.transform.position.x, target1.transform.position.y + 1, -10);
            cam.transform.position = Vector3.Lerp(cam.transform.position, newPlacement, lerpSpeed * Time.deltaTime);
        }
    }

    public void WhenPlayerMovingToLeft(GameObject target1)
    {
        if(target1 != null)
        {
            Vector3 newPlacement = new Vector3(target1.transform.position.x, target1.transform.position.y + 1, -10);
            cam.transform.position = Vector3.Lerp(cam.transform.position, newPlacement, lerpSpeed * Time.deltaTime);
        }
    }
}
