using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayfabManager;

public class Leaderboard : MonoBehaviour
{
    public GameObject scoreboardEntryPrefab;
    public Transform scoreboardContainer;
    private void Awake()
    {
        PlayfabManager bedobledy = new PlayfabManager();
        bedobledy.GetLeaderboard();
    }

    
}
