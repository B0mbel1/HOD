using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RemedyUsage : NetworkBehaviour
{
    public GameObject remedy;

    //public void Update()
    //{
    //    if (!IsOwner) return;
    //}

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

        if (playerHealth != null)
        {
            if (playerHealth.czyHost && IsServer)
            {
                //playerHealth.RestoreHealthClientRpc(10f, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 0 } } });
                playerHealth.RestoreHealthServerRpc(10f);
                Destroy(gameObject);
            }

            if (playerHealth.czyHost == false && (!IsServer))
            {
                //playerHealth.RestoreHealthClientRpc(10f, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                playerHealth.SynchroServerRpc(10f);
                Destroy(gameObject);
            }
        }
    }
}






