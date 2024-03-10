
using System;
using TMPro;
using Unity.Multiplayer.Samples.Utilities.ClientAuthority;
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
            Die(intVar);
            //RespawnPlayer(NetworkManager.LocalClientId);
        }
    }



    public void Die(int clientId)
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
        if (IsHost)
        { 
        //gameObject.GetComponent<NetworkObject>().Despawn();
        NetworkObject playerPrefab = NetworkManager.ConnectedClientsList[clientId].PlayerObject;
        playerPrefab.GetComponent<PlayerMovementOnline>().enabled = false;
        playerPrefab.GetComponentInChildren<MeshRenderer>().enabled = false;
        playerPrefab.GetComponentInChildren<shootingfinal>().enabled = false;
        RespawnPlayer(clientId);
        }
        if (IsClient)
        {
            DieServerRpc(clientId);
            RespawnPlayerServerRpc(clientId, spawnPoint);
            dead = false;
        }
    }

    public void RespawnPlayer(int clientId)
    {

        // NetworkObject playerPrefab = Instantiate(NetworkManager.LocalClient.PlayerObject, new Vector3(0f, 1f, 0f), Quaternion.identity);
        // playerPrefab.SpawnAsPlayerObject(clientId);
        NetworkObject playerPrefab = NetworkManager.ConnectedClientsList[clientId].PlayerObject;
        transform.position = spawnPoint;
        playerPrefab.GetComponentInChildren<MeshRenderer>().enabled = true;
        playerPrefab.GetComponent<PlayerMovementOnline>().enabled = true;
        playerPrefab.GetComponentInChildren<shootingfinal>().enabled = true;


        dead = false;
        // Invoke("RestoreHealth", 2f);
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

    [ServerRpc(RequireOwnership = false)]
    public void DieServerRpc(int clientid)
    {
        NetworkObject playerPrefab = NetworkManager.ConnectedClientsList[clientid].PlayerObject;
        playerPrefab.GetComponent<PlayerMovementOnline>().enabled = false;
        playerPrefab.GetComponentInChildren<MeshRenderer>().enabled = false;
        playerPrefab.GetComponentInChildren<shootingfinal>().enabled = false;
    }

    [ServerRpc(RequireOwnership = false)]
    public void RespawnPlayerServerRpc(int clientid, Vector3 position)
    {
        NetworkObject playerPrefab = NetworkManager.ConnectedClientsList[clientid].PlayerObject;
        playerPrefab.GetComponent<ClientNetworkTransform>().transform.position = position;
        ChangeClientPositionClientRpc(position);
        playerPrefab.GetComponentInChildren<MeshRenderer>().enabled = true;
        playerPrefab.GetComponent<PlayerMovementOnline>().enabled = true;
        playerPrefab.GetComponentInChildren<shootingfinal>().enabled = true;
    }

    [ClientRpc]
    public void ChangeClientPositionClientRpc(Vector3 position)
    {
        if (!IsHost)
        {
            transform.position = position;
        }
    }
}
