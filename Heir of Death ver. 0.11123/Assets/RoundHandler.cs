
using TMPro;

using Unity.Netcode;

using UnityEngine;



public class RoundHandler : NetworkBehaviour
{
    public Vector3 spawnPoint;
    public TextMeshProUGUI ownerRoundCountTxt;
    public TextMeshProUGUI opponentRoundCountTxt;
    public bool dead;
    RoundCounter counter;

    public override void OnNetworkSpawn()
    {
        
        spawnPoint = NetworkManager.Singleton.ConnectedClients[NetworkManager.LocalClientId].PlayerObject.transform.position;
        dead = false;
        Debug.Log(spawnPoint);
        counter = GetComponent<RoundCounter>();
        base.OnNetworkSpawn();
        
    }
    private void Update()
    {
        
        if (!IsOwner) return;
        if(PlayerHealth.health <= 0)
        {
            dead = true;
            PlayerHealth.health = 100;
        }
        
        if(IsHost)
        {
            ownerRoundCountTxt.text = RoundCounter.counter0.ToString();
            opponentRoundCountTxt.text = RoundCounter.counter1.ToString();
        }

        if(IsClient && !IsHost)
        {
            ownerRoundCountTxt.text = RoundCounter.counter1.ToString();
            opponentRoundCountTxt.text = RoundCounter.counter0.ToString();
        }
        
        
        
        
    }

    private void FixedUpdate()
    {
        if (dead == true)
        {
            Die(NetworkManager.LocalClientId);
            //RespawnPlayer(NetworkManager.LocalClientId);
        }
    }

    

    public void Die(ulong clientId)
    {
        if (IsHost)
        {
            counter.UpdateHostLocally();
            if (!IsHost)
            {
                counter.GetClientCounterClientRpc();
            }
        }

        if (IsClient && !IsHost)
        {
            counter.UpdateClientLocally();
            counter.UpdateClientCounterServerRpc();
        }
        Debug.Log("Destroying");
            //gameObject.GetComponent<NetworkObject>().Despawn();
            NetworkObject playerPrefab = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
            playerPrefab.GetComponent<PlayerMovementOnline>().enabled = false;
            playerPrefab.GetComponentInChildren<MeshRenderer>().enabled = false;
            playerPrefab.GetComponentInChildren<shootingfinal>().enabled = false;
        RespawnPlayer(clientId);
    }

    public void RespawnPlayer(ulong clientId)
    {
        
        // NetworkObject playerPrefab = Instantiate(NetworkManager.LocalClient.PlayerObject, new Vector3(0f, 1f, 0f), Quaternion.identity);
        // playerPrefab.SpawnAsPlayerObject(clientId);
        NetworkObject playerPrefab = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        transform.position = spawnPoint;
        playerPrefab.GetComponentInChildren<MeshRenderer>().enabled = true;
        playerPrefab.GetComponent<PlayerMovementOnline>().enabled = true;
        playerPrefab.GetComponentInChildren<shootingfinal>().enabled = true;
        
        
        dead = false;
        // Invoke("RestoreHealth", 2f);
    }

    
}
