using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject cam;
    private float length, startpos;
    public float speed;
    public bool autosSrolling = false;
    public float autoScrollSpeed = 1f;

    public void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    public void LateUpdate()
    {
        float temp = (cam.transform.position.x * (1 - speed));
        float dist = (cam.transform.position.x * speed);

        if(autosSrolling)
        {
            startpos -= autoScrollSpeed * Time.deltaTime;
        }

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if(temp > startpos + length)
        {
            startpos += length;
        }
        else if(temp < startpos - length)
        {
            startpos -= length;
        }
    }
}
