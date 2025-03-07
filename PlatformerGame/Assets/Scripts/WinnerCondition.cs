using UnityEngine;

public class WinnerCondition : MonoBehaviour
{
    public GameObject UIWinner;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Goal"))
        {
            Time.timeScale = 0;
            UIWinner.SetActive(true);

        }       
    }
}
