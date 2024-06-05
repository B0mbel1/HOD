using Unity.Multiplayer.Samples.Utilities.ClientAuthority;

public class EnableCNT : ClientNetworkTransform
{
    public override void OnNetworkSpawn()
    {
        if(!IsOwner || IsServer) return;

        transform.position = CharacterSpawner.Instance.spawnPointClient;
        base.OnNetworkSpawn();
    }

    public void ChangePos()
    {
        transform.position = CharacterSpawner.Instance.spawnPointClient;
    }
}
