
using Unity.Multiplayer.Samples;
using Unity.Multiplayer.Samples.Utilities.ClientAuthority;
using Unity.Netcode;

using UnityEngine;

public class EnableCNT : ClientNetworkTransform
{
    public override void OnNetworkSpawn()
    {
        if(!IsOwner || IsServer) return;
         
        transform.position = CharacterSpawner.Instance.GetSpawnPoint();
       
        base.OnNetworkSpawn();
    }

    public void ChangePos()
    {
        transform.position = CharacterSpawner.Instance.GetSpawnPoint();
    }
}
