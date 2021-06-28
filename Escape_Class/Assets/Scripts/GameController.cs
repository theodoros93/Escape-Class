using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject CM_FreeLook1;

    public void onPause() 
    {
        Time.timeScale = 0;
    }

    public void ResumeGame() 
    {
        Time.timeScale = 1;
        CM_FreeLook1.SetActive(true);
        GameObject.Find("Player").GetComponent<AgentMovement>().movementSpeed = 15;
    }


    public void ExitGame() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start page");
    }
}
