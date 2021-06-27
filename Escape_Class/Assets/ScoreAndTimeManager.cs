using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAndTimeManager : MonoBehaviour
{
    public Text scoreTxt;
    public Text timeText;
    public int score;
    private float startTime;

    void Start()
    {
        score = 0;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //score++;
        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        scoreTxt.text = score.ToString();
        timeText.text = minutes + ":" + seconds;
    }
}
