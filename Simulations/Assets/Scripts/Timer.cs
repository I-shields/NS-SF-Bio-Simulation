using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float DayTime = 10;
    public float currentTime = 0f;
    public float countUp = 0f;
    public float TimeLeft = -99;
    public bool timerRunning = false;
    public bool timerStopped = false;
    private bool pressed = false;
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if(pressed)
        {
            if (timerRunning)
            {
                currentTime -= Time.deltaTime;
                countUp += Time.deltaTime;
                if (currentTime <= 0f)
                {
                    TimerEnded();
                    pressed = false;
                }

            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            pressed = true;
            timerRunning = true;
        }
    }

    public void StartTimer()
    {
        timerRunning = true;
        pressed = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
        timerStopped = true;
        currentTime = DayTime;

    }

    public void ResetTimer()
    {
        currentTime = DayTime;
        timerRunning = false;
    }

    void TimerEnded()
    {
        GameObject ui = GameObject.FindGameObjectWithTag("Ui");
        ui.GetComponent<Stats>().giveStats();
        StopTimer();
    }
}
