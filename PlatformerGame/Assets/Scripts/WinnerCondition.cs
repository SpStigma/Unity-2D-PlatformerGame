using UnityEngine;

public class WinnerCondition : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Goal"))
        {
            Debug.Log("hit flag");
        }       
    }
}
