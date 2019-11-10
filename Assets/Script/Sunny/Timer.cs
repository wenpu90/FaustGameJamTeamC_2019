using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timer = 0;
    public GameObject gameOver;
    public Text timeUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CountDownTimer();
    }

    public void CountDownTimer()
    {
        if (timer < 60)
            timer += Time.deltaTime;
        timeUI.text = "Time : " + (int)timer;
        if (timer <= 0)
        {
            timer = 0;
            Time.timeScale = 0;
            gameOver.SetActive(true);
        }
    }
}
