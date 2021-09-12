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
    public UnityEvent onLoginSuccessful = new UnityEvent();

    public void Start()
    {
        error.text = "";
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
        var registerRequest = new RegisterPlayFabUserRequest { Email = email.text, Password = password.text, Username = username.text };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, RegisterSuccess, RegisterFailed);
    }

    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        this.Login();

        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = username.text
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateUserDisplayNameSuccess, OnUpdateUserDisplayNameError);
    }

    private void OnUpdateUserDisplayNameError(PlayFabError error)
    {
        Debug.Log("Error while setting user's display name!");
    }

    private void OnUpdateUserDisplayNameSuccess(UpdateUserTitleDisplayNameResult obj)
    {
        Debug.Log("Successfully updated the user's display name!");
    }

    private void RegisterFailed(PlayFabError error)
    {
        foreach (KeyValuePair<string, List<string>> keyValuePair in error.ErrorDetails)
        {
            Debug.Log("Key " + keyValuePair.Key + ": ");
            foreach (string errorDetail in keyValuePair.Value)
            {
                Debug.Log(errorDetail);
            }
        }
        this.error.text = "Register: " + error.ErrorMessage;
    }

    private void Login()
    {
        var request = new LoginWithEmailAddressRequest { Email = email.text, Password = password.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, LoginSuccess, LoginFailed);
    }

    private void LoginSuccess(LoginResult result)
    {
        Debug.Log("Sucess: " + result);
        this.onLoginSuccessful.Invoke();

        UpdateLeaderboard();
    }

    private void LoginFailed(PlayFabError error)
    {
        foreach (KeyValuePair<string, List<string>> keyValuePair in error.ErrorDetails) {
            Debug.Log("Key " + keyValuePair.Key + ": ");
            foreach (string errorDetail in keyValuePair.Value)
            {
                Debug.Log(errorDetail);
            }
        }
        this.error.text = "Login: " + error.ErrorMessage;
    }

    private void UpdateLeaderboard()
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Highscores",
                    Value = UnityEngine.Random.Range(0, 1000)
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnLeaderboardUpdateError);
    }

    private void OnLeaderboardUpdateError(PlayFabError error)
    {
        Debug.Log("Couldn't update leaderboard!");
        Debug.Log(error.ErrorMessage);
    }

    private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult error)
    {
        Debug.Log("Updated leaderboard successfully!");
    }
}
