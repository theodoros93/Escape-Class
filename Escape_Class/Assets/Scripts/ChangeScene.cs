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
    [SerializeField] private int questionId;
    Text question;
    Text answer1;
    Text answer2;
    Text answer3;
    Text answer4;
    Text correctAnswer;
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
            //Application.LoadLevel("Stage02");
            uiQuizPopup.SetActive(true);
            LoadQuestion();
            Time.timeScale = 0;
        }
    }

    void LoadQuestion()
    {
        //var miniGames = new MiniGames();
        //List<MiniGame> miniGamesList = await miniGames.GetMiniGames(new ObjectId("60c5e98c8f159c9c8587e5dc"), 3, "easy");

        miniGame = ScoreAndTimeManager.getMiniGame(questionId);

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
    }

    public void CheckQuestion()
    {
        var currentEventSystem = EventSystem.current;
        if (currentEventSystem == null) { return; }

        var currentSelectedGameObject = currentEventSystem.currentSelectedGameObject;
        if (currentSelectedGameObject == null) { return; }

        Debug.Log(currentSelectedGameObject.GetComponentInChildren<Text>().text.ToString());
        Debug.Log(GameObject.Find("correct_answer").GetComponent<Text>().text);

        if (currentSelectedGameObject.GetComponentInChildren<Text>().text.ToString() == GameObject.Find("correct_answer").GetComponent<Text>().text)
        {
            Debug.Log("Sostos!");
        }
        else
        {
            Debug.Log("Lathos!");
        }
    }
}
