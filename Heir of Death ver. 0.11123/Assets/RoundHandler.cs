using System;
using System.Collections.Generic;
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
    public static bool klientDed, hostDed;

    private void Update()
    {
        if (IsHost && PlayerHealth.health <= 0)
        {
            //respawn2ServerRpc(true);

            respawnClientRpc(new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });

        }

        if (!IsHost && IsClient && PlayerHealth.health <= 0)
        {
            respawnServerRpc(true);
        }

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
        if (!IsOwner) return;

        if (klientDed == true)
        {
            //respawnSynchroServerRpc(1);
            RespawnPlayer();
            klientDed = false;
        }
        if (hostDed == true)
        {
            //respawnSynchroServerRpc(0);
            RespawnPlayer();
            hostDed = false;
        }

        if (dead == true)
        {
            if (IsHost)
            {
                Die();
                RespawnPlayer();
            }

            if (!IsHost && IsClient)
            {
                Die();
                RespawnPlayer();
            }

            setTimeServerRpc();
            setTimeSynchroServerRpc(1);
            //RespawnPlayer(NetworkManager.LocalClientId);
        }

        if (TimerScript.TimeLeft == 0)
        {
            if (IsHost)
            {
                RespawnPlayer();
            }

            if (!IsHost && IsClient)
            {
                RespawnPlayer();
            }
        }
    }



    [ServerRpc]
    public void setTimeServerRpc()
    {
        TimerScript.TimeLeft = 10;       //ZMIANA
    }

    [ClientRpc]
    public void setTimeClientRpc()
    {
        TimerScript.TimeLeft = 10;          //ZMIANA
    }

    [ServerRpc]
    public void setTimeSynchroServerRpc(ulong a)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { a }
            }
        };

        respawnClientRpc(clientRpcParams);
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

        //RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        GetComponentInChildren<MeshRenderer>().enabled = true;
        GetComponent<PlayerMovementOnline>().enabled = true;
        GetComponentInChildren<shootingfinal>().enabled = true;

        transform.position = spawnPoint;
        dead = false;

        odnow = false;

        PlayerHealth.health = 100;

        TimerScript.TimeLeft = 10f;              //ZMIANA
    }

    [ServerRpc(RequireOwnership =false)]
    public void respawnServerRpc(bool klient)
    {
        klientDed = klient;
    }

    [ServerRpc(RequireOwnership = false)]
    public void respawn2ServerRpc(bool host)
    {
        hostDed = host;
    }

    [ClientRpc]
    public void respawnClientRpc(ClientRpcParams clientRpcParams)
    {
        hostDed = true;
    }

    [ServerRpc(RequireOwnership = false)]
    public void respawnSynchroServerRpc(ulong a)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { a }
            }
        };
        respawnClientRpc(clientRpcParams);
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
