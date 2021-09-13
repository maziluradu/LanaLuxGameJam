using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AuthenticationMenu : MonoBehaviour
{
    [SerializeField] public Text username, password, email, error, submit;
    public PlayFabManager playFabManager;

    public void Start()
    {
        error.text = "";

        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            playFabManager.onLoginSuccessful.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void SetErrorText(string text)
    {
        error.text = text;
    }

    public void Submit()
    {
        if (submit.text == "LOGIN")
        {
            Login();
        }
        else
        {
            Register();
        }
    }

    private void Register()
    {
        playFabManager.Register(email.text, password.text, username.text);
    }

    private void Login()
    {
        playFabManager.Login(email.text, password.text);
    }
}
