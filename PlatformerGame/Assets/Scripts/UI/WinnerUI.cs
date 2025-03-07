using UnityEngine;
using TMPro;

public class WinnerUI : MonoBehaviour
{
    public GameObject[] otherUI;
    private float timer;
    public TextMeshProUGUI timerToShow;


    public void Start()
    {
        DiasbleOtherUI(otherUI);
        ShowTimer();
    }

    public void DiasbleOtherUI(GameObject[] otherUI)
    {
        foreach(GameObject otherUI2 in otherUI)
        {
            otherUI2.SetActive(false);
        }
    }

    public void ShowTimer()
    {
        timer = Timer.instance.time;

        timerToShow.text = timer.ToString("F2");
    }
}
