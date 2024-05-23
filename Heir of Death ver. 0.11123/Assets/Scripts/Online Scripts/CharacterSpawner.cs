using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class CharacterSpawner : NetworkBehaviour
{
    public static CharacterSpawner Instance { get; internal set; }

    [SerializeField] private CharacterDatabase characterDatabase;

    // [SerializeField] private Vector3[] spawnPoints;

    public Vector3 spawnPointClient;
    public Vector3 spawnPointHost;

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
                    var characterInstance = Instantiate(character.GameplayPrefab, spawnPointClient, Quaternion.identity);
                    characterInstance.SpawnAsPlayerObject(client.Value.clientId);
                }
            }
            SpawnHost();
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
                var hostInstance = Instantiate(hostCharacter.GameplayPrefab, spawnPointHost, Quaternion.identity);
                hostInstance.SpawnAsPlayerObject(hostClient.Value.clientId);
            }
        }
    }

    public Vector3 GetSpawnPoint()
    {
        return spawnPointClient;
    }
}
