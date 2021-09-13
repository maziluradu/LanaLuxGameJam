using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeroShower : MonoBehaviour
{
    public void Start()
    {
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            UpdateHighestScoredPlayer();
        }
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

        if (highestRankedMage != null)
        {
            GetComponent<TextMesh>().text = $"Behold\n{highestRankedMage.DisplayName},\nbest mage there is";
        }
    }
}
