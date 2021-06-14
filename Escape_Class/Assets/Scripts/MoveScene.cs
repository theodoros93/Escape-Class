using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    [SerializeField] private string SampleScene;
    [SerializeField] private GameObject uiElement;
    private void OnTriggerStay(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            //Make UI Appear
            uiElement.SetActive(true);
            //Button Press
            if(Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("Stage02");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            uiElement.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
