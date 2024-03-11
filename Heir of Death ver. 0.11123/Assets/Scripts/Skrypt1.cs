using Unity.Netcode;
using UnityEngine;

public class Skrypt1 : NetworkBehaviour
{
    public bool czyHost;
    public AudioClip shelter;
    private AudioSource shelterSource;
    
    void Start()
    {
        shelterSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!IsOwner) return;

        if (IsHost)
        {
            czyHost = true;
        }
        if (IsClient && !IsHost)
        {
            czyHost = false;
        }
    }

    [ServerRpc]
    public void BulletSpawnServerRpc()
    {
        NetworkLog.LogInfo("Sciek³y niemiec");
        shelterSource.PlayOneShot(shelter);
    }

    [ClientRpc]
    public void BulletSpawnClientRpc(ClientRpcParams clientRpcParams)
    {
        shelterSource.PlayOneShot(shelter);
    }

    [ServerRpc]
    public void SynchroServerRpc()
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { 1 }
            }
        };

        BulletSpawnClientRpc(clientRpcParams);
    }
}
