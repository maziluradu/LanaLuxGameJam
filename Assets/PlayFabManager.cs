using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayFabManager : MonoBehaviour
{
    public UnityEvent onLoginSuccessful = new UnityEvent();
    public UnityEvent<string> onAuthenticationFailed = new UnityEvent<string>();

    private string username;
    private string email;
    private string password;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("SampleScene 1");
    }

    public void UpdateLeaderboard(int currentWave)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Highscores",
                    Value = currentWave
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

    public void Register(string email, string password, string username)
    {
        this.username = username;
        this.email = email;
        this.password = password;
        var registerRequest = new RegisterPlayFabUserRequest { Email = email, Password = password, Username = username };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, RegisterSuccess, RegisterFailed);
    }

    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        this.Login(email, password);

        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = username
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
        onAuthenticationFailed.Invoke("Register: " + error.ErrorMessage);
    }

    public void Login(string email, string password)
    {
        var request = new LoginWithEmailAddressRequest { Email = email, Password = password };
        PlayFabClientAPI.LoginWithEmailAddress(request, LoginSuccess, LoginFailed);
    }

    private void LoginSuccess(LoginResult result)
    {
        Debug.Log("Success: " + result);
        PlayerPrefs.SetString("AuthToken", result.SessionTicket);
        this.onLoginSuccessful.Invoke();
    }

    private void LoginFailed(PlayFabError error)
    {
        foreach (KeyValuePair<string, List<string>> keyValuePair in error.ErrorDetails)
        {
            Debug.Log("Key " + keyValuePair.Key + ": ");
            foreach (string errorDetail in keyValuePair.Value)
            {
                Debug.Log(errorDetail);
            }
        }
        onAuthenticationFailed.Invoke("Login: " + error.ErrorMessage);
    }
}
