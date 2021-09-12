using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Linq;

public class MainMenu : MonoBehaviour
{
    public Text highestRankedMageUsername;

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenJamLink()
    {
        Application.OpenURL("https://itch.io/jam/azure-lux");
    }

    public void UpdateHighestScoredPlayer()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Highscores",
            StartPosition = 0,
            MaxResultsCount = 1
        };

        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardRefreshed, OnLeaderboardRefreshError);
    }

    private void OnLeaderboardRefreshError(PlayFabError error)
    {
        Debug.Log("Couldn't refresh leaderboard! Error: " + error.ErrorMessage);
    }

    private void OnLeaderboardRefreshed(GetLeaderboardResult obj)
    {
        var highestRankedMage = obj.Leaderboard.FirstOrDefault();

        highestRankedMageUsername.text = $"{highestRankedMage.DisplayName} (survived {highestRankedMage.StatValue} waves)";
    }
}
