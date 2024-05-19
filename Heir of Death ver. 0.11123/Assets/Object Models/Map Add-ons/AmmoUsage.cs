using Unity.Netcode;
using UnityEngine;

public class AmmoUsage : NetworkBehaviour
{
    public GameObject clip;

    public bool czyHost;

    void OnTriggerEnter(Collider other)
    {
        AmmoScript ammoScript = other.GetComponentInParent<AmmoScript>();
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

        if (playerHealth != null)
        {
            if (playerHealth.czyHost && IsServer)
            {
                ammoScript.GetAmmoServerRpc();
                
                destroyObjectServerRpc();
                destroySynchroObjectServerRpc(1);
            }

            if (playerHealth.czyHost == false && IsClient)
            {
                ammoScript.SynchroServerRpc();
                
                destroyObjectServerRpc();
                destroySynchroObjectServerRpc(0);
                destroySynchroObjectServerRpc(1);
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void destroyObjectServerRpc()
    {
        gameObject.SetActive(false);
    }

    [ServerRpc(RequireOwnership =false)]
    public void destroySynchroObjectServerRpc(ulong a)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { a }
            }
        };

        destroyObjectClientRpc(clientRpcParams);
    }

    [ClientRpc]
    public void destroyObjectClientRpc(ClientRpcParams clientRpcParams)
    {
        gameObject.SetActive(false);
    }
}
