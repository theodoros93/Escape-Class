using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserLoginController : MonoBehaviour
{
    public string email;
    public string password;

    public void UserLogin()
    {
        Debug.Log(email);
        Debug.Log(password);
    }

    public void SetEmail(string inputEmail)
    {
        email = inputEmail;
    }

    public void SetPassword(string inputPassword)
    {
        password = inputPassword;
    }
}
