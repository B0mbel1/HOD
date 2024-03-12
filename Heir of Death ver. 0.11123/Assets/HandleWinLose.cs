using PlayFab;
using PlayFab.ClientModels;
using PlayFab.EventsModels;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleWinLose : NetworkBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;
    public int wynik;
    private bool leaderboardUpdated = false;

    public void Update()
    {
        //if (!IsOwner) return;
        if (RoundCounter.counterhost >= 10 && !leaderboardUpdated)
        {
            if (IsHost)
            {
                wynik = 1;
                SendLeaderboard(wynik);
                ActivateWinScreen();
                leaderboardUpdated = true;
            }
            if (!IsHost && IsClient)
            {
                ActivateLoseScreen();
                leaderboardUpdated = true;
            }
        }

        if (RoundCounter.counterclient >= 10 && !leaderboardUpdated)
        {
            if (IsHost)
            {
                ActivateLoseScreen();
                leaderboardUpdated = true;
            }
            if (!IsHost && IsClient)
            {
                wynik = 1;
                SendLeaderboard(wynik);
                ActivateWinScreen();
                leaderboardUpdated = true;
            }
        }
    }

    public override void OnNetworkDespawn()
    {
        
        base.OnNetworkDespawn();
    }
    public void ActivateWinScreen()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0f;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }

    public void ActivateLoseScreen()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void WyjdzZGry()
    {
        RoundCounter.counterhost = 0;
        RoundCounter.counterclient = 0;
        Time.timeScale = 1f;
        ServerManager.Instance.StopServer();

        
        SceneManager.LoadScene("Menu");
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "KC",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnErrorL);
    }

    void OnErrorL(PlayFabError error)
    {
        Debug.Log(error.ErrorMessage);
        Debug.Log(error.GenerateErrorReport());
    }



    public void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {

    }
}
