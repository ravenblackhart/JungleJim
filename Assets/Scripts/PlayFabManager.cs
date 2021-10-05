using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab; 
using PlayFab.ClientModels;

public class PlayFabManager : MonoBehaviour
{
    public GameObject rowPrefab;
    public GameObject rowsParent;

    private void Start()
    {
        LoginPlayFab();
    }

    void LoginPlayFab()
    {
        var request = new LoginWithCustomIDRequest()
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result) => Debug.Log($"Login Successful");

    void OnError(PlayFabError error)
    {
        Debug.Log($"Error while logging in");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate
                {
                    StatisticName = "DistanceScore", Value = score
                }
                
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result) => Debug.Log("Update Successful");
}
