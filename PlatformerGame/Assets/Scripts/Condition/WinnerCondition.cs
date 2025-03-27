using UnityEngine;

public class WinnerCondition : MonoBehaviour
{
    public static WinnerCondition instance;
    public GameObject UIWinner;
    public bool stageEnd = false;

    public void Awake()
    {
        instance = this;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Goal"))
        {
            stageEnd = true;
            Time.timeScale = 0;
            if(UIWinner != null)
            {
                UIWinner.SetActive(true);
            }
            if(CursorEffect.instance != null)
            {
                CursorEffect.instance.SetMenuState(true);
            }

        }
        else
        {
            stageEnd = false;
        }
    }
}
