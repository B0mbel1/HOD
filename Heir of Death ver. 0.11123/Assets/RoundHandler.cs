using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class RoundHandler : NetworkBehaviour
{
    public Vector3 spawnPoint;
    public GameObject GetSpawnPosition;
    public TextMeshProUGUI ownerRoundCountTxt;
    public TextMeshProUGUI opponentRoundCountTxt;
    public bool dead;
    public ulong ulongVar;
    public int intVar;
    
    public static bool odnow;

    public override void OnNetworkSpawn()
    {
        spawnPoint = GetSpawnPosition.transform.position;
        dead = false;
        Debug.Log(spawnPoint);
        base.OnNetworkSpawn();
        ulongVar = NetworkManager.LocalClientId;
        intVar = Convert.ToInt32(ulongVar.ToString());
    }

    private void Update()
    {
        if (!IsOwner) return;

        if (PlayerHealth.health <= 0)
        {
            dead = true;
            odnow = true;
            PlayerHealth.health = 100;
        }

        if (IsHost)
        {
            ownerRoundCountTxt.text = RoundCounter.counterhost.ToString();
            opponentRoundCountTxt.text = RoundCounter.counterclient.ToString();
        }
        else
        {
            ownerRoundCountTxt.text = RoundCounter.counterclient.ToString();
            opponentRoundCountTxt.text = RoundCounter.counterhost.ToString();
        }
    }

    private void FixedUpdate()
    {
        if (dead == true)
        {
            Die();
            //RespawnPlayer(NetworkManager.LocalClientId);
        }
    }

    public void Die()
    {
        if (!IsHost && IsClient)
        {
            HostRoundWin();
            HostRoundWinServerRpc();
        }

        if (IsHost)
        {
            ClientRoundWin();
            UpdateClientRoundWinClientRpc();
        }

        Debug.Log("Destroying");
       
        GetComponent<PlayerMovementOnline>().enabled = false;
        GetComponentInChildren<MeshRenderer>().enabled = false;
        GetComponentInChildren<shootingfinal>().enabled = false;
        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        GetComponentInChildren<MeshRenderer>().enabled = true;
        GetComponent<PlayerMovementOnline>().enabled = true;
        GetComponentInChildren<shootingfinal>().enabled = true;

        transform.position = spawnPoint;
        dead = false;

        odnow = false;
    }

    public void HostRoundWin()
    {
        RoundCounter.counterhost += 1;
    }

    [ServerRpc(RequireOwnership = false)]
    public void HostRoundWinServerRpc()
    {
        RoundCounter.counterhost += 1;
    }

    public void ClientRoundWin()
    {
        RoundCounter.counterclient += 1;
    }

    [ClientRpc]
    public void UpdateClientRoundWinClientRpc()
    {
        if (!IsHost)
        {
            RoundCounter.counterclient += 1;
        }
    }
}
