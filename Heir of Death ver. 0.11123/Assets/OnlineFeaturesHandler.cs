using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PlayfabManager;

public class OnlineFeaturesHandler : MonoBehaviour
{
    public Button Multi;
    public Button Ranking;
    public TextMeshProUGUI Info;
    public GameObject Logowanie;
    public GameObject Wyloguj;
    public GameObject PoziomTxt;
    public GameObject PoziomNumber;
    public TextMeshProUGUI PoziomNumberTXT;
    public List<PlayerData> playerDataList = new List<PlayerData>();
    public double wynik = 0;
    public int zaokraglenie = 0;

    private void Start()
    {
        GetLeaderboard();
    }

    private void Update()
    {
        if (PlayfabManager.name == null)
        {
            Multi.interactable = false;
            Ranking.interactable = false;
            
            DisplayInfoAboutLogin();
            Logowanie.SetActive(true);
            Wyloguj.SetActive(false);
            PoziomTxt.SetActive(false);
            PoziomNumber.SetActive(false);
        }
        else
        {
            Logowanie.SetActive(false);
            Wyloguj.SetActive(true);
            PoziomTxt.SetActive(true);
            PoziomNumber.SetActive(true);
           
        }
    }

    public void DisplayInfoAboutLogin()
    {
        Info.text = "Aby skorzystaæ z tych funkcji zaloguj siê";
    }

    public void GotoLogowanie()
    {
        Cleanup();
        SceneManager.LoadScene("WelcomeScreen");
    }

    public void GotoNewLogowanie()
    {
        Cleanup();
        PlayfabManager.name = null;
        SceneManager.LoadScene("WelcomeScreen");
    }

    void Cleanup()
    {
        if (NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }
        if(ServerManager.Instance != null)
        {
            Destroy(ServerManager.Instance.gameObject);
        }
    }

    public void SendLvl(int wynik)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Lvl",
                    Value = wynik
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

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfull lvl send");
        GetLvl();
    }

    public void GetLvl()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Lvl",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnErrorL);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach(var item in result.Leaderboard)
        {
            if(item.DisplayName == PlayfabManager.name)
            {
                PoziomNumberTXT.text = item.StatValue.ToString();
            }
        }
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "KC",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet2, OnErrorL);
    }

    public void OnLeaderboardGet2(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            PlayerData nowyRekord = new PlayerData();
            nowyRekord.position = item.Position + 1;
            nowyRekord.playerName = item.DisplayName;
            nowyRekord.score = item.StatValue;
            playerDataList.Add(nowyRekord);
        }

        foreach (var data in playerDataList)
        {
            if(data.playerName == PlayfabManager.name)
            {
                wynik = data.score/5;
                Debug.Log(data.score);
                Debug.Log(wynik);
                zaokraglenie = (int)wynik;
                Debug.Log(zaokraglenie);

                if (wynik > zaokraglenie)
                {
                    zaokraglenie++;
                }

                if(zaokraglenie <= 0)
                {
                    zaokraglenie = 0;
                }

                SendLvl(zaokraglenie);
            }
        }
    }
}
