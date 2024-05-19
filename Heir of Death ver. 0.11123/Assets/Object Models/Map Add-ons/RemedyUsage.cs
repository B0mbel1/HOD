using Unity.Netcode;
using UnityEngine;

public class RemedyUsage : NetworkBehaviour
{
    public GameObject remedy;

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

        if (playerHealth != null)
        {
            if (playerHealth.czyHost && IsServer)
            {
                playerHealth.RestoreHealthServerRpc();

                destroyRemedyServerRpc();
                destroySynchroRemedyServerRpc(1);
                Debug.Log("Zycie:" + PlayerHealth.health);
            }

            if (playerHealth.czyHost == false && (!IsServer))
            {
                playerHealth.SynchroServerRpc();
                
                destroyRemedyServerRpc();
                destroySynchroRemedyServerRpc(1);
                destroySynchroRemedyServerRpc(0);
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void destroyRemedyServerRpc()
    {
        gameObject.SetActive(false);
    }

    [ServerRpc(RequireOwnership = false)]
    public void destroySynchroRemedyServerRpc(ulong a)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { a }
            }
        };

        destroyRemedyClientRpc(clientRpcParams);
    }

    [ClientRpc]
    public void destroyRemedyClientRpc(ClientRpcParams clientRpcParams)
    {
        gameObject.SetActive(false);
    }
}
