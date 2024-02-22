



using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;

using UnityEngine;

public class CharacterSpawner : NetworkBehaviour
    
{

    public static CharacterSpawner Instance { get; internal set; }

    [SerializeField] private CharacterDatabase characterDatabase;

    [SerializeField] private Vector3[] spawnPoints;

    public Vector3 spawnPointClient;

    private void Awake()
    {
        Instance = this;
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            ulong host = NetworkManager.LocalClientId;

            foreach (var client in ServerManager.Instance.ClientData)
            {

                var character = characterDatabase.GetCharacterById(client.Value.characterId);
                if (character != null && host != client.Value.clientId)
                {
                    
                    var characterSpawnPoint = RandomSpawnPoint();
                    spawnPointClient = characterSpawnPoint;
                    var characterInstance = Instantiate(character.GameplayPrefab, characterSpawnPoint, Quaternion.identity);
                    
                    characterInstance.SpawnAsPlayerObject(client.Value.clientId);
                    
                    Debug.Log(client.Value.clientId + " pozycja " + characterSpawnPoint.x + " " + characterSpawnPoint.y + " " + characterSpawnPoint.z);
                    
                }
            }

            SpawnHost();
            //Invoke("SpawnHost", 2.0f);
            
        }
        
    }

    
    public void SpawnHost()
    {
        ulong host = NetworkManager.LocalClientId;
        var hostClient = ServerManager.Instance.ClientData.FirstOrDefault(c => c.Value.clientId == host);

        if (hostClient.Value != null)
        {
            var hostCharacter = characterDatabase.GetCharacterById(hostClient.Value.characterId);
            if (hostCharacter != null)
            {
                var hostSpawnPoint = RandomSpawnPoint();
                var hostInstance = Instantiate(hostCharacter.GameplayPrefab, hostSpawnPoint, Quaternion.identity);
                hostInstance.SpawnAsPlayerObject(hostClient.Value.clientId);
                Debug.Log(hostClient.Value.clientId + " pozycja " + hostSpawnPoint.x + " " + hostSpawnPoint.y + " " + hostSpawnPoint.z);
            }
        }
    }

    public Vector3 RandomSpawnPoint()
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex];
    }

    public Vector3 GetSpawnPoint()
    {
        return spawnPointClient;
    }
}
