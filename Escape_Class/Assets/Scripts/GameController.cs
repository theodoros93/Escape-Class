using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void onPause() 
    {
        Time.timeScale = 0;
    }

    public void ResumeGame() 
    {
        Time.timeScale = 1;
    }

    public void ExitGame() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start page");
    }
}
