using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterSpawner : NetworkBehaviour
{
    [SerializeField] private CharacterDatabase characterDatabase;
    [SerializeField] private Transform characterSpawnPoint;
    public override void OnNetworkSpawn()
    {
        if(!IsServer) { return; }

        foreach(var client in ServerManager.Instance.ClientData)
        {
            var character = characterDatabase.GetCharacterById(client.Value.characterId);
            if(character != null)
            {
                var spawnPos = new Vector3(characterSpawnPoint.position.x, characterSpawnPoint.position.y, characterSpawnPoint.position.z);
                var characterInstance = Instantiate(character.GameplayPrefab, spawnPos, Quaternion.identity);
                characterInstance.SpawnAsPlayerObject(client.Value.clientId);
            }
        }
        
    }
}
