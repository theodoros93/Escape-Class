using MongoDB.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static MiniGames;

public class ScoreAndTimeManager : MonoBehaviour
{
    public Text scoreTxt;
    public Text timeText;
    public Text mistakesText;
    public int score;
    public int mistakes;
    public float startTime;
    public static List<MiniGame> miniGamesList;

    public int correctAnswers;
    public int currentRoom;

    void Start()
    {
        LoadMiniGames("60c5e98c8f159c9c8587e5dc", 3, "easy");
        score = 0;
        mistakes = 0;
        correctAnswers = 0;
        currentRoom = 1;
        startTime = Time.time;
    }

    public async void LoadMiniGames(string lessonCode, int chapter, string level)
    {
        var miniGames = new MiniGames();
        miniGamesList = await miniGames.GetMiniGames(new ObjectId(lessonCode), chapter, level);
        Debug.Log(miniGamesList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        scoreTxt.text = score.ToString();
        timeText.text = minutes + ":" + seconds;
        mistakesText.text = mistakes.ToString();
    }

    public static MiniGame getMiniGame(int gameId)
    {
        return miniGamesList[gameId];
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public void SubstractScore(int scoreToSubstract)
    {
        score -= scoreToSubstract;
    }
}
