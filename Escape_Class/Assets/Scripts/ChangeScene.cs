using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static MiniGames;

public class ChangeScene : MonoBehaviour
{

    [SerializeField] private GameObject uiElement;
    [SerializeField] private GameObject uiQuizPopup;
    [SerializeField] private GameObject finalResultsPopup;
    [SerializeField] private int questionId;
    Text question;
    Text answer1;
    Text answer2;
    Text answer3;
    Text answer4;
    Text correctAnswer;
    Text questionLevel;
    [System.Obsolete]
    MiniGame miniGame;


    bool colliding;

    private void OnTriggerEnter(Collider other)
    {
        uiElement.SetActive(true);
        colliding = true;
    }
    private void OnTriggerExit(Collider other)
    {
        uiElement.SetActive(false);
        colliding = false;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    //SceneManager.LoadScene("Stage02");

    //}

    [System.Obsolete]
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Application.LoadLevel("Stage02");
        //}
        if (colliding && Input.GetKeyDown(KeyCode.E))
        {
            var scoreAndTimeManager = GameObject.Find("ScoreAndTime");
            if ((questionId == 3 &&
                scoreAndTimeManager.GetComponent<ScoreAndTimeManager>().correctAnswers < 3) ||
                (questionId == 7 &&
                scoreAndTimeManager.GetComponent<ScoreAndTimeManager>().correctAnswers < 7))
            {
                // den mporei na apantisei
            }
            else
            {
                uiQuizPopup.SetActive(true);
                LoadQuestion();
                Time.timeScale = 0;
            }
            //Application.LoadLevel("Stage02");
            
        }
    }

    void LoadQuestion()
    {
        //var miniGames = new MiniGames();
        //List<MiniGame> miniGamesList = await miniGames.GetMiniGames(new ObjectId("60c5e98c8f159c9c8587e5dc"), 3, "easy");
        var scoreAndTimeManager = GameObject.Find("ScoreAndTime");

        var currentQuestionId = questionId;
        if (scoreAndTimeManager.GetComponent<ScoreAndTimeManager>().currentRoom == 2)
        {
            currentQuestionId -= 4;
        }
        miniGame = ScoreAndTimeManager.getMiniGame(currentQuestionId);

        question = GameObject.Find("question").GetComponent<Text>();
        question.text = miniGame.question;

        answer1 = GameObject.Find("answer_1").GetComponentInChildren<Text>();
        answer1.text = miniGame.a;

        answer2 = GameObject.Find("answer_2").GetComponentInChildren<Text>();
        answer2.text = miniGame.b;

        answer3 = GameObject.Find("answer_3").GetComponentInChildren<Text>();
        answer3.text = miniGame.c;

        answer4 = GameObject.Find("answer_4").GetComponentInChildren<Text>();
        answer4.text = miniGame.d;


        correctAnswer = GameObject.Find("correct_answer").GetComponent<Text>();
        correctAnswer.text = miniGame.answer;

        questionLevel = GameObject.Find("question_level").GetComponent<Text>();
        questionLevel.text = miniGame.level;
    }

    public void CheckQuestion()
    {
        //var currentEventSystem = EventSystem.current;
        //if (currentEventSystem == null) { return; }

        //var currentSelectedGameObject = currentEventSystem.currentSelectedGameObject;
        //if (currentSelectedGameObject == null) { return; }

        //if (currentSelectedGameObject.GetComponentInChildren<Text>().text.ToString() == GameObject.Find("correct_answer").GetComponent<Text>().text)
        //{
        //    var scoreAndTimeManager = GameObject.Find("ScoreAndTime");
        //    scoreAndTimeManager.GetComponent<ScoreAndTimeManager>().AddScore(10);
        //}
        //else
        //{
        //    Debug.Log("Lathos!");
        //}

        var currentEventSystem = EventSystem.current;
        if (currentEventSystem == null) { return; }

        var currentSelectedGameObject = currentEventSystem.currentSelectedGameObject;
        if (currentSelectedGameObject == null) { return; }

        if (currentSelectedGameObject.GetComponentInChildren<Text>().text.ToString() == GameObject.Find("correct_answer").GetComponent<Text>().text)
        {
            var scoreAndTimeManager = GameObject.Find("ScoreAndTime");
            var scoreToAdd = GameObject.Find("question_level").GetComponent<Text>().text == "easy" ? 100 : 150;
            scoreAndTimeManager.GetComponent<ScoreAndTimeManager>().AddScore(scoreToAdd);
            scoreAndTimeManager.GetComponent<ScoreAndTimeManager>().correctAnswers++;
            uiQuizPopup.SetActive(false);

            if (scoreAndTimeManager.GetComponent<ScoreAndTimeManager>().correctAnswers == 8)
            {
                var liveResultsManager = GameObject.Find("LiveScoreCanvas");
                liveResultsManager.SetActive(false);

                finalResultsPopup.SetActive(true);

                float t = Time.time - scoreAndTimeManager.GetComponent<ScoreAndTimeManager>().startTime;
                Time.timeScale = 0;
                string minutes = ((int)t / 60).ToString();
                string seconds = (t % 60).ToString("f2");

                GameObject.Find("finalScoreValue").GetComponent<Text>().text = scoreAndTimeManager.GetComponent<ScoreAndTimeManager>().score.ToString();
                GameObject.Find("finalTimeValue").GetComponent<Text>().text = minutes + ":" + seconds;
                GameObject.Find("finalMistakesValue").GetComponent<Text>().text = scoreAndTimeManager.GetComponent<ScoreAndTimeManager>().mistakes.ToString();

            }
            else
            {
                Time.timeScale = 1;
            }

            if (scoreAndTimeManager.GetComponent<ScoreAndTimeManager>().currentRoom == 1 &&
                scoreAndTimeManager.GetComponent<ScoreAndTimeManager>().correctAnswers == 4)
            {
                scoreAndTimeManager.GetComponent<ScoreAndTimeManager>().currentRoom = 2;
                GameObject.Find("Ch12").GetComponent<CapsuleCollider>().enabled = false;
                scoreAndTimeManager.GetComponent<ScoreAndTimeManager>().LoadMiniGames("60c5e90d8f159c9c8587e5d9", 1, "hard");
            }
        }
        else
        {
            var scoreAndTimeManager = GameObject.Find("ScoreAndTime");
            scoreAndTimeManager.GetComponent<ScoreAndTimeManager>().SubstractScore(50);
            scoreAndTimeManager.GetComponent<ScoreAndTimeManager>().mistakes++;
        }
    }
}
