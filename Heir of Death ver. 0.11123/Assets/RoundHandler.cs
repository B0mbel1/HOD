using System;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class RoundHandler : NetworkBehaviour
{
    public Vector3 spawnPoint;
    
    public Vector3 hostSpawn;
    public Vector3 clientSpawn;

    public TextMeshProUGUI ownerRoundCountTxt;
    public TextMeshProUGUI opponentRoundCountTxt;
    public bool dead;
    public ulong ulongVar;
    public int intVar;

    public override void OnNetworkSpawn()
    {
        if (IsHost)
        {
            spawnPoint = hostSpawn;
        }

        if (!IsHost && IsClient)
        {
            spawnPoint = clientSpawn;
        }


        dead = false;
        
        base.OnNetworkSpawn();

        ulongVar = NetworkManager.LocalClientId;
        intVar = Convert.ToInt32(ulongVar.ToString());

    }
    public static bool klientDed, hostDed;

    private void Update()
    {
        if (!IsOwner) return;

        if (IsHost && PlayerHealth.health <= 0)
        {
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
            RespawnPlayer();
            klientDed = false;
        }
        if (hostDed == true)
        {
            RespawnPlayer();
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
        TimerScript.TimeLeft = 90;       
    }

    [ClientRpc]
    public void setTimeClientRpc()
    {
        TimerScript.TimeLeft = 90;         
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


        PlayerHealth.health = 100;

        TimerScript.TimeLeft = 90f;             
    }

    [ServerRpc(RequireOwnership = false)]
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