using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    [SerializeField] private GameObject uiElement;
    [System.Obsolete]
    
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
            Application.LoadLevel("Stage02");
        }
    }
}
