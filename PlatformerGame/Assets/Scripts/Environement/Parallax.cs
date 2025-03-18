using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject cam;
    private float lengthX, startposX;
    private float heightY, startposY;
    public float speed;
    public bool autoScrolling = false;
    public float autoScrollSpeed = 1f;

    public void Start()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        heightY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    public void LateUpdate()
    {
        float tempX = (cam.transform.position.x * (1 - speed));
        float distX = (cam.transform.position.x * speed);
        float tempY = (cam.transform.position.y * (1 - speed));
        float distY = (cam.transform.position.y * speed);

        if (autoScrolling)
        {
            startposX -= autoScrollSpeed * Time.deltaTime;
        }

        transform.position = new Vector3(startposX + distX, startposY + distY, transform.position.z);

        if (tempX > startposX + lengthX)
        {
            startposX += lengthX;
        }
        else if (tempX < startposX - lengthX)
        {
            startposX -= lengthX;
        }

        if (tempY > startposY + heightY)
        {
            startposY += heightY;
        }
        else if (tempY < startposY - heightY)
        {
            startposY -= heightY;
        }
    }
}
