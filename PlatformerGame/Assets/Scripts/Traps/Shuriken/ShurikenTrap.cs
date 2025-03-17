using UnityEngine;
using UnityEngine.SceneManagement;

public class ShurikenTrap : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }        
    }
}
