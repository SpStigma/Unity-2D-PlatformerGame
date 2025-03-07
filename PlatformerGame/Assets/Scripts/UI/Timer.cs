using TMPro;
using UnityEngine;


public class Timer : MonoBehaviour
{
    public static Timer instance;
    public TextMeshProUGUI timer;
    public float time;

    public void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        timer.text = time.ToString("F2");
    }
}
