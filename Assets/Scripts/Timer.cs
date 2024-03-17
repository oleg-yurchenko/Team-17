using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
       
        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        //if ((t % 60) >= 10)
        //{
        //    Debug.Log("timer reset");
        //    ToggleTextVisibility();
        //    reset();
        //}
        // somehow add the reset for level end 

        timerText.text = minutes + ":" + seconds;
    }


    void reset()
    {
        startTime = Time.time;
    }

    public void ToggleTextVisibility()
    {
        timerText.enabled = !timerText.enabled;
    }
}