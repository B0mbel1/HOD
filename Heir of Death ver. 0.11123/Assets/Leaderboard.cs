using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static PlayfabManager;

public class Leaderboard : MonoBehaviour
{
    public GameObject scoreboardEntryPrefab;
    public Transform scoreboardContainer;
    public List<PlayerData> playerDataList = new List<PlayerData>();

    public void Getit()
    {
        playerDataList.Clear();
        GetLeaderboard();
    }

    public void showld()
    {
        UpdateLeaderboard(playerDataList);
    }
    
    public void UpdateLeaderboard(List<PlayerData> playerDataList)
    {
        if (scoreboardContainer.transform.childCount > 0)
        {
            foreach (Transform child in scoreboardContainer)
            {
                Destroy(child.gameObject);
            }
        }
        float offsetY = 0f;
        
        foreach (PlayerData playerData in playerDataList)
        {
            GameObject scoreboardEntry = Instantiate(scoreboardEntryPrefab, scoreboardContainer);

            SetScoreboardEntryData(scoreboardEntry, playerData);
            
            RectTransform entryRectTransform = scoreboardEntry.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(entryRectTransform.anchoredPosition.x, -offsetY);
            
            offsetY += entryRectTransform.rect.height; 
        }
    }

    void SetScoreboardEntryData(GameObject entry, PlayerData playerData)
    {
        TextMeshProUGUI positionText = entry.transform.Find("Position/PositionText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI playerNameText = entry.transform.Find("Player/PlayerNameText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI scoreText = entry.transform.Find("PlayerScore/ScoreText").GetComponent<TextMeshProUGUI>();

        positionText.text = playerData.position.ToString();
        playerNameText.text = playerData.playerName;
        scoreText.text = playerData.score.ToString();
    }

    void OnErrorL(PlayFabError error)
    {
        Debug.Log(error.ErrorMessage);
        Debug.Log(error.GenerateErrorReport());
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

    public void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {

    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "KC",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnErrorL);
    }

    public void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            PlayerData nowyRekord = new PlayerData();
            nowyRekord.position = item.Position+1;
            nowyRekord.playerName = item.DisplayName;
            nowyRekord.score = item.StatValue;
            playerDataList.Add(nowyRekord);
        }
    }
}
