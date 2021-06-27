using MongoDB.Bson;
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
        var miniGames = new MiniGames();
        miniGames.GetMiniGames(new ObjectId("60c5e98c8f159c9c8587e5dc"), 3, "easy");
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
