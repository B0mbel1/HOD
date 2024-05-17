using Unity.Netcode;
using UnityEngine;

public class Skrypt : NetworkBehaviour
{
    public bool czyHost;

    public AudioClip ameno;
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        NetworkLog.LogInfo("Nagrobek");
        audioSource.PlayOneShot(ameno);
    }

    [ClientRpc]
    public void BulletSpawnClientRpc(ClientRpcParams clientRpcParams)
    {
        audioSource.PlayOneShot(ameno);
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
