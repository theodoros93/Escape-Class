using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserLoginController : MonoBehaviour
{
    public string username;
    public string password;

    public async void UserLogin()
    {
        var authenticateNow = new AuthenticateUser();
        var auth = await authenticateNow.Authenticate(username, password);
        Debug.Log(auth.found);
        Debug.Log(auth.role);
        Debug.Log(username);
        Debug.Log(password);
        if (auth.found)
        {
            if (auth.role=="Teacher")
            {
                SceneManager.LoadScene("Teacher Start Page");
            }
            else
            {
                SceneManager.LoadScene("Start page");
            }
        }
        //SceneManager.LoadScene("Start page");
    }

    public void SetEmail(string inputUsername)
    {
        username = inputUsername;
    }

    public void SetPassword(string inputPassword)
    {
        password = inputPassword;
    }
}
