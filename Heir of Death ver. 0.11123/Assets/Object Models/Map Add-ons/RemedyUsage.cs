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
            if (playerHealth.czyHost)
            {
                playerHealth.RestoreHealthClientRpc(10f, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 0 } } });
                Destroy(gameObject);
            }

            if (playerHealth.czyHost == false)
            {
                playerHealth.RestoreHealthClientRpc(10f, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                Destroy(gameObject);
            }
        }
    }
}






